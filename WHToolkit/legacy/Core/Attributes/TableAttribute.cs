namespace WHToolkit.Database.Attributes;

/// <summary>
/// 클래스와 데이터베이스 테이블의 매핑을 지정하는 특성입니다.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class TableAttribute : Attribute
{
    /// <summary>
    /// 데이터베이스 테이블 이름
    /// </summary>
    public string TableName { get; }

    /// <summary>
    /// 데이터베이스 스키마 이름
    /// </summary>
    public string? Schema { get; set; }

    /// <summary>
    /// TableAttribute의 새 인스턴스를 초기화합니다.
    /// </summary>
    /// <param name="tableName">데이터베이스 테이블 이름</param>
    public TableAttribute(string tableName)
    {
        TableName = tableName;
    }
}