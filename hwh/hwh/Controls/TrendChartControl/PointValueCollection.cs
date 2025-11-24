using System.Collections;

namespace hwh.Controls.TrendChartControl;

/// <summary>
/// 시계열 데이터 포인트 컬렉션
/// </summary>
public class PointValueCollection : IEnumerable<PointValueItem>
{
    private readonly Dictionary<long, double> _data = new();
    private readonly HashSet<long> _updatedPoints = new();

    public long TimestampMin { get; private set; } = long.MaxValue;
    public long TimestampMax { get; private set; } = long.MinValue;
    public double ValueMin { get; private set; } = double.MaxValue;
    public double ValueMax { get; private set; } = double.MinValue;

    public int Count => _data.Count;

    public double this[long timestamp]
    {
        get => _data.TryGetValue(timestamp, out var value) ? value : double.NaN;
    }

    /// <summary>
    /// 데이터 포인트 추가
    /// </summary>
    public void Add(PointValueItem item)
    {
        Add(item.UnixTimestamp, item.Value);
    }

    /// <summary>
    /// 데이터 포인트 추가
    /// </summary>
    public void Add(DateTime dateTime, double value)
    {
        long timestamp = new DateTimeOffset(dateTime).ToUnixTimeSeconds();
        Add(timestamp, value);
    }

    /// <summary>
    /// 데이터 포인트 추가
    /// </summary>
    public void Add(long unixTimestamp, double value)
    {
        if (_data.ContainsKey(unixTimestamp))
        {
            _data[unixTimestamp] = value;
        }
        else
        {
            _data.Add(unixTimestamp, value);

            TimestampMin = Math.Min(TimestampMin, unixTimestamp);
            TimestampMax = Math.Max(TimestampMax, unixTimestamp);

            if (!double.IsNaN(value) && !double.IsInfinity(value))
            {
                ValueMin = Math.Min(ValueMin, value);
                ValueMax = Math.Max(ValueMax, value);
            }
        }

        _updatedPoints.Add(unixTimestamp);
    }

    /// <summary>
    /// 모든 데이터 포인트 반환
    /// </summary>
    public List<PointValueItem> GetAllPoints()
    {
        var result = new List<PointValueItem>(_data.Count);
        foreach (var kvp in _data.OrderBy(x => x.Key))
        {
            result.Add(new PointValueItem(kvp.Key, kvp.Value));
        }
        return result;
    }

    /// <summary>
    /// 업데이트된 데이터 포인트만 반환 (마지막 호출 이후)
    /// </summary>
    public List<PointValueItem> GetUpdatedPoints()
    {
        if (_updatedPoints.Count == 0)
            return new List<PointValueItem>();

        var result = new List<PointValueItem>(_updatedPoints.Count);
        var sortedTimestamps = _updatedPoints.OrderBy(x => x).ToList();

        foreach (var timestamp in sortedTimestamps)
        {
            if (_data.TryGetValue(timestamp, out var value))
            {
                result.Add(new PointValueItem(timestamp, value));
            }
        }

        _updatedPoints.Clear();
        return result;
    }

    /// <summary>
    /// ScottPlot용 좌표 배열 생성
    /// </summary>
    public (double[] xs, double[] ys) ToScottPlotArrays()
    {
        var points = GetAllPoints();
        var xs = new double[points.Count];
        var ys = new double[points.Count];

        for (int i = 0; i < points.Count; i++)
        {
            xs[i] = points[i].ToDateTime().ToOADate(); // DateTime을 OLE Automation 날짜로 변환
            ys[i] = points[i].Value;
        }

        return (xs, ys);
    }

    /// <summary>
    /// 모든 데이터 삭제
    /// </summary>
    public void Clear()
    {
        _data.Clear();
        _updatedPoints.Clear();
        TimestampMin = long.MaxValue;
        TimestampMax = long.MinValue;
        ValueMin = double.MaxValue;
        ValueMax = double.MinValue;
    }

    public IEnumerator<PointValueItem> GetEnumerator()
    {
        return GetAllPoints().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

