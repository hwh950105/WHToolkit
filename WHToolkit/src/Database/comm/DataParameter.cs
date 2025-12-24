using System;
using System.Data;
using System.Data.Common;
using Microsoft.Extensions.Configuration;   

namespace WHToolkit.Database.Common
{
    public class DataParameter : DbParameter
    {
        public override int Size { get; set; }
        public override DbType DbType { get; set; }
        public override ParameterDirection Direction { get; set; }
        public override bool IsNullable { get; set; } = true;
        public override string ParameterName { get; set; } = string.Empty;
        public override string SourceColumn { get; set; } = string.Empty;
        public override object? Value { get; set; }
        public override bool SourceColumnNullMapping { get; set; }

        public DataParameter(ParameterDirection Direction, string ParameterName, object? Value)
        {
            this.Direction = Direction;
            this.ParameterName = ParameterName ?? throw new ArgumentNullException(nameof(ParameterName));
            this.Value = Value;
            if (Value is DateTime)
            {
                DbType = DbType.DateTime;
            }
        }

        public DataParameter(ParameterDirection Direction, string ParameterName, object? Value, int Size)
        {
            this.Direction = Direction;
            this.ParameterName = ParameterName ?? throw new ArgumentNullException(nameof(ParameterName));
            this.Value = Value;
            this.Size = Size;
            if (Value is DateTime)
            {
                DbType = DbType.DateTime;
            }
        }

        public DataParameter(ParameterDirection Direction, DbType DbType, string ParameterName, object? Value)
        {
            this.Direction = Direction;
            this.DbType = DbType;
            this.ParameterName = ParameterName ?? throw new ArgumentNullException(nameof(ParameterName));
            this.Value = Value;
        }

        public override void ResetDbType()
        {
            DbType = DbType.String;
        }
    }

    public class DataParameterCollection : List<DataParameter>
    {
        public void Add(string parameterName, object? value)
        {
            this.Add(new DataParameter(ParameterDirection.Input, parameterName, value));
        }

        public void Add(string parameterName, object? value, int size)
        {
            this.Add(new DataParameter(ParameterDirection.Input, parameterName, value, size));
        }

        public void Add(DbType dbType, string parameterName, object? value)
        {
            this.Add(new DataParameter(ParameterDirection.Input, dbType, parameterName, value));
        }

        public void Add(ParameterDirection direction, string parameterName, object? value)
        {
            this.Add(new DataParameter(direction, parameterName, value));
        }

        public void Add(ParameterDirection direction, string parameterName, object? value, int size)
        {
            this.Add(new DataParameter(direction, parameterName, value, size));
        }

        public void Add(ParameterDirection direction, DbType dbType, string parameterName, object? value)
        {
            this.Add(new DataParameter(direction, dbType, parameterName, value));
        }
    }

    /// <summary>
    /// appsettings.json 설정 관리 유틸리티 클래스
    /// </summary>
    public static class Commoncode
    
    {
        private static IConfiguration? _configuration;

        /// <summary>
        /// Configuration 인스턴스를 가져오거나 생성합니다
        /// </summary>
        private static IConfiguration GetConfiguration()
        {
            if (_configuration == null)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                _configuration = builder.Build();
            }
            return _configuration;
        }

        /// <summary>
        /// appsettings.json에서 키로 값을 가져옵니다
        /// </summary>
        /// <param name="key">가져올 키 (예: "ConnectionStrings:NpgDatabase", "AppSettings:Version")</param>
        /// <returns>키에 해당하는 값, 없으면 빈 문자열</returns>
        public static string GetConfigValue(string key)
        {
            try
            {
                var config = GetConfiguration();
                return config[key] ?? string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetConfigValue Error for key '{key}': {ex.Message}");
                return string.Empty;
            }
        }

        /// <summary>
        /// appsettings.json의 ConnectionStrings 섹션에서 값을 가져옵니다
        /// </summary>
        /// <param name="connectionName">ConnectionString 이름</param>
        /// <returns>연결 문자열, 없으면 빈 문자열</returns>
        public static string GetConnectionString(string connectionName)
        {
            try
            {
                var config = GetConfiguration();
                return config.GetConnectionString(connectionName) ?? string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetConnectionString Error for '{connectionName}': {ex.Message}");
                return string.Empty;
            }
        }

        /// <summary>
        /// appsettings.json에서 특정 섹션을 객체로 바인딩합니다
        /// </summary>
        /// <typeparam name="T">바인딩할 객체 타입</typeparam>
        /// <param name="sectionName">섹션 이름</param>
        /// <returns>바인딩된 객체, 실패 시 기본 인스턴스</returns>
        public static T GetSection<T>(string sectionName) where T : class, new()
        {
            try
            {
                var config = GetConfiguration();
                return config.GetSection(sectionName).Get<T>() ?? new T();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetSection Error for '{sectionName}': {ex.Message}");
                return new T();
            }
        }

        /// <summary>
        /// appsettings.json에서 특정 섹션의 모든 키-값을 Dictionary로 가져옵니다
        /// </summary>
        /// <param name="sectionName">섹션 이름</param>
        /// <returns>키-값 Dictionary</returns>
        public static IDictionary<string, string> GetSectionDictionary(string sectionName)
        {
            try
            {
                var config = GetConfiguration();
                var section = config.GetSection(sectionName);
                var result = new Dictionary<string, string>();

                foreach (var child in section.GetChildren())
                {
                    result[child.Key] = child.Value ?? string.Empty;
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetSectionDictionary Error for '{sectionName}': {ex.Message}");
                return new Dictionary<string, string>();
            }
        }

        /// <summary>
        /// Configuration을 리로드합니다
        /// </summary>
        public static void ReloadConfiguration()
        {
            _configuration = null;
        }
    }
}
