using System.ComponentModel;

namespace hwh.Controls.TrendChartControl;

/// <summary>
/// 시계열 차트의 Tag(시리즈) 정보
/// </summary>
public class TagItem : INotifyPropertyChanged
{
    private string? _alias;
    private double? _currentValue;
    private bool _visible = true;

    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Tag 순번
    /// </summary>
    public int No { get; set; }

    /// <summary>
    /// Tag 이름
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// 표시 이름 (Alias)
    /// </summary>
    public string? Alias
    {
        get => _alias;
        set
        {
            if (_alias != value)
            {
                _alias = value;
                OnPropertyChanged(nameof(Alias));
            }
        }
    }

    /// <summary>
    /// 표시할 이름 (Alias가 있으면 Alias, 없으면 Name)
    /// </summary>
    public string DisplayName => string.IsNullOrWhiteSpace(Alias) ? Name : Alias;

    /// <summary>
    /// 최소값
    /// </summary>
    public double? MinValue { get; set; }

    /// <summary>
    /// 최대값
    /// </summary>
    public double? MaxValue { get; set; }

    /// <summary>
    /// 현재값
    /// </summary>
    public double? CurrentValue
    {
        get => _currentValue;
        set
        {
            if (_currentValue != value)
            {
                _currentValue = value;
                OnPropertyChanged(nameof(CurrentValue));
            }
        }
    }

    /// <summary>
    /// 차트에 표시 여부
    /// </summary>
    public bool Visible
    {
        get => _visible;
        set
        {
            if (_visible != value)
            {
                _visible = value;
                OnPropertyChanged(nameof(Visible));
            }
        }
    }

    /// <summary>
    /// 시계열 데이터 포인트 컬렉션
    /// </summary>
    public PointValueCollection PointValues { get; } = new();

    /// <summary>
    /// 추가 속성
    /// </summary>
    public Dictionary<string, object> Attributes { get; } = new();

    public TagItem(string tagName)
    {
        Name = tagName ?? throw new ArgumentNullException(nameof(tagName));
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

