using System;
using System.Collections.Generic;
using System.Linq;

namespace hwh.Data
{
    /// <summary>
    /// 태그 데이터 모델
    /// </summary>
    public class TagData
    {
        /// <summary>
        /// 태그 이름
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 태그 표시 이름
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 태그 단위
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 최소값
        /// </summary>
        public double MinValue { get; set; }

        /// <summary>
        /// 최대값
        /// </summary>
        public double MaxValue { get; set; }

        /// <summary>
        /// 현재값
        /// </summary>
        public double CurrentValue { get; set; }

        /// <summary>
        /// 시계열 데이터 (시간, 값)
        /// </summary>
        public List<(DateTime Timestamp, double Value)> TimeSeriesData { get; set; }

        /// <summary>
        /// 색상 (차트 표시용)
        /// </summary>
        public string Color { get; set; }

        public TagData(string name, string displayName, string unit, double minValue, double maxValue, string color)
        {
            Name = name;
            DisplayName = displayName;
            Unit = unit;
            MinValue = minValue;
            MaxValue = maxValue;
            CurrentValue = (minValue + maxValue) / 2;
            TimeSeriesData = new List<(DateTime, double)>();
            Color = color;
        }

        /// <summary>
        /// 새 데이터 포인트 추가
        /// </summary>
        public void AddDataPoint(DateTime timestamp, double value)
        {
            CurrentValue = value;
            TimeSeriesData.Add((timestamp, value));

            // 최대 1000개 데이터 포인트만 유지
            if (TimeSeriesData.Count > 1000)
            {
                TimeSeriesData.RemoveAt(0);
            }
        }

        /// <summary>
        /// 특정 시간 범위의 데이터 가져오기
        /// </summary>
        public List<(DateTime Timestamp, double Value)> GetDataInRange(DateTime startTime, DateTime endTime)
        {
            return TimeSeriesData
                .Where(d => d.Timestamp >= startTime && d.Timestamp <= endTime)
                .ToList();
        }

        /// <summary>
        /// ScottPlot용 배열로 변환
        /// </summary>
        public (double[] xs, double[] ys) ToScottPlotArrays(DateTime? startTime = null, DateTime? endTime = null)
        {
            var data = startTime.HasValue && endTime.HasValue
                ? GetDataInRange(startTime.Value, endTime.Value)
                : TimeSeriesData;

            if (data.Count == 0)
                return (Array.Empty<double>(), Array.Empty<double>());

            double[] xs = data.Select(d => d.Timestamp.ToOADate()).ToArray();
            double[] ys = data.Select(d => d.Value).ToArray();

            return (xs, ys);
        }
    }
}

