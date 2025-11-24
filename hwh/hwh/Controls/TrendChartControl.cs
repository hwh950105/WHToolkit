using System.ComponentModel;
using System.Windows.Forms;
using ScottPlot;
using hwh.Controls.TrendChartControl;

namespace hwh.Controls;

/// <summary>
/// ScottPlot 기반 시계열 차트 컨트롤
/// </summary>
public partial class ScottPlotTrendChart : UserControl
{
    private System.Windows.Forms.Timer? _autoUpdateTimer;
    private bool _isInitialized = false;
    private bool _isUpdatingProgrammatically = false;  // 프로그래밍 방식의 업데이트 플래그
    private readonly Dictionary<string, ScottPlot.Plottables.Scatter> _scatterPlots = new();
    private ITagDataProvider? _dataProvider;
    private readonly ScottPlot.Color[] _defaultColors = new[]
    {
        ScottPlot.Color.FromHex("#1f77b4"),
        ScottPlot.Color.FromHex("#ff7f0e"),
        ScottPlot.Color.FromHex("#2ca02c"),
        ScottPlot.Color.FromHex("#d62728"),
        ScottPlot.Color.FromHex("#9467bd"),
        ScottPlot.Color.FromHex("#8c564b"),
        ScottPlot.Color.FromHex("#e377c2"),
        ScottPlot.Color.FromHex("#7f7f7f"),
        ScottPlot.Color.FromHex("#bcbd22"),
        ScottPlot.Color.FromHex("#17becf")
    };

    /// <summary>
    /// 데이터 제공자 (필수 설정)
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ITagDataProvider? DataProvider
    {
        get => _dataProvider;
        set => _dataProvider = value;
    }

    /// <summary>
    /// Tag 컬렉션
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public TagCollection Tags { get; } = new();

    /// <summary>
    /// 자동 업데이트 주기 (밀리초)
    /// </summary>
    [Category("Behavior")]
    [Description("자동 업데이트 주기 (밀리초)")]
    [DefaultValue(200)]
    public int AutoUpdateInterval { get; set; } = 200;

    /// <summary>
    /// 실시간 모드 시작 오프셋 (분)
    /// </summary>
    [Category("Behavior")]
    [Description("실시간 모드 시작 오프셋 (분)")]
    [DefaultValue(1)]
    public int RealtimeStartOffsetMinutes { get; set; } = 1;

    /// <summary>
    /// 실시간 모드 윈도우 크기 (분)
    /// </summary>
    [Category("Behavior")]
    [Description("실시간 모드 윈도우 크기 (분)")]
    [DefaultValue(10)]
    public int RealtimeWindowMinutes { get; set; } = 10;

    /// <summary>
    /// Tag 정보 그리드 표시 여부
    /// </summary>
    [Category("Appearance")]
    [Description("Tag 정보 그리드 표시 여부")]
    [DefaultValue(true)]
    public bool ShowTagGrid
    {
        get => panelBottom.Visible;
        set => panelBottom.Visible = value;
    }

    /// <summary>
    /// 시간 범위 버튼 표시 여부
    /// </summary>
    [Category("Appearance")]
    [Description("시간 범위 버튼 표시 여부")]
    [DefaultValue(true)]
    public bool ShowRangeButtons
    {
        get => groupBox5.Visible && groupBox4.Visible && groupBox3.Visible && groupBox2.Visible;
        set
        {
            groupBox5.Visible = value;
            groupBox4.Visible = value;
            groupBox3.Visible = value;
            groupBox2.Visible = value;
        }
    }

    /// <summary>
    /// 데이터 요청 이벤트 (시간 범위 변경 시 발생)
    /// </summary>
    public event EventHandler<TimeRangeEventArgs>? TimeRangeChanged;

    public ScottPlotTrendChart()
    {
        InitializeComponent();
        
        InitializeChart();
        InitializeEvents();

        // 기본 시간 범위 설정
        dtpStart.Value = DateTime.Now.AddMinutes(-RealtimeStartOffsetMinutes);
        dtpEnd.Value = DateTime.Now.AddMinutes(RealtimeWindowMinutes);

        // 빈 상태로 시작 (개발자가 Initialize 호출할 때까지 대기)
        // DataProvider 설정 후 Initialize() 메서드를 호출해야 함
    }

    private void InitializeChart()
    {
        // ScottPlot 초기화
        formsPlot1.Plot.Clear();

        // 차트 스타일 설정
        formsPlot1.Plot.Axes.Title.Label.FontSize = 18;
        formsPlot1.Plot.Axes.Title.Label.Bold = true;
        formsPlot1.Plot.Axes.Title.Label.FontName = "맑은 고딕";

        // X축 설정 (시간)
        formsPlot1.Plot.Axes.DateTimeTicksBottom();
        formsPlot1.Plot.Axes.Bottom.Label.Text = "시간";
        formsPlot1.Plot.Axes.Bottom.Label.FontSize = 14;
        formsPlot1.Plot.Axes.Bottom.Label.FontName = "맑은 고딕";
        formsPlot1.Plot.Axes.Bottom.Label.Bold = true;

        // Y축 설정
        formsPlot1.Plot.Axes.Left.Label.Text = "값";
        formsPlot1.Plot.Axes.Left.Label.FontSize = 14;
        formsPlot1.Plot.Axes.Left.Label.FontName = "맑은 고딕";
        formsPlot1.Plot.Axes.Left.Label.Bold = true;

        // 폰트 설정
        formsPlot1.Plot.Axes.Bottom.TickLabelStyle.FontName = "맑은 고딕";
        formsPlot1.Plot.Axes.Bottom.TickLabelStyle.FontSize = 11;
        formsPlot1.Plot.Axes.Left.TickLabelStyle.FontName = "맑은 고딕";
        formsPlot1.Plot.Axes.Left.TickLabelStyle.FontSize = 11;

        // 그리드 설정
        formsPlot1.Plot.Grid.MajorLineColor = ScottPlot.Color.FromHex("#EEEEEE");
        formsPlot1.Plot.Grid.MajorLineWidth = 1;

        // 범례 설정
        formsPlot1.Plot.Legend.FontSize = 12;
        formsPlot1.Plot.Legend.FontName = "맑은 고딕";
        formsPlot1.Plot.Legend.IsVisible = true;
        formsPlot1.Plot.Legend.Alignment = Alignment.UpperRight;

        // 줌/패닝은 FormsPlot에서 기본으로 활성화되어 있음

        _isInitialized = true;
    }

    private void InitializeEvents()
    {
        Tags.PropertyChanged += Tags_PropertyChanged;
        
        // DataGridView 바인딩
        dgvTags.AutoGenerateColumns = false;
        dgvTags.CellValueChanged += DgvTags_CellValueChanged;
    }

    private void Tags_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        // Tag가 변경되면 그리드 업데이트
        RefreshTagGrid();
    }

    private void DgvTags_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

        // Visible 컬럼 변경 시 차트 업데이트
        if (dgvTags.Columns[e.ColumnIndex].Name == "colVisible")
        {
            var tagName = dgvTags.Rows[e.RowIndex].Cells["colName"].Value?.ToString();
            if (!string.IsNullOrEmpty(tagName))
            {
                var tag = Tags[tagName];
                if (tag != null)
                {
                    var visible = (bool)(dgvTags.Rows[e.RowIndex].Cells["colVisible"].Value ?? false);
                    tag.Visible = visible;
                    RefreshChart();
                }
            }
        }
    }

    /// <summary>
    /// 차트 제목 설정
    /// </summary>
    public void SetTitle(string title)
    {
        formsPlot1.Plot.Title(title);
    }

    /// <summary>
    /// 차트 새로고침 (모든 데이터 다시 그리기)
    /// </summary>
    public new void Refresh()
    {
        if (!_isInitialized) return;

        // 기존 플롯 제거
        formsPlot1.Plot.Clear();
        _scatterPlots.Clear();

        int colorIndex = 0;

        // 각 Tag에 대해 플롯 생성
        foreach (var tag in Tags)
        {
            if (!tag.Visible || tag.PointValues.Count == 0)
                continue;

            var (xs, ys) = tag.PointValues.ToScottPlotArrays();

            if (xs.Length == 0) continue;

            var scatter = formsPlot1.Plot.Add.Scatter(xs, ys);
            scatter.LineWidth = 2;
            scatter.MarkerSize = 0;
            scatter.Color = _defaultColors[colorIndex % _defaultColors.Length];
            scatter.LegendText = tag.DisplayName;

            _scatterPlots[tag.Name] = scatter;

            // 통계 업데이트
            tag.MinValue = tag.PointValues.ValueMin;
            tag.MaxValue = tag.PointValues.ValueMax;
            
            if (tag.PointValues.Count > 0)
            {
                var lastTimestamp = tag.PointValues.TimestampMax;
                tag.CurrentValue = tag.PointValues[lastTimestamp];
            }

            colorIndex++;
        }

        // 축 범위 설정
        SetAxisLimits(dtpStart.Value, dtpEnd.Value);

        // Y축 자동 조정 (데이터에 맞춰 조정)
        if (Tags.Any(t => t.Visible && t.PointValues.Count > 0))
        {
            formsPlot1.Plot.Axes.AutoScale();
        }

        // 차트 업데이트
        formsPlot1.Refresh();

        // 그리드 업데이트
        RefreshTagGrid();

        base.Refresh();
    }

    /// <summary>
    /// 차트만 업데이트 (그리드는 업데이트하지 않음)
    /// </summary>
    private void RefreshChart()
    {
        if (!_isInitialized) return;

        formsPlot1.Plot.Clear();
        _scatterPlots.Clear();

        int colorIndex = 0;

        foreach (var tag in Tags)
        {
            if (!tag.Visible || tag.PointValues.Count == 0)
                continue;

            var (xs, ys) = tag.PointValues.ToScottPlotArrays();

            if (xs.Length == 0) continue;

            var scatter = formsPlot1.Plot.Add.Scatter(xs, ys);
            scatter.LineWidth = 2;
            scatter.MarkerSize = 0;
            scatter.Color = _defaultColors[colorIndex % _defaultColors.Length];
            scatter.LegendText = tag.DisplayName;

            _scatterPlots[tag.Name] = scatter;

            colorIndex++;
        }

        SetAxisLimits(dtpStart.Value, dtpEnd.Value);
        
        // Y축 자동 조정 (데이터에 맞춰 조정)
        if (Tags.Any(t => t.Visible && t.PointValues.Count > 0))
        {
            formsPlot1.Plot.Axes.AutoScale();
        }
        
        formsPlot1.Refresh();
    }

    /// <summary>
    /// Tag 그리드 업데이트
    /// </summary>
    private void RefreshTagGrid()
    {
        dgvTags.DataSource = null;
        dgvTags.DataSource = Tags.ToList();
    }

    /// <summary>
    /// 축 범위 설정
    /// </summary>
    private void SetAxisLimits(DateTime start, DateTime end)
    {
        double xMin = start.ToOADate();
        double xMax = end.ToOADate();

        formsPlot1.Plot.Axes.SetLimitsX(xMin, xMax);
    }

    /// <summary>
    /// 시간 범위 설정
    /// </summary>
    public void SetTimeRange(DateTime start, DateTime end)
    {
        dtpStart.Value = start;
        dtpEnd.Value = end;
        SetAxisLimits(start, end);
        
        // 이벤트 발생
        TimeRangeChanged?.Invoke(this, new TimeRangeEventArgs(start, end));
    }

    /// <summary>
    /// 차트 초기화 (개발자가 명시적으로 호출)
    /// </summary>
    public void Initialize(ChartInitMode mode = ChartInitMode.Empty)
    {
        switch (mode)
        {
            case ChartInitMode.Realtime:
                InitializeRealtimeMode();
                chkAutoUpdate.Checked = true;
                break;

            case ChartInitMode.Historical:
                InitializeHistoricalMode();
                break;

            case ChartInitMode.Empty:
            default:
                // 빈 차트 상태 유지
                Tags.Clear();
                RefreshChart();
                break;
        }
    }

    /// <summary>
    /// 태그 추가
    /// </summary>
    public void AddTag(string tagName, string? displayName = null)
    {
        if (_dataProvider == null)
            throw new InvalidOperationException("DataProvider가 설정되지 않았습니다. 먼저 DataProvider 속성을 설정하세요.");

        var metadata = _dataProvider.GetTagMetadata(tagName);

        var tag = new TagItem(tagName)
        {
            Alias = displayName ?? metadata.DisplayName,
            Visible = true
        };

        Tags.Add(tag);
        RefreshTagGrid();
    }

    /// <summary>
    /// 여러 태그 한번에 추가
    /// </summary>
    public void AddTags(params string[] tagNames)
    {
        foreach (var tagName in tagNames)
        {
            AddTag(tagName);
        }
    }

    /// <summary>
    /// 태그 제거
    /// </summary>
    public void RemoveTag(string tagName)
    {
        if (Tags.Remove(tagName))
        {
            RefreshChart();
        }
    }

    /// <summary>
    /// 모든 태그 제거
    /// </summary>
    public void ClearTags()
    {
        Tags.Clear();
        RefreshChart();
    }

    /// <summary>
    /// 시간 범위에 맞는 이력 데이터 로드
    /// </summary>
    public void LoadData(DateTime startTime, DateTime endTime)
    {
        if (_dataProvider == null)
            throw new InvalidOperationException("DataProvider가 설정되지 않았습니다. 먼저 DataProvider 속성을 설정하세요.");

        foreach (var tag in Tags)
        {
            tag.PointValues.Clear();

            var data = _dataProvider.GetHistoricalData(tag.Name, startTime, endTime);

            foreach (var point in data)
            {
                tag.PointValues.Add(point.Timestamp, point.Value);
            }
        }

        SetTimeRange(startTime, endTime);
        Refresh();
    }

    /// <summary>
    /// 현재 설정된 시간 범위로 데이터 로드
    /// </summary>
    public void LoadData()
    {
        LoadData(dtpStart.Value, dtpEnd.Value);
    }

    /// <summary>
    /// 단일 데이터 포인트 추가 (실시간용)
    /// </summary>
    public void AddDataPoint(string tagName, DateTime timestamp, double value)
    {
        var tag = Tags[tagName];
        if (tag != null)
        {
            tag.PointValues.Add(timestamp, value);
        }
    }

    /// <summary>
    /// 실시간 모드 초기화 (빈 차트로 시작)
    /// </summary>
    private void InitializeRealtimeMode()
    {
        // 차트 완전히 클리어
        formsPlot1.Plot.Clear();
        _scatterPlots.Clear();

        // 차트 제목 설정
        SetTitle("실시간 센서 모니터링 (실시간 모드)");

        // 시간 범위 설정
        dtpStart.Value = DateTime.Now.AddMinutes(-RealtimeStartOffsetMinutes);
        dtpEnd.Value = DateTime.Now.AddMinutes(RealtimeWindowMinutes);
        
        // X축 범위를 현재 시간으로 명확하게 설정
        double xMin = dtpStart.Value.ToOADate();
        double xMax = dtpEnd.Value.ToOADate();
        formsPlot1.Plot.Axes.SetLimitsX(xMin, xMax);
        
        // Y축은 자동 조정
        formsPlot1.Plot.Axes.AutoScale();
        
        // 차트 그리기
        formsPlot1.Refresh();
        
        // 그리드 업데이트
        RefreshTagGrid();
    }

    /// <summary>
    /// 이력 모드 초기화
    /// </summary>
    private void InitializeHistoricalMode()
    {
        // 차트 완전히 클리어
        formsPlot1.Plot.Clear();
        _scatterPlots.Clear();

        // 차트 제목 설정
        SetTitle("센서 모니터링 (이력 모드)");

        // 시간 범위 설정 (최근 1시간)
        dtpEnd.Value = DateTime.Now;
        dtpStart.Value = DateTime.Now.AddHours(-1);

        // X축 범위 설정
        SetAxisLimits(dtpStart.Value, dtpEnd.Value);

        // 차트 그리기
        formsPlot1.Refresh();

        // 그리드 업데이트
        RefreshTagGrid();
    }

    /// <summary>
    /// DB에서 데이터 로드 (하위 호환성을 위해 유지, 내부적으로 LoadData 호출)
    /// </summary>
    [Obsolete("LoadData() 메서드를 사용하세요.")]
    private void LoadDataFromDatabase()
    {
        if (Tags.Count > 0)
        {
            LoadData();
        }
    }

    /// <summary>
    /// 샘플 데이터 생성 (하위 호환성을 위해 유지)
    /// </summary>
    [Obsolete("LoadData() 메서드를 사용하세요.")]
    private void GenerateSampleData()
    {
        if (Tags.Count > 0)
        {
            LoadData();
        }
    }

    #region Event Handlers

    private void BtnRange_Click(object? sender, EventArgs e)
    {
        if (sender is not Button btn) return;

        DateTime endTime = DateTime.Now;
        DateTime startTime;

        // 실시간 버튼 클릭 시
        if (btn.Name == "btnRealtime")
        {
            // 실시간 모드: 1분 전 ~ 미래 10분으로 설정
            dtpStart.Value = DateTime.Now.AddMinutes(-1);
            dtpEnd.Value = DateTime.Now.AddMinutes(10);
            
            // 자동 업데이트 활성화
            if (!chkAutoUpdate.Checked)
            {
                chkAutoUpdate.Checked = true;
            }
            
            // 실시간 모드로 다시 초기화 (과거 데이터 없이 빈 차트로 시작)
            InitializeRealtimeMode();
            return;
        }

        // 버튼 이름으로 범위 결정
        startTime = btn.Name switch
        {
            "btnRange10m" => endTime.AddMinutes(-10),
            "btnRange30m" => endTime.AddMinutes(-30),
            "btnRange60m" => endTime.AddHours(-1),
            "btnRange3h" => endTime.AddHours(-3),
            "btnRange6h" => endTime.AddHours(-6),
            "btnRange12h" => endTime.AddHours(-12),
            "btnRange1d" => endTime.AddDays(-1),
            "btnRange7d" => endTime.AddDays(-7),
            "btnRange30d" => endTime.AddDays(-30),
            "btnRange3mo" => endTime.AddMonths(-3),
            "btnRange6mo" => endTime.AddMonths(-6),
            "btnRange12mo" => endTime.AddYears(-1),
            _ => endTime.AddHours(-1)
        };

        SetTimeRange(startTime, endTime);
        
        // 과거 데이터 로드
        if (_dataProvider != null && Tags.Count > 0)
        {
            LoadData();
        }
    }

    private void DtpStart_ValueChanged(object? sender, EventArgs e)
    {
        if (_isUpdatingProgrammatically) return;  // 프로그래밍 방식의 업데이트는 무시

        if (dtpStart.Value >= dtpEnd.Value)
        {
            dtpEnd.Value = dtpStart.Value.AddMinutes(10);
        }

        SetAxisLimits(dtpStart.Value, dtpEnd.Value);
        TimeRangeChanged?.Invoke(this, new TimeRangeEventArgs(dtpStart.Value, dtpEnd.Value));
    }

    private void DtpEnd_ValueChanged(object? sender, EventArgs e)
    {
        if (_isUpdatingProgrammatically) return;  // 프로그래밍 방식의 업데이트는 무시

        if (dtpEnd.Value <= dtpStart.Value)
        {
            dtpStart.Value = dtpEnd.Value.AddMinutes(-10);
        }

        SetAxisLimits(dtpStart.Value, dtpEnd.Value);
        TimeRangeChanged?.Invoke(this, new TimeRangeEventArgs(dtpStart.Value, dtpEnd.Value));
    }

    private void BtnRefresh_Click(object? sender, EventArgs e)
    {
        // 이벤트가 연결되어 있으면 외부에서 데이터 로드
        if (TimeRangeChanged != null && TimeRangeChanged.GetInvocationList().Length > 0)
        {
            TimeRangeChanged.Invoke(this, new TimeRangeEventArgs(dtpStart.Value, dtpEnd.Value));
        }
        else
        {
            // 이벤트가 없으면 샘플 데이터 재생성
            RegenerateSampleData();
        }
    }

    /// <summary>
    /// 샘플 데이터 재생성 (현재 시간 범위 기준) - 데이터 다시 로드
    /// </summary>
    private void RegenerateSampleData()
    {
        if (_dataProvider == null || Tags.Count == 0)
        {
            return;
        }

        // 기존 데이터 클리어
        foreach (var tag in Tags)
        {
            tag.PointValues.Clear();
        }

        var startTime = dtpStart.Value;
        var endTime = dtpEnd.Value;

        // 각 Tag에 대해 DataProvider에서 데이터 가져오기
        foreach (var tag in Tags)
        {
            try
            {
                var data = _dataProvider.GetHistoricalData(tag.Name, startTime, endTime);

                foreach (var point in data)
                {
                    tag.PointValues.Add(point.Timestamp, point.Value);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to load data for {tag.Name}: {ex.Message}");
            }
        }

        // 차트 새로고침
        Refresh();
    }

    private void ChkAutoUpdate_CheckedChanged(object? sender, EventArgs e)
    {
        if (chkAutoUpdate.Checked)
        {
            StartAutoUpdate();
        }
        else
        {
            StopAutoUpdate();
        }
    }

    #endregion

    #region Auto Update

    private void StartAutoUpdate()
    {
        if (_autoUpdateTimer != null) return;

        _autoUpdateTimer = new System.Windows.Forms.Timer
        {
            Interval = AutoUpdateInterval
        };
        _autoUpdateTimer.Tick += AutoUpdateTimer_Tick;
        _autoUpdateTimer.Start();
    }

    private void StopAutoUpdate()
    {
        if (_autoUpdateTimer == null) return;

        _autoUpdateTimer.Stop();
        _autoUpdateTimer.Tick -= AutoUpdateTimer_Tick;
        _autoUpdateTimer.Dispose();
        _autoUpdateTimer = null;
    }

    private void AutoUpdateTimer_Tick(object? sender, EventArgs e)
    {
        if (_dataProvider == null || Tags.Count == 0)
            return;

        // 현재 시간으로 새 데이터 포인트 추가
        DateTime now = DateTime.Now;

        // 각 Tag의 최신 값을 DataProvider에서 가져오기
        foreach (var tag in Tags)
        {
            try
            {
                double currentValue = _dataProvider.GetCurrentValue(tag.Name);
                tag.PointValues.Add(now, currentValue);
            }
            catch (Exception ex)
            {
                // 에러 발생 시 로깅 (실시간 업데이트 계속 진행)
                System.Diagnostics.Debug.WriteLine($"Failed to get value for {tag.Name}: {ex.Message}");
            }
        }

        // 시간 범위를 현재 시간으로 이동 (이벤트 발생 방지)
        _isUpdatingProgrammatically = true;
        try
        {
            // 실시간 모드: 설정된 오프셋과 윈도우 크기 사용
            dtpStart.Value = now.AddMinutes(-RealtimeStartOffsetMinutes);
            dtpEnd.Value = now.AddMinutes(RealtimeWindowMinutes);
            
            // X축 범위 직접 설정
            SetAxisLimits(dtpStart.Value, dtpEnd.Value);
        }
        finally
        {
            _isUpdatingProgrammatically = false;
        }

        // 차트 새로고침
        Refresh();
    }

    #endregion

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            StopAutoUpdate();
            components?.Dispose();
        }
        base.Dispose(disposing);
    }
}

/// <summary>
/// 차트 초기화 모드
/// </summary>
public enum ChartInitMode
{
    /// <summary>
    /// 빈 차트 상태
    /// </summary>
    Empty,

    /// <summary>
    /// 실시간 모드로 시작
    /// </summary>
    Realtime,

    /// <summary>
    /// 이력 모드로 시작
    /// </summary>
    Historical
}

/// <summary>
/// 시간 범위 변경 이벤트 인수
/// </summary>
public class TimeRangeEventArgs : EventArgs
{
    public DateTime StartTime { get; }
    public DateTime EndTime { get; }

    public long StartTimestamp => new DateTimeOffset(StartTime).ToUnixTimeSeconds();
    public long EndTimestamp => new DateTimeOffset(EndTime).ToUnixTimeSeconds();

    public TimeRangeEventArgs(DateTime startTime, DateTime endTime)
    {
        StartTime = startTime;
        EndTime = endTime;
    }
}

