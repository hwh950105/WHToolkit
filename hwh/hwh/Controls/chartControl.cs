using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScottPlot;
using hwh.Data;

namespace hwh.Controls
{
    public partial class chartControl : UserControl
    {
        private Random rand = new Random();
        private FakeDatabase _db;
        private System.Windows.Forms.Timer _updateTimer;
        private bool _autoUpdate = false;
        private Action? _lastChartAction = null; // 마지막 차트 생성 액션 저장

        public chartControl()
        {
            InitializeComponent();
            
            // 가짜 DB 초기화
            _db = FakeDatabase.Instance;
            
            // 업데이트 타이머 초기화
            _updateTimer = new System.Windows.Forms.Timer();
            _updateTimer.Interval = 1000; // 1초마다 업데이트
            _updateTimer.Tick += UpdateTimer_Tick;
            
            LoadDefaultChart();
        }

        /// <summary>
        /// 타이머 틱 이벤트 - 실시간 데이터 업데이트
        /// </summary>
        private void UpdateTimer_Tick(object? sender, EventArgs e)
        {
            // DB에서 새 데이터 생성
            _db.UpdateAllTags();
            
            // 현재 표시 중인 차트 새로고침
            RefreshCurrentChart();
        }

        /// <summary>
        /// 현재 차트 새로고침
        /// </summary>
        private void RefreshCurrentChart()
        {
            // 마지막으로 저장된 차트 액션을 다시 실행
            if (_lastChartAction != null)
            {
                _lastChartAction.Invoke();
            }
        }

        /// <summary>
        /// 자동 업데이트 시작
        /// </summary>
        public void StartAutoUpdate()
        {
            _autoUpdate = true;
            _updateTimer.Start();
        }

        /// <summary>
        /// 자동 업데이트 중지
        /// </summary>
        public void StopAutoUpdate()
        {
            _autoUpdate = false;
            _updateTimer.Stop();
        }

        /// <summary>
        /// 기본 차트 로드 (산점도)
        /// </summary>
        private void LoadDefaultChart()
        {
            btnScatter_Click(this, EventArgs.Empty);
        }

        /// <summary>
        /// Clear 후 축 복원 (파이 차트의 Frameless 상태 해제)
        /// </summary>
        private void RestoreAxes()
        {
            // 축 가시성 복원
            formsPlot1.Plot.Axes.Bottom.IsVisible = true;
            formsPlot1.Plot.Axes.Left.IsVisible = true;
            formsPlot1.Plot.Axes.Top.IsVisible = true;
            formsPlot1.Plot.Axes.Right.IsVisible = true;
            
            // 축 프레임 라인 복원
            formsPlot1.Plot.Axes.Bottom.FrameLineStyle.Width = 1;
            formsPlot1.Plot.Axes.Bottom.FrameLineStyle.Color = ScottPlot.Colors.Black;
            formsPlot1.Plot.Axes.Left.FrameLineStyle.Width = 1;
            formsPlot1.Plot.Axes.Left.FrameLineStyle.Color = ScottPlot.Colors.Black;
            formsPlot1.Plot.Axes.Top.FrameLineStyle.Width = 1;
            formsPlot1.Plot.Axes.Top.FrameLineStyle.Color = ScottPlot.Colors.Black;
            formsPlot1.Plot.Axes.Right.FrameLineStyle.Width = 1;
            formsPlot1.Plot.Axes.Right.FrameLineStyle.Color = ScottPlot.Colors.Black;
            
            // 커스텀 TickGenerator 초기화 (막대 그래프의 레이블 제거)
            formsPlot1.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericAutomatic();
            formsPlot1.Plot.Axes.Left.TickGenerator = new ScottPlot.TickGenerators.NumericAutomatic();
        }

        /// <summary>
        /// 차트 공통 스타일 적용 (폰트 크기, 여백 등)
        /// </summary>
        private void ApplyChartStyling()
        {
            // 제목 폰트 설정 (한글 지원)
            formsPlot1.Plot.Axes.Title.Label.FontSize = 20;
            formsPlot1.Plot.Axes.Title.Label.Bold = true;
            formsPlot1.Plot.Axes.Title.Label.FontName = "맑은 고딕";
            
            // 축 레이블 폰트 설정 (한글 지원)
            formsPlot1.Plot.Axes.Bottom.Label.FontSize = 16;
            formsPlot1.Plot.Axes.Bottom.Label.Bold = true;
            formsPlot1.Plot.Axes.Bottom.Label.FontName = "맑은 고딕";
            
            formsPlot1.Plot.Axes.Left.Label.FontSize = 16;
            formsPlot1.Plot.Axes.Left.Label.Bold = true;
            formsPlot1.Plot.Axes.Left.Label.FontName = "맑은 고딕";
            
            // 축 눈금 레이블 폰트 설정 (한글 지원)
            formsPlot1.Plot.Axes.Bottom.TickLabelStyle.FontSize = 13;
            formsPlot1.Plot.Axes.Bottom.TickLabelStyle.FontName = "맑은 고딕";
            
            formsPlot1.Plot.Axes.Left.TickLabelStyle.FontSize = 13;
            formsPlot1.Plot.Axes.Left.TickLabelStyle.FontName = "맑은 고딕";
            
            formsPlot1.Plot.Axes.Right.TickLabelStyle.FontSize = 13;
            formsPlot1.Plot.Axes.Right.TickLabelStyle.FontName = "맑은 고딕";
            
            formsPlot1.Plot.Axes.Top.TickLabelStyle.FontSize = 13;
            formsPlot1.Plot.Axes.Top.TickLabelStyle.FontName = "맑은 고딕";
            
            // 범례 폰트 설정 (한글 지원)
            formsPlot1.Plot.Legend.FontSize = 14;
            formsPlot1.Plot.Legend.FontName = "맑은 고딕";
            
            // 그리드 표시
            formsPlot1.Plot.Grid.IsVisible = true;
            
            // 자동 축 마진 (데이터가 잘 보이도록)
            formsPlot1.Plot.Axes.AutoScale();
            formsPlot1.Plot.Axes.Margins(0.05, 0.1);
        }

        /// <summary>
        /// 산점도 (Scatter Plot) - 온도 vs 압력
        /// </summary>
        private void btnScatter_Click(object sender, EventArgs e)
        {
            _lastChartAction = DrawScatterChart;
            DrawScatterChart();
            
            // 실시간 업데이트 시작
            if (!_autoUpdate)
            {
                StartAutoUpdate();
            }
        }

        private void DrawScatterChart()
        {
            formsPlot1.Plot.Clear();
            RestoreAxes();

            // DB에서 온도와 압력 태그 가져오기
            var tempTag = _db.GetTag("TEMP_01");
            var presTag = _db.GetTag("PRES_01");

            if (tempTag != null && presTag != null)
            {
                // 최근 100개 데이터 포인트 가져오기
                var tempData = tempTag.TimeSeriesData.TakeLast(100).ToArray();
                var presData = presTag.TimeSeriesData.TakeLast(100).ToArray();

                if (tempData.Length > 0 && presData.Length > 0)
                {
                    double[] xs = tempData.Select(d => d.Value).ToArray();
                    double[] ys = presData.Select(d => d.Value).ToArray();

                    // 산점도 추가
                    var scatter = formsPlot1.Plot.Add.Scatter(xs, ys);
                    scatter.MarkerSize = 8;
                    scatter.LineWidth = 0;
                    scatter.Color = ScottPlot.Color.FromHex("#1f77b4");

                    // 차트 꾸미기
                    formsPlot1.Plot.Title("온도 vs 압력 상관관계");
                    formsPlot1.Plot.XLabel($"{tempTag.DisplayName} ({tempTag.Unit})");
                    formsPlot1.Plot.YLabel($"{presTag.DisplayName} ({presTag.Unit})");
                    formsPlot1.Plot.Grid.MajorLineColor = ScottPlot.Color.FromHex("#EEEEEE");
                    formsPlot1.Plot.Grid.MajorLineWidth = 1.5f;
                }
            }

            ApplyChartStyling();
            formsPlot1.Refresh();
        }

        /// <summary>
        /// 막대 그래프 (Bar Chart) - 각 태그의 현재 값
        /// </summary>
        private void btnBar_Click(object sender, EventArgs e)
        {
            formsPlot1.Plot.Clear();
            RestoreAxes();
            StopAutoUpdate(); // 막대 그래프는 정적 표시

            // DB에서 랜덤 태그 가져오기
            var tags = _db.GetRandomTags(7);

            if (tags.Count > 0)
            {
                string[] labels = tags.Select(t => t.DisplayName).ToArray();
                double[] values = tags.Select(t => t.CurrentValue).ToArray();
                double[] positions = Generate.Consecutive(values.Length);

                // 막대 그래프 추가
                var bars = formsPlot1.Plot.Add.Bars(positions, values);
                
                // 각 막대에 다른 색상 적용
                for (int i = 0; i < bars.Bars.Count && i < tags.Count; i++)
                {
                    bars.Bars[i].FillColor = ScottPlot.Color.FromHex(tags[i].Color);
                }

                // X축 레이블 설정
                ScottPlot.TickGenerators.NumericManual ticks = new();
                for (int i = 0; i < labels.Length; i++)
                {
                    ticks.AddMajor(i, labels[i]);
                }
                formsPlot1.Plot.Axes.Bottom.TickGenerator = ticks;

                // 차트 꾸미기
                formsPlot1.Plot.Title("센서별 현재 값");
                formsPlot1.Plot.XLabel("센서");
                formsPlot1.Plot.YLabel("값");
                formsPlot1.Plot.Axes.SetLimitsX(-1, labels.Length);
                formsPlot1.Plot.Grid.MajorLineColor = ScottPlot.Color.FromHex("#EEEEEE");
                formsPlot1.Plot.Grid.MajorLineWidth = 1.5f;
            }

            ApplyChartStyling();
            formsPlot1.Refresh();
        }

        /// <summary>
        /// 선 그래프 (Line Chart) - 시계열 데이터
        /// </summary>
        private void btnLine_Click(object sender, EventArgs e)
        {
            _lastChartAction = DrawLineChart;
            DrawLineChart();
            
            // 실시간 업데이트 시작
            if (!_autoUpdate)
            {
                StartAutoUpdate();
            }
        }

        private void DrawLineChart()
        {
            formsPlot1.Plot.Clear();
            RestoreAxes();

            // DB에서 온도 센서들 가져오기
            var tempTags = _db.GetTagsByCategory("TEMP");

            if (tempTags.Count > 0)
            {
                foreach (var tag in tempTags)
                {
                    // 최근 200개 데이터 포인트로 시계열 표시
                    var (xs, ys) = tag.ToScottPlotArrays();
                    
                    if (xs.Length > 0)
                    {
                        var line = formsPlot1.Plot.Add.Scatter(xs, ys);
                        line.LineWidth = 2;
                        line.Color = ScottPlot.Color.FromHex(tag.Color);
                        line.LegendText = tag.DisplayName;
                        line.MarkerSize = 0;
                    }
                }

                // X축을 DateTime 형식으로 표시
                formsPlot1.Plot.Axes.DateTimeTicksBottom();

                // 범례 추가
                formsPlot1.Plot.Legend.IsVisible = true;
                formsPlot1.Plot.Legend.Alignment = Alignment.UpperRight;

                // 차트 꾸미기
                formsPlot1.Plot.Title("온도 센서 시계열 추이");
                formsPlot1.Plot.XLabel("시간");
                formsPlot1.Plot.YLabel("온도 (°C)");
                formsPlot1.Plot.Grid.MajorLineColor = ScottPlot.Color.FromHex("#EEEEEE");
                formsPlot1.Plot.Grid.MajorLineWidth = 1.5f;
            }

            ApplyChartStyling();
            formsPlot1.Refresh();
        }

        /// <summary>
        /// 다중 시계열 그래프 - 압력과 유량
        /// </summary>
        private void btnSin_Click(object sender, EventArgs e)
        {
            _lastChartAction = DrawMultiSeriesChart;
            DrawMultiSeriesChart();
            
            // 실시간 업데이트 시작
            if (!_autoUpdate)
            {
                StartAutoUpdate();
            }
        }

        private void DrawMultiSeriesChart()
        {
            formsPlot1.Plot.Clear();
            RestoreAxes();

            // DB에서 압력과 유량 센서 가져오기
            var presTags = _db.GetTagsByCategory("PRES");
            var flowTags = _db.GetTagsByCategory("FLOW");

            var allTags = new List<TagData>();
            allTags.AddRange(presTags.Take(2));
            allTags.AddRange(flowTags.Take(2));

            if (allTags.Count > 0)
            {
                foreach (var tag in allTags)
                {
                    var (xs, ys) = tag.ToScottPlotArrays();
                    
                    if (xs.Length > 0)
                    {
                        // 정규화 (0-100 범위로)
                        double min = tag.MinValue;
                        double max = tag.MaxValue;
                        double[] normalizedYs = ys.Select(y => (y - min) / (max - min) * 100).ToArray();

                        var line = formsPlot1.Plot.Add.Scatter(xs, normalizedYs);
                        line.LineWidth = 2;
                        line.Color = ScottPlot.Color.FromHex(tag.Color);
                        line.LegendText = $"{tag.DisplayName} (정규화)";
                        line.MarkerSize = 0;
                    }
                }

                // X축을 DateTime 형식으로 표시
                formsPlot1.Plot.Axes.DateTimeTicksBottom();

                // 범례 추가
                formsPlot1.Plot.Legend.IsVisible = true;
                formsPlot1.Plot.Legend.Alignment = Alignment.UpperRight;

                // 차트 꾸미기
                formsPlot1.Plot.Title("압력/유량 센서 비교 (정규화)");
                formsPlot1.Plot.XLabel("시간");
                formsPlot1.Plot.YLabel("정규화 값 (%)");
                formsPlot1.Plot.Axes.SetLimitsY(-10, 110);
                formsPlot1.Plot.Grid.MajorLineColor = ScottPlot.Color.FromHex("#EEEEEE");
                formsPlot1.Plot.Grid.MajorLineWidth = 1.5f;
            }

            ApplyChartStyling();
            formsPlot1.Refresh();
        }

        /// <summary>
        /// 파이 차트 (Pie Chart) - 태그 카테고리별 평균 값
        /// </summary>
        private void btnPie_Click(object sender, EventArgs e)
        {
            formsPlot1.Plot.Clear();
            StopAutoUpdate(); // 파이 차트는 정적 표시

            // DB에서 랜덤 태그 가져오기
            var tags = _db.GetRandomTags(5);

            if (tags.Count > 0)
            {
                // 파이 데이터 생성
                List<ScottPlot.PieSlice> slices = new();
                
                foreach (var tag in tags)
                {
                    slices.Add(new ScottPlot.PieSlice
                    {
                        Value = tag.CurrentValue,
                        Label = tag.DisplayName,
                        FillColor = ScottPlot.Color.FromHex(tag.Color),
                        LabelFontSize = 14,
                        LabelFontName = "맑은 고딕"
                    });
                }

                // 파이 차트 추가
                var pie = formsPlot1.Plot.Add.Pie(slices);
                pie.ExplodeFraction = 0.05; // 약간 분리
                pie.SliceLabelDistance = 1.3; // 레이블 거리

                // 차트 꾸미기
                formsPlot1.Plot.Title("센서별 현재 값 비율");
                formsPlot1.Plot.Axes.Title.Label.FontSize = 20;
                formsPlot1.Plot.Axes.Title.Label.Bold = true;
                formsPlot1.Plot.Axes.Title.Label.FontName = "맑은 고딕";
                formsPlot1.Plot.HideGrid();
                formsPlot1.Plot.Axes.Frameless();
            }

            formsPlot1.Refresh();
        }

        /// <summary>
        /// 랜덤 태그 실시간 모니터링
        /// </summary>
        private void btnRandom_Click(object sender, EventArgs e)
        {
            _lastChartAction = DrawRandomChart;
            DrawRandomChart();
            
            // 실시간 업데이트 시작
            if (!_autoUpdate)
            {
                StartAutoUpdate();
            }
        }

        private void DrawRandomChart()
        {
            formsPlot1.Plot.Clear();
            RestoreAxes();

            // DB에서 모든 태그 가져오기
            var allTags = _db.GetAllTags();

            if (allTags.Count > 0)
            {
                // 랜덤으로 5개 태그 선택
                var selectedTags = _db.GetRandomTags(5);

                foreach (var tag in selectedTags)
                {
                    // 최근 100개 데이터 포인트
                    var (xs, ys) = tag.ToScottPlotArrays();
                    
                    if (xs.Length > 0)
                    {
                        var line = formsPlot1.Plot.Add.Scatter(xs, ys);
                        line.LineWidth = 2;
                        line.Color = ScottPlot.Color.FromHex(tag.Color);
                        line.LegendText = $"{tag.DisplayName} ({tag.Unit})";
                        line.MarkerSize = 0;
                    }
                }

                // X축을 DateTime 형식으로 표시
                formsPlot1.Plot.Axes.DateTimeTicksBottom();

                // 범례 추가
                formsPlot1.Plot.Legend.IsVisible = true;
                formsPlot1.Plot.Legend.Alignment = Alignment.UpperRight;

                // 차트 꾸미기
                formsPlot1.Plot.Title("랜덤 센서 실시간 모니터링");
                formsPlot1.Plot.XLabel("시간");
                formsPlot1.Plot.YLabel("값");
                formsPlot1.Plot.Grid.MajorLineColor = ScottPlot.Color.FromHex("#EEEEEE");
                formsPlot1.Plot.Grid.MajorLineWidth = 1.5f;
            }

            ApplyChartStyling();
            formsPlot1.Refresh();
        }
    }
}
