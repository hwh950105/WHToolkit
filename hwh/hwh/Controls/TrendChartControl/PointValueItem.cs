namespace hwh.Controls.TrendChartControl;

/// <summary>
/// 시계열 데이터 포인트 (시간-값 쌍)
/// </summary>
public class PointValueItem
{
    /// <summary>
    /// Unix Timestamp (초 단위)
    /// </summary>
    public long UnixTimestamp { get; set; }

    /// <summary>
    /// 값
    /// </summary>
    public double Value { get; set; }

    public PointValueItem() { }

    public PointValueItem(long unixTimestamp, double value)
    {
        UnixTimestamp = unixTimestamp;
        Value = value;
    }

    public PointValueItem(DateTime dateTime, double value)
    {
        UnixTimestamp = new DateTimeOffset(dateTime).ToUnixTimeSeconds();
        Value = value;
    }

    /// <summary>
    /// DateTime으로 변환
    /// </summary>
    public DateTime ToDateTime()
    {
        return DateTimeOffset.FromUnixTimeSeconds(UnixTimestamp).LocalDateTime;
    }
}

