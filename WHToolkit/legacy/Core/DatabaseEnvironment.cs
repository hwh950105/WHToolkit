namespace WHToolkit.Database;

/// <summary>
/// 데이터베이스 환경 구성 정보를 관리하는 클래스입니다.
/// </summary>
internal class DatabaseEnvironment
{
    /// <summary>
    /// 데이터베이스 프로바이더 종류
    /// </summary>
    public ProviderKind Provider { get; set; }

    /// <summary>
    /// 데이터베이스 연결 문자열
    /// </summary>
    public string ConnectionString { get; set; } = string.Empty;

    /// <summary>
    /// 연결 문자열 암호화 여부
    /// </summary>
    public bool IsEncrypted { get; set; }

    /// <summary>
    /// 전체 연결 문자열 암호화 여부
    /// </summary>
    public bool IsFullEncrypted { get; set; }

    /// <summary>
    /// 명령 타임아웃 시간 (초 단위, 기본값: 30)
    /// </summary>
    public int CommandTimeout { get; set; } = 30;
}