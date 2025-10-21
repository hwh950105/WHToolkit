namespace WHToolkit.Database;

/// <summary>
/// 지원되는 데이터베이스 프로바이더 종류를 정의하는 열거형입니다.
/// </summary>
public enum ProviderKind
{
    /// <summary>
    /// 선택된 프로바이더 없음
    /// </summary>
    None,

    /// <summary>
    /// Microsoft SQL Server
    /// </summary>
    MSSQL,

    /// <summary>
    /// Oracle Database
    /// </summary>
    Oracle,

    /// <summary>
    /// MySQL Database
    /// </summary>
    MySQL,

    /// <summary>
    /// PostgreSQL Database
    /// </summary>
    PostgreSQL
}