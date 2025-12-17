using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace hwh.Controls.TrendChartControl
{
    /// <summary>
    /// 실제 센서 데이터베이스를 사용하는 데이터 프로바이더
    /// sensor_data 테이블을 사용하여 차트 데이터를 제공합니다.
    /// </summary>
    public class SensorDatabaseProvider : ITagDataProvider
    {
        private static readonly Dictionary<string, Color> ColorPalette = new Dictionary<string, Color>
        {
            { "temperature", Color.Red },
            { "humidity", Color.Blue },
            { "vibration", Color.Orange },
            { "pressure", Color.Green },
            { "flow", Color.Purple },
            { "level", Color.Cyan },
            { "power", Color.Magenta },
            { "voltage", Color.DarkOrange },
            { "current", Color.DarkBlue }
        };

        private int _colorIndex = 0;
        private readonly Color[] _defaultColors = new[]
        {
            Color.Red, Color.Blue, Color.Green, Color.Orange, 
            Color.Purple, Color.Cyan, Color.Magenta, Color.Brown
        };

        /// <summary>
        /// 태그(센서 타입) 메타데이터 가져오기
        /// </summary>
        public TagMetadata GetTagMetadata(string tagName)
        {
            try
            {
                string unit = DbCall.GetSensorUnit(tagName);
                
                // 센서 타입에 맞는 색상 선택
                Color color;
                if (ColorPalette.ContainsKey(tagName.ToLower()))
                {
                    color = ColorPalette[tagName.ToLower()];
                }
                else
                {
                    color = _defaultColors[_colorIndex % _defaultColors.Length];
                    _colorIndex++;
                }

                return new TagMetadata
                {
                    Name = tagName,
                    DisplayName = GetDisplayName(tagName),
                    Unit = unit,
                    Color = $"#{color.R:X2}{color.G:X2}{color.B:X2}"
                };
            }
            catch (Exception ex)
            {
                Core.LogHelper.Error(ex, "태그 메타데이터 조회 실패 - TagName: {0}", tagName);
                throw new Exception($"태그 메타데이터 조회 실패: {tagName}", ex);
            }
        }

        /// <summary>
        /// 현재 값 가져오기 (최신 데이터)
        /// </summary>
        public double GetCurrentValue(string tagName)
        {
            try
            {
                return DbCall.GetLatestSensorValue(tagName);
            }
            catch (Exception ex)
            {
                Core.LogHelper.Error(ex, "현재 값 조회 실패 - TagName: {0}", tagName);
                throw new Exception($"현재 값 조회 실패: {tagName}", ex);
            }
        }

        /// <summary>
        /// 이력 데이터 가져오기
        /// </summary>
        public List<DataPoint> GetHistoricalData(string tagName, DateTime startTime, DateTime endTime)
        {
            try
            {

                List<DataPoint> dataPoints = new List<DataPoint>();
                DataTable dataTable = DbCall.GetSensorDataByTimeRange(tagName, startTime, endTime);
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    DataPoint dataPoint = new DataPoint();
                    DateTime timestamp = Convert.ToDateTime(dataTable.Rows[i]["created_at"]);
                    double value = Convert.ToDouble(dataTable.Rows[i]["value"]);
                    dataPoint.Timestamp = timestamp;
                    dataPoint.Value = value;
                    dataPoints.Add(dataPoint);    
                }

                return DbCall.GetSensorDataByTimeRangelist(tagName, startTime, endTime);
            }
            catch (Exception ex)
            {
                Core.LogHelper.Error(ex, "이력 데이터 조회 실패 - TagName: {0}", tagName);
                throw new Exception($"이력 데이터 조회 실패: {tagName}", ex);
            }
        }

        /// <summary>
        /// 센서 타입을 사람이 읽기 쉬운 이름으로 변환
        /// </summary>
        private string GetDisplayName(string sensorType)
        {
            var displayNames = new Dictionary<string, string>
            {
                { "temperature", "온도" },
                { "humidity", "습도" },
                { "vibration", "진동" },
                { "pressure", "압력" },
                { "flow", "유량" },
                { "level", "레벨" },
                { "power", "전력" },
                { "voltage", "전압" },
                { "current", "전류" }
            };

            return displayNames.ContainsKey(sensorType.ToLower()) 
                ? displayNames[sensorType.ToLower()] 
                : sensorType;
        }

        /// <summary>
        /// 사용 가능한 모든 센서 타입 목록 가져오기
        /// </summary>
        public static List<string> GetAvailableSensorTypes()
        {
            return DbCall.GetSensorTypes();
        }

    }
}

