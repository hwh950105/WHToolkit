using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace hwh.Controls.TrendChartControl
{
    /// <summary>
    /// 실제 센서 데이터를 사용하는 TrendChartControl 사용 예제
    /// </summary>
    public class SensorDataUsageExample
    {
        /// <summary>
        /// 예제 1: 실시간 모드로 차트 초기화
        /// </summary>
        public static void Example1_RealtimeMode(ScottPlotTrendChart chart)
        {
            // 1. 데이터 프로바이더 설정
            var dataProvider = new SensorDatabaseProvider();
            chart.DataProvider = dataProvider;

            // 2. 사용 가능한 센서 타입 가져오기
            List<string> sensorTypes = SensorDatabaseProvider.GetAvailableSensorTypes();

            // 3. 태그 추가 (센서 타입별로)
            foreach (string sensorType in sensorTypes)
            {
                chart.AddTag(sensorType);
            }

            // 4. 실시간 모드로 초기화
            chart.Initialize(ChartInitMode.Realtime);
        }

        /// <summary>
        /// 예제 2: 과거 데이터 조회 모드
        /// </summary>
        public static void Example2_HistoricalMode(ScottPlotTrendChart chart)
        {
            // 1. 데이터 프로바이더 설정
            var dataProvider = new SensorDatabaseProvider();
            chart.DataProvider = dataProvider;

            // 2. 특정 센서 타입만 추가
            chart.AddTag("temperature");
            chart.AddTag("humidity");

            // 3. 과거 데이터 로드 (최근 1시간)
            DateTime endTime = DateTime.Now;
            DateTime startTime = endTime.AddHours(-1);
            chart.LoadData(startTime, endTime);
        }

        /// <summary>
        /// 예제 3: 특정 센서만 선택해서 표시
        /// </summary>
        public static void Example3_SpecificSensors(ScottPlotTrendChart chart)
        {
            // 1. 데이터 프로바이더 설정
            var dataProvider = new SensorDatabaseProvider();
            chart.DataProvider = dataProvider;

            // 2. 원하는 센서만 선택
            var selectedSensors = new List<string> 
            { 
                "temperature",  // 온도
                "vibration",    // 진동
                "pressure"      // 압력
            };

            // 3. 태그 추가
            foreach (var sensor in selectedSensors)
            {
                chart.AddTag(sensor);
            }

            // 4. 초기화 (빈 상태)
            chart.Initialize(ChartInitMode.Empty);

            // 5. 데이터 로드 (최근 24시간)
            DateTime endTime = DateTime.Now;
            DateTime startTime = endTime.AddHours(-24);
            chart.LoadData(startTime, endTime);
        }

        /// <summary>
        /// 예제 4: Form에서 사용하는 전체 예제
        /// </summary>
        public class ExampleForm : Form
        {
            private ScottPlotTrendChart? trendChart;
            private ComboBox? cmbSensorType;
            private Button? btnAddSensor;
            private Button? btnRealtimeMode;
            private Button? btnHistoricalMode;

            public ExampleForm()
            {
                InitializeUI();
                InitializeChart();
            }

            private void InitializeUI()
            {
                this.Text = "센서 데이터 차트 예제";
                this.Size = new System.Drawing.Size(1200, 800);

                // 트렌드 차트 생성
                trendChart = new ScottPlotTrendChart
                {
                    Dock = DockStyle.Fill
                };

                // 센서 선택 콤보박스
                cmbSensorType = new ComboBox
                {
                    Dock = DockStyle.Top,
                    DropDownStyle = ComboBoxStyle.DropDownList
                };

                // 센서 추가 버튼
                btnAddSensor = new Button
                {
                    Text = "센서 추가",
                    Dock = DockStyle.Top
                };
                btnAddSensor.Click += BtnAddSensor_Click;

                // 실시간 모드 버튼
                btnRealtimeMode = new Button
                {
                    Text = "실시간 모드",
                    Dock = DockStyle.Top
                };
                btnRealtimeMode.Click += BtnRealtimeMode_Click;

                // 과거 데이터 버튼
                btnHistoricalMode = new Button
                {
                    Text = "과거 데이터 (최근 1시간)",
                    Dock = DockStyle.Top
                };
                btnHistoricalMode.Click += BtnHistoricalMode_Click;

                // 컨트롤 추가
                this.Controls.Add(trendChart);
                this.Controls.Add(btnHistoricalMode);
                this.Controls.Add(btnRealtimeMode);
                this.Controls.Add(btnAddSensor);
                this.Controls.Add(cmbSensorType);
            }

            private void InitializeChart()
            {
                if (trendChart == null || cmbSensorType == null) return;

                try
                {
                    // 데이터 프로바이더 설정
                    var dataProvider = new SensorDatabaseProvider();
                    trendChart.DataProvider = dataProvider;

                    // 사용 가능한 센서 타입 로드
                    List<string> sensorTypes = SensorDatabaseProvider.GetAvailableSensorTypes();
                    cmbSensorType.Items.Clear();
                    foreach (string sensorType in sensorTypes)
                    {
                        cmbSensorType.Items.Add(sensorType);
                    }

                    if (cmbSensorType.Items.Count > 0)
                    {
                        cmbSensorType.SelectedIndex = 0;
                    }

                    // 빈 차트로 시작
                    trendChart.Initialize(ChartInitMode.Empty);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"차트 초기화 실패: {ex.Message}", "오류", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            private void BtnAddSensor_Click(object? sender, EventArgs e)
            {
                if (trendChart == null || cmbSensorType == null) return;

                try
                {
                    string selectedSensor = cmbSensorType.SelectedItem?.ToString() ?? "";
                    if (string.IsNullOrEmpty(selectedSensor))
                    {
                        MessageBox.Show("센서를 선택해주세요.", "알림", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // 센서 추가
                    trendChart.AddTag(selectedSensor);

                    // 데이터 로드
                    trendChart.LoadData();

                    MessageBox.Show($"{selectedSensor} 센서가 추가되었습니다.", "알림", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"센서 추가 실패: {ex.Message}", "오류", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            private void BtnRealtimeMode_Click(object? sender, EventArgs e)
            {
                if (trendChart == null) return;

                try
                {
                    // 실시간 모드로 초기화 (기존 태그 유지)
                    trendChart.Initialize(ChartInitMode.Realtime);
                    
                    MessageBox.Show("실시간 모드로 전환되었습니다.", "알림", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"실시간 모드 전환 실패: {ex.Message}", "오류", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            private void BtnHistoricalMode_Click(object? sender, EventArgs e)
            {
                if (trendChart == null) return;

                try
                {
                    // 과거 데이터 로드 (최근 1시간)
                    DateTime endTime = DateTime.Now;
                    DateTime startTime = endTime.AddHours(-1);
                    trendChart.LoadData(startTime, endTime);

                    MessageBox.Show("과거 데이터가 로드되었습니다.", "알림", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"과거 데이터 로드 실패: {ex.Message}", "오류", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 예제 5: 동적으로 센서 추가/제거
        /// </summary>
        public static void Example5_DynamicSensors(ScottPlotTrendChart chart)
        {
            // 1. 데이터 프로바이더 설정
            var dataProvider = new SensorDatabaseProvider();
            chart.DataProvider = dataProvider;

            // 2. 초기 센서 추가
            chart.AddTag("temperature");
            chart.AddTag("humidity");
            chart.Initialize(ChartInitMode.Realtime);

            // 3. 나중에 센서 추가
            System.Threading.Tasks.Task.Delay(5000).ContinueWith(_ =>
            {
                chart.AddTag("pressure");
            });

            // 4. 나중에 센서 제거
            System.Threading.Tasks.Task.Delay(10000).ContinueWith(_ =>
            {
                chart.RemoveTag("humidity");
            });
        }
    }
}

