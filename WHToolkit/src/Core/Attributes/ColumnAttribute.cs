namespace WHToolkit.Database.Attributes;

/// <summary>
/// 속성과 데이터베이스 컬럼의 매핑을 지정하는 특성입니다.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class ColumnAttribute : Attribute
{
    /// <summary>
    /// 데이터베이스 컬럼 이름
    /// </summary>
    public string ColumnName { get; }

    /// <summary>
    /// NULL 허용 여부
    /// </summary>
    public bool IsNullable { get; set; } = true;

    /// <summary>
    /// 데이터베이스 고유 데이터 타입
    /// </summary>
    public string? DataType { get; set; }

    /// <summary>
    /// ColumnAttribute의 새 인스턴스를 초기화합니다.
    /// </summary>
    /// <param name="columnName">데이터베이스 컬럼 이름</param>
    public ColumnAttribute(string columnName)
    {
        ColumnName = columnName;
    }
}