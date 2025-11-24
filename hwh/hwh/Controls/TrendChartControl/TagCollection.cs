using System.Collections;
using System.ComponentModel;

namespace hwh.Controls.TrendChartControl;

/// <summary>
/// Tag 컬렉션
/// </summary>
public class TagCollection : IEnumerable<TagItem>, INotifyPropertyChanged
{
    private readonly List<TagItem> _tags = new();

    public event PropertyChangedEventHandler? PropertyChanged;

    public int Count => _tags.Count;

    /// <summary>
    /// 모든 Tag의 최소 Timestamp
    /// </summary>
    public long TimestampMin
    {
        get
        {
            if (_tags.Count == 0) return 0;
            return _tags.Min(tag => tag.PointValues.TimestampMin);
        }
    }

    /// <summary>
    /// 모든 Tag의 최대 Timestamp
    /// </summary>
    public long TimestampMax
    {
        get
        {
            if (_tags.Count == 0) return 0;
            return _tags.Max(tag => tag.PointValues.TimestampMax);
        }
    }

    /// <summary>
    /// Tag 이름으로 검색
    /// </summary>
    public TagItem? this[string tagName]
    {
        get => _tags.FirstOrDefault(tag => tag.Name == tagName);
    }

    /// <summary>
    /// 인덱스로 접근
    /// </summary>
    public TagItem this[int index]
    {
        get => _tags[index];
    }

    /// <summary>
    /// Tag 추가
    /// </summary>
    public void Add(string tagName)
    {
        Add(new TagItem(tagName));
    }

    /// <summary>
    /// Tag 추가
    /// </summary>
    public void Add(TagItem tagItem)
    {
        if (tagItem == null)
            throw new ArgumentNullException(nameof(tagItem));

        if (this[tagItem.Name] != null)
            throw new InvalidOperationException($"같은 이름의 Tag가 이미 존재합니다: {tagItem.Name}");

        tagItem.PropertyChanged += TagItem_PropertyChanged;
        tagItem.No = _tags.Count + 1;

        _tags.Add(tagItem);
        OnPropertyChanged(nameof(Count));
    }

    /// <summary>
    /// Tag 제거
    /// </summary>
    public bool Remove(string tagName)
    {
        var tag = this[tagName];
        if (tag == null) return false;

        tag.PropertyChanged -= TagItem_PropertyChanged;
        var removed = _tags.Remove(tag);

        if (removed)
        {
            // 순번 재정렬
            for (int i = 0; i < _tags.Count; i++)
            {
                _tags[i].No = i + 1;
            }
            OnPropertyChanged(nameof(Count));
        }

        return removed;
    }

    /// <summary>
    /// 모든 Tag 제거
    /// </summary>
    public void Clear()
    {
        foreach (var tag in _tags)
        {
            tag.PropertyChanged -= TagItem_PropertyChanged;
        }

        _tags.Clear();
        OnPropertyChanged(nameof(Count));
    }

    private void TagItem_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        PropertyChanged?.Invoke(sender, e);
    }

    public IEnumerator<TagItem> GetEnumerator()
    {
        return _tags.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

