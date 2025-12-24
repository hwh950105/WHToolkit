namespace WHToolkit.Database.Attributes;

/// <summary>
/// 속성을 데이터베이스 매핑에서 제외하도록 지정하는 특성입니다.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class IgnoreAttribute : Attribute
{
}