namespace WHToolkit.Database;

/// <summary>
/// 데이터베이스 작업에 필요한 상수 및 매핑 정보를 관리하는 클래스입니다.
/// </summary>
internal class ConstantsDefine
{
    /// <summary>
    /// 문자열 프로바이더 이름과 ProviderKind 열거형 간의 매핑 딕셔너리
    /// </summary>
    public Dictionary<string, ProviderKind> ProviderMapping { get; } = new();

    /// <summary>
    /// 암호화가 필요한 연결 문자열 속성 목록
    /// </summary>
    public List<string> EncryptConnectionAttribute { get; } = new();

    /// <summary>
    /// ConstantsDefine의 새 인스턴스를 초기화합니다.
    /// </summary>
    public ConstantsDefine()
    {
        InitializeProviderMapping();
        InitializeEncryptConnectionAttributes();
    }

    /// <summary>
    /// 프로바이더 매핑을 초기화합니다.
    /// </summary>
    private void InitializeProviderMapping()
    {
        ProviderMapping.Add("MSSQL", ProviderKind.MSSQL);
        ProviderMapping.Add("SQLOLEDB.1", ProviderKind.MSSQL);
        ProviderMapping.Add("MYSQL", ProviderKind.MySQL);
        ProviderMapping.Add("ORACLE", ProviderKind.Oracle);
        ProviderMapping.Add("POSTGRESQL", ProviderKind.PostgreSQL);
    }

    /// <summary>
    /// 암호화할 연결 문자열 속성을 초기화합니다.
    /// </summary>
    private void InitializeEncryptConnectionAttributes()
    {
        // Note: All entries must be in lowercase
        EncryptConnectionAttribute.AddRange(new[]
        {
            "server",
            "database",
            "uid",
            "pwd",
            "data source",
            "initial catalog",
            "user id",
            "password"
        });
    }
}