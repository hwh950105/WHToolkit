using System;
using System.Collections.Generic;

namespace hwh.Controls.TrendChartControl
{
    /// <summary>
    /// 태그 데이터 제공자 인터페이스
    /// </summary>
    public interface ITagDataProvider
    {
        /// <summary>
        /// 태그 메타데이터 가져오기
        /// </summary>
        TagMetadata GetTagMetadata(string tagName);

        /// <summary>
        /// 현재 값 가져오기 (실시간용)
        /// </summary>
        double GetCurrentValue(string tagName);

        /// <summary>
        /// 이력 데이터 가져오기
        /// </summary>
        List<DataPoint> GetHistoricalData(string tagName, DateTime startTime, DateTime endTime);
    }

    /// <summary>
    /// 태그 메타데이터
    /// </summary>
    public class TagMetadata
    {
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
    }

    /// <summary>
    /// 데이터 포인트
    /// </summary>
    public class DataPoint
    {
        public DateTime Timestamp { get; set; }
        public double Value { get; set; }

        public DataPoint() { }

        public DataPoint(DateTime timestamp, double value)
        {
            Timestamp = timestamp;
            Value = value;
        }
    }
}

