using System.Data;
using System.Data.Common;
using WHToolkit.Database.Provider;

namespace WHToolkit.Database;

/// <summary>
/// Abstract database helper class providing multi-database support
/// </summary>
public abstract partial class DBHelper : IDisposable
{
    private readonly string _securityKey = "HwH_DbHeLpEr_KeY"; // Reserved for future encryption functionality
    private readonly string _instanceId = Guid.NewGuid().ToString();

    private static readonly SemaphoreSlim _creatorSemaphore = new(1, 1);
    private readonly SemaphoreSlim _instanceSemaphore = new(1, 1);
    private bool _disposed;
    
    internal static readonly Dictionary<string, (object Data, DateTime Expiry)> CommandCache = new();
    internal static readonly ConstantsDefine Constants = new();
    internal readonly DatabaseEnvironment Environment = new();

    public IDbTransaction? Transaction;

    // Abstract properties for provider-specific implementations
    public abstract string TextPrefix { get; set; }
    public abstract string InPrefix { get; set; }
    public abstract string OutPrefix { get; set; }

    public string CurrentPrefix { get; set; } = string.Empty;

    /// <summary>
    /// Enable 1-second caching (applies to entire helper instance)
    /// </summary>
    public bool IsDefaultCache { get; set; } = true;
    
    /// <summary>
    /// Enable 1-second caching (applies to current operation only)
    /// </summary>
    public bool IsCache { get; set; } = true;

    public DataSet? LastResultDataSet { get; private set; }
    public int LastResultNonQuery { get; private set; }
    public object? LastResultScalar { get; private set; }
    public string ConfigName { get; private set; } = string.Empty;

    public bool IsEmptyStringEqualNull { get; set; }

    #region Abstract properties

    protected abstract string SequenceNextFormat { get; }
    protected abstract IDbConnection Connection { get; set; }
    public abstract IDbCommand Command { get; set; }
    protected abstract IDbDataAdapter DataAdapter { get; set; }
    protected abstract DbParameter ConvertParameter(DataParameter parameter);

    #endregion

    #region Virtual properties

    protected virtual bool AllowTextParameter => true;
    public virtual bool AllowConnectionTransaction { get; set; } = true;
    protected virtual bool AllowAutoParameterMapping => true;

    #endregion

    #region Public properties

    public bool IsDoNotLog { get; set; } = false;

    public bool IsEncrypted
    {
        get => Environment.IsEncrypted;
        set
        {
            if (value) Environment.IsFullEncrypted = false;
            Environment.IsEncrypted = value;
        }
    }

    public bool IsFullEncrypted
    {
        get => Environment.IsFullEncrypted;
        set
        {
            if (value) Environment.IsEncrypted = false;
            Environment.IsFullEncrypted = value;
        }
    }

    public int CommandTimeout
    {
        get => Environment.CommandTimeout;
        set => Environment.CommandTimeout = value;
    }

    public ProviderKind Provider => Environment.Provider;

    public string ConnectionString
    {
        get => DecryptConnectionString(Environment.ConnectionString);
        private set => Environment.ConnectionString = value;
    }

    public DataParameterCollection Parameters = new();
    public bool IsStayConnectedAlways { get; set; } = false;
    public virtual bool IsAutoTransaction { get; set; } = true;
    public string Database => Connection.Database;
    public int RetryCount { get; set; } = 1;

    #endregion

    public static DBHelper? Instance = null;

    #region CreateInstance methods

    public static async Task<DBHelper?> CreateInstanceAsync(string configName)
    {
        return await CreateInstanceAsync(null, configName);
    }

    public static async Task<DBHelper?> CreateInstanceAsync(string? providerName, string configName)
    {
        DBHelper? db = null;

        await _creatorSemaphore.WaitAsync();
        try
        {
            // In a real implementation, you would load configuration from appsettings.json or other config source
            // For now, this is a placeholder that would need to be implemented based on your configuration system
            
            if (!string.IsNullOrWhiteSpace(providerName))
                db = SetProvider(providerName);
            else
                throw new InvalidOperationException($"Database provider for {configName} cannot be determined.");

            if (db != null)
            {
                db.ConfigName = configName;
                // db.ConnectionString = connectionConfig.ConnectionString; // Load from config
            }
        }
        catch (Exception ex)
        {
            // Log error here if needed
            Console.WriteLine($"Error creating database instance: {ex.Message}");
        }
        finally
        {
            _creatorSemaphore.Release();
        }

        return db;
    }

    public static async Task<DBHelper?> CreateConnectionAsync(string providerName, string connectionString)
    {
        DBHelper? db = null;

        await _creatorSemaphore.WaitAsync();
        try
        {
            if (!string.IsNullOrWhiteSpace(providerName))
                db = SetProvider(providerName);
            else
                throw new InvalidOperationException("Database provider cannot be determined.");

            if (db != null)
                db.ConnectionString = connectionString;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating database connection: {ex.Message}");
        }
        finally
        {
            _creatorSemaphore.Release();
        }

        return db;
    }

    public static async Task<DBHelper?> CreateConnectionAsync(ProviderKind provider, string connectionString)
    {
        DBHelper? db = null;

        await _creatorSemaphore.WaitAsync();
        try
        {
            if (provider != ProviderKind.None)
                db = SetProvider(provider);
            else
                throw new InvalidOperationException("Database provider cannot be determined.");

            if (db != null)
                db.ConnectionString = connectionString;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating database connection: {ex.Message}");
        }
        finally
        {
            _creatorSemaphore.Release();
        }

        return db;
    }

    #endregion

    #region Private static methods

    private static DBHelper? SetProvider(string providerName)
    {
        foreach (var item in Constants.ProviderMapping)
        {
            if (providerName.ToUpper().Contains(item.Key.ToUpper()))
            {
                return SetProvider(item.Value);
            }
        }

        throw new NotSupportedException($"{providerName} is not a supported provider name. Please update HWH.Framework.");
    }

    private static DBHelper? SetProvider(ProviderKind provider)
    {
        return provider switch
        {
            ProviderKind.MSSQL => new DataMSSQL(),
            ProviderKind.Oracle => new DataOracle(),
            ProviderKind.MySQL => new DataMySQL(),
            ProviderKind.PostgreSQL => new DataNpgsql(),
            _ => throw new NotSupportedException($"{provider} provider class is not defined. Please update HWH.Framework.")
        };
    }

    #endregion

    #region Transaction methods

    public async Task TransactionBeginAsync()
    {
        await TransactionBeginAsync(IsolationLevel.ReadCommitted);
    }

    public async Task TransactionBeginAsync(IsolationLevel level)
    {
        try
        {
            if (AllowConnectionTransaction)
            {
                await _instanceSemaphore.WaitAsync();
                try
                {
                    if (Connection.State != ConnectionState.Open)
                        await ((DbConnection)Connection).OpenAsync();

                    Transaction ??= ((DbConnection)Connection).BeginTransaction(level);
                }
                finally
                {
                    _instanceSemaphore.Release();
                }
            }
        }
        catch (Exception ex)
        {
            if (!IsDoNotLog) Console.WriteLine($"Transaction error: {ex.Message}");
            throw;
        }
    }

    public async Task TransactionCommitAsync()
    {
        if (Transaction != null)
        {
            if (Transaction is DbTransaction dbTransaction)
                await dbTransaction.CommitAsync();
            else
                Transaction.Commit();
                
            Transaction = null;

            if (Command.Connection != null)
            {
                await ((DbConnection)Command.Connection).CloseAsync();
            }

            if (Connection != null)
            {
                await ((DbConnection)Connection).CloseAsync();
            }
        }
    }

    public async Task TransactionRollbackAsync()
    {
        if (Transaction != null)
        {
            if (Transaction is DbTransaction dbTransaction)
                await dbTransaction.RollbackAsync();
            else
                Transaction.Rollback();
                
            Transaction = null;

            if (Command.Connection != null)
            {
                await ((DbConnection)Command.Connection).CloseAsync();
            }

            if (Connection != null)
            {
                await ((DbConnection)Connection).CloseAsync();
            }
        }
    }

    #endregion

    #region Execute methods

    public virtual async Task<DataSet> ExecuteDataSetAsync(CommandType commandType, string commandText, DataParameterCollection? parameters = null)
    {
        Instance = this;

        await _instanceSemaphore.WaitAsync();
        try
        {
            int errorCount = 0;
            bool isError;
            Exception? errorException = null;

            do
            {
                IDbTransaction? tran = null;
                try
                {
                    isError = false;

                    Command.CommandType = commandType;
                    Command.CommandText = commandText;
                    Command.Parameters.Clear();

                    if (parameters?.Count > 0)
                    {
                        if (commandType == CommandType.Text)
                            CurrentPrefix = TextPrefix;

                        SetParameter(Command, parameters);
                    }

                    LastResultDataSet = null;
                    string cacheKey = $"ExecuteDataSet_{GetCommandKey()}";

                    // 1-second caching mechanism with expiration
                    bool usedCache = false;
                    if (IsCache && CommandCache.ContainsKey(cacheKey))
                    {
                        var cached = CommandCache[cacheKey];

                        // Check if cache is still valid (within 1 second)
                        if (DateTime.Now < cached.Expiry)
                        {
                            LastResultDataSet = (DataSet)cached.Data;
                            usedCache = true;
                        }
                        else
                        {
                            // Cache expired, remove it
                            CommandCache.Remove(cacheKey);
                        }
                    }

                    if (!usedCache)
                    {
                        await ConnectAsync();

                        if (Transaction != null)
                        {
                            Command.Transaction = Transaction;
                        }
                        else if (IsAutoTransaction)
                        {
                            tran = ((DbConnection)Command.Connection!).BeginTransaction(IsolationLevel.ReadCommitted);
                            Command.Transaction = tran;
                        }

                        LastResultDataSet = new DataSet();
                        DataAdapter.Fill(LastResultDataSet);

                        if (tran != null)
                            tran.Commit();

                        if (IsCache)
                        {
                            // Store in cache with 1-second expiration
                            CommandCache[cacheKey] = (LastResultDataSet, DateTime.Now.AddSeconds(1));
                        }
                    }

                    IsCache = IsDefaultCache;
                    CurrentPrefix = string.Empty;
                    parameters?.Clear();

                    return LastResultDataSet;
                }
                catch (Exception ex)
                {
                    errorException = ex;
                    isError = true;
                    errorCount++;

                    if (tran != null)
                    {
                        await ((DbTransaction)tran).RollbackAsync();
                    }

                    if (errorCount < RetryCount)
                        await Task.Delay(100);
                }
            } while (isError && errorCount < RetryCount);

            if (!IsDoNotLog && errorException != null)
            {
                Console.WriteLine($"Database error: {errorException.Message} - {commandType} - {commandText}");
            }

            await TransactionRollbackAsync();
            throw new InvalidOperationException("Database operation failed", errorException);
        }
        finally
        {
            _instanceSemaphore.Release();
            
            if (Transaction == null)
            {
                await CleanupConnectionAsync();
            }
        }
    }

    public virtual async Task<int> ExecuteNonQueryAsync(CommandType commandType, string commandText, DataParameterCollection? parameters = null)
    {
        Instance = this;
        LastResultNonQuery = -1;

        await _instanceSemaphore.WaitAsync();
        try
        {
            int errorCount = 0;
            bool isError;
            Exception? errorException = null;

            do
            {
                IDbTransaction? tran = null;
                try
                {
                    isError = false;
                    await ConnectAsync();

                    if (Transaction != null)
                    {
                        Command.Transaction = Transaction;
                    }
                    else if (IsAutoTransaction)
                    {
                        tran = ((DbConnection)Command.Connection!).BeginTransaction(IsolationLevel.ReadCommitted);
                        Command.Transaction = tran;
                    }

                    Command.CommandType = commandType;
                    Command.CommandText = commandText;
                    Command.Parameters.Clear();

                    if (parameters?.Count > 0)
                    {
                        if (commandType == CommandType.Text)
                            CurrentPrefix = TextPrefix;

                        SetParameter(Command, parameters);
                    }

                    if (Command is DbCommand dbCommand)
                        LastResultNonQuery = await dbCommand.ExecuteNonQueryAsync();
                    else
                        LastResultNonQuery = Command.ExecuteNonQuery();

                    CurrentPrefix = string.Empty;

                    if (tran != null)
                        await ((DbTransaction)tran).CommitAsync();

                    parameters?.Clear();

                    return LastResultNonQuery;
                }
                catch (Exception ex)
                {
                    errorException = ex;
                    isError = true;
                    errorCount++;

                    if (tran != null)
                    {
                        await ((DbTransaction)tran).RollbackAsync();
                    }

                    if (errorCount < RetryCount)
                        await Task.Delay(100);
                }
            } while (isError && errorCount < RetryCount);

            if (!IsDoNotLog && errorException != null)
            {
                Console.WriteLine($"Database error: {errorException.Message} - {commandType} - {commandText}");
            }

            await TransactionRollbackAsync();
            throw new InvalidOperationException("Database operation failed", errorException);
        }
        finally
        {
            _instanceSemaphore.Release();
            
            if (Transaction == null)
            {
                await CleanupConnectionAsync();
            }
        }
    }

    public virtual async Task<object?> ExecuteScalarAsync(CommandType commandType, string commandText, DataParameterCollection? parameters = null)
    {
        Instance = this;
        LastResultScalar = null;

        await _instanceSemaphore.WaitAsync();
        try
        {
            int errorCount = 0;
            bool isError;
            Exception? errorException = null;

            do
            {
                IDbTransaction? tran = null;
                try
                {
                    isError = false;
                    await ConnectAsync();

                    if (Transaction != null)
                    {
                        Command.Transaction = Transaction;
                    }
                    else if (IsAutoTransaction)
                    {
                        tran = ((DbConnection)Command.Connection!).BeginTransaction(IsolationLevel.ReadCommitted);
                        Command.Transaction = tran;
                    }

                    Command.CommandType = commandType;
                    Command.CommandText = commandText;
                    Command.Parameters.Clear();

                    if (parameters?.Count > 0)
                    {
                        if (commandType == CommandType.Text)
                            CurrentPrefix = TextPrefix;

                        SetParameter(Command, parameters);
                    }

                    if (Command is DbCommand dbCommand)
                        LastResultScalar = await dbCommand.ExecuteScalarAsync();
                    else
                        LastResultScalar = Command.ExecuteScalar();

                    CurrentPrefix = string.Empty;

                    if (tran != null)
                        await ((DbTransaction)tran).CommitAsync();

                    parameters?.Clear();

                    return LastResultScalar;
                }
                catch (Exception ex)
                {
                    errorException = ex;
                    isError = true;
                    errorCount++;

                    if (tran != null)
                    {
                        await ((DbTransaction)tran).RollbackAsync();
                    }

                    if (errorCount < RetryCount)
                        await Task.Delay(100);
                }
            } while (isError && errorCount < RetryCount);

            if (!IsDoNotLog && errorException != null)
            {
                Console.WriteLine($"Database error: {errorException.Message} - {commandType} - {commandText}");
            }

            await TransactionRollbackAsync();
            throw new InvalidOperationException("Database operation failed", errorException);
        }
        finally
        {
            _instanceSemaphore.Release();
            
            if (Transaction == null)
            {
                await CleanupConnectionAsync();
            }
        }
    }

    #endregion

    #region Helper methods

    public virtual async Task<bool> ConnectAsync()
    {
        if (Command.Connection == null)
            Command.Connection = Connection;

        try
        {
            await _instanceSemaphore.WaitAsync();
            try
            {
                if (Command.Connection.State != ConnectionState.Open)
                {
                    if (string.IsNullOrWhiteSpace(Command.Connection.ConnectionString))
                        Command.Connection.ConnectionString = ConnectionString;

                    if (Command.Connection is DbConnection dbConnection)
                        await dbConnection.OpenAsync();
                    else
                        Command.Connection.Open();
                }

                return Command.Connection.State == ConnectionState.Open;
            }
            finally
            {
                _instanceSemaphore.Release();
            }
        }
        catch (Exception ex)
        {
            if (!IsDoNotLog) Console.WriteLine($"Connection error: {ex.Message}");
            throw;
        }
    }

    private string DecryptConnectionString(string connectionString)
    {
        // Simplified - in a real implementation, you'd implement proper encryption/decryption
        if (IsFullEncrypted || IsEncrypted)
        {
            // Implementation would go here
            Console.WriteLine("Warning: Connection string encryption not implemented in this migration");
        }
        
        return connectionString;
    }

    protected virtual void SetParameter(IDbCommand command, DataParameterCollection parameters)
    {
        command.Parameters.Clear();

        foreach (var param in parameters)
        {
            if (IsEmptyStringEqualNull && param.Value is string str && string.IsNullOrEmpty(str))
            {
                param.Value = null;
            }

            if (command.CommandType == CommandType.Text)
            {
                string paramName = param.ParameterName.StartsWith(TextPrefix) 
                    ? param.ParameterName 
                    : $"{TextPrefix}{param.ParameterName}";

                if (command.CommandText.Contains(paramName))
                {
                    if (AllowTextParameter)
                        command.Parameters.Add(ConvertParameter(param));
                    else
                    {
                        string replacement = param.DbType switch
                        {
                            DbType.String or DbType.DateTime or DbType.Guid or DbType.AnsiString or DbType.Date 
                                => $"'{param.Value}'",
                            _ => $"{param.Value}"
                        };
                        command.CommandText = command.CommandText.Replace(paramName, replacement);
                    }
                }
            }
            else
            {
                command.Parameters.Add(ConvertParameter(param));
            }
        }

        parameters.Clear();
    }

    protected virtual string GetCommandKey()
    {
        var key = $"{ConnectionString}-{Command.CommandType}_{Command.CommandText}_";
        var parameters = string.Join("", Command.Parameters.Cast<DbParameter>()
            .Select(p => $"{p.Direction}_{p.ParameterName}_{p.Value}"));
        
        // Simple hash - in production you might want to use a proper hashing algorithm
        return (key + parameters).GetHashCode().ToString();
    }

    private async Task CleanupConnectionAsync()
    {
        bool hasOutputParams = Command.Parameters.Cast<DbParameter>()
            .Any(p => p.Direction == ParameterDirection.Output || p.Direction == ParameterDirection.InputOutput);

        if (!hasOutputParams)
        {
            Command.Dispose();
            Command = null!;
        }

        if (DataAdapter is IDisposable disposableAdapter)
            disposableAdapter.Dispose();
        DataAdapter = null!;

        if (!IsStayConnectedAlways)
        {
            if (Command?.Connection != null)
            {
                if (Command.Connection is DbConnection dbConn)
                    await dbConn.CloseAsync();
                else
                    Command.Connection.Close();
                    
                Command.Connection.Dispose();
                Command.Connection = null;
            }

            if (Connection != null)
            {
                if (Connection is DbConnection dbConn)
                    await dbConn.CloseAsync();
                else
                    Connection.Close();
                    
                Connection.Dispose();
                Connection = null!;
            }
        }
    }

    #endregion

    #region Dispose

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            // Dispose managed resources
            try
            {
                // Commit any pending transaction synchronously (blocking)
                if (Transaction != null)
                {
                    try
                    {
                        TransactionCommitAsync().GetAwaiter().GetResult();
                    }
                    catch
                    {
                        // Transaction commit failed, attempt rollback
                        try
                        {
                            TransactionRollbackAsync().GetAwaiter().GetResult();
                        }
                        catch
                        {
                            // Suppress rollback errors during dispose
                        }
                    }
                }

                // Close and dispose command connection
                if (Command?.Connection != null)
                {
                    try
                    {
                        if (Command.Connection.State == System.Data.ConnectionState.Open)
                        {
                            Command.Connection.Close();
                        }
                        Command.Connection.Dispose();
                    }
                    catch
                    {
                        // Suppress connection disposal errors
                    }
                }

                // Dispose command
                Command?.Dispose();

                // Close and dispose main connection
                if (Connection != null)
                {
                    try
                    {
                        if (Connection.State == System.Data.ConnectionState.Open)
                        {
                            Connection.Close();
                        }
                        Connection.Dispose();
                    }
                    catch
                    {
                        // Suppress connection disposal errors
                    }
                }

                // Dispose data adapter
                if (DataAdapter is IDisposable disposableAdapter)
                {
                    disposableAdapter.Dispose();
                }

                // Dispose dataset
                LastResultDataSet?.Dispose();

                // Dispose semaphore
                _instanceSemaphore?.Dispose();
            }
            catch
            {
                // Suppress all disposal errors to prevent exceptions in finalizer
            }
        }

        // Mark as disposed
        _disposed = true;
    }

    #endregion
}