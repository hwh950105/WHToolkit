using System;
using System.Collections.Generic;
using System.Linq;

namespace hwh.Data
{
    /// <summary>
    /// 가짜 데이터베이스 - 실시간 태그 데이터 생성 및 관리
    /// </summary>
    public class FakeDatabase
    {
        private static FakeDatabase? _instance;
        private static readonly object _lock = new object();
        private Random _random = new Random();
        private Dictionary<string, TagData> _tags = new Dictionary<string, TagData>();
        private DateTime _startTime;

        /// <summary>
        /// 싱글톤 인스턴스
        /// </summary>
        public static FakeDatabase Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new FakeDatabase();
                        }
                    }
                }
                return _instance;
            }
        }

        private FakeDatabase()
        {
            _startTime = DateTime.Now.AddHours(-2); // 2시간 전부터 데이터 시작
            InitializeTags();
            GenerateHistoricalData();
        }

        /// <summary>
        /// 초기 태그 설정
        /// </summary>
        private void InitializeTags()
        {
            _tags.Clear();

            // 온도 센서들
            _tags.Add("TEMP_01", new TagData("TEMP_01", "반응로 온도", "°C", 20, 100, "#FF6B6B"));
            _tags.Add("TEMP_02", new TagData("TEMP_02", "냉각수 온도", "°C", 15, 45, "#4ECDC4"));
            _tags.Add("TEMP_03", new TagData("TEMP_03", "배출구 온도", "°C", 25, 85, "#FFE66D"));

            // 압력 센서들
            _tags.Add("PRES_01", new TagData("PRES_01", "반응로 압력", "kPa", 100, 150, "#95E1D3"));
            _tags.Add("PRES_02", new TagData("PRES_02", "공급 압력", "kPa", 80, 120, "#F38181"));

            // 유량 센서들
            _tags.Add("FLOW_01", new TagData("FLOW_01", "원료 유량", "L/min", 10, 50, "#AA96DA"));
            _tags.Add("FLOW_02", new TagData("FLOW_02", "냉각수 유량", "L/min", 20, 80, "#FCBAD3"));

            // 레벨 센서들
            _tags.Add("LEVEL_01", new TagData("LEVEL_01", "탱크 A 레벨", "%", 0, 100, "#A8D8EA"));
            _tags.Add("LEVEL_02", new TagData("LEVEL_02", "탱크 B 레벨", "%", 0, 100, "#FFCFDF"));

            // 전력 센서들
            _tags.Add("POWER_01", new TagData("POWER_01", "주 펌프 전력", "kW", 5, 25, "#C7CEEA"));
            _tags.Add("POWER_02", new TagData("POWER_02", "보조 펌프 전력", "kW", 3, 15, "#B5EAD7"));
        }

        /// <summary>
        /// 과거 데이터 생성 (2시간 분량)
        /// </summary>
        private void GenerateHistoricalData()
        {
            DateTime currentTime = _startTime;
            DateTime endTime = DateTime.Now;

            while (currentTime <= endTime)
            {
                foreach (var tag in _tags.Values)
                {
                    double value = GenerateRealisticValue(tag, currentTime);
                    tag.AddDataPoint(currentTime, value);
                }

                currentTime = currentTime.AddSeconds(5); // 5초 간격
            }
        }

        /// <summary>
        /// 실제처럼 보이는 값 생성 (트렌드 + 노이즈)
        /// </summary>
        private double GenerateRealisticValue(TagData tag, DateTime timestamp)
        {
            double range = tag.MaxValue - tag.MinValue;
            double center = (tag.MaxValue + tag.MinValue) / 2;

            // 시간 기반 사인파 트렌드
            double timeOffset = (timestamp - _startTime).TotalMinutes;
            double trend = Math.Sin(timeOffset * 0.05) * (range * 0.3);

            // 랜덤 노이즈
            double noise = (_random.NextDouble() - 0.5) * (range * 0.1);

            // 이전 값에 기반한 부드러운 변화
            double previousValue = tag.CurrentValue;
            if (tag.TimeSeriesData.Count > 0)
            {
                previousValue = tag.TimeSeriesData.Last().Value;
            }

            double smoothing = 0.7;
            double rawValue = center + trend + noise;
            double smoothedValue = previousValue * smoothing + rawValue * (1 - smoothing);

            // 범위 제한
            return Math.Max(tag.MinValue, Math.Min(tag.MaxValue, smoothedValue));
        }

        /// <summary>
        /// 새 데이터 포인트 생성 (실시간 업데이트용)
        /// </summary>
        public void UpdateAllTags()
        {
            DateTime now = DateTime.Now;

            foreach (var tag in _tags.Values)
            {
                double value = GenerateRealisticValue(tag, now);
                tag.AddDataPoint(now, value);
            }
        }

        /// <summary>
        /// 모든 태그 가져오기
        /// </summary>
        public List<TagData> GetAllTags()
        {
            return _tags.Values.ToList();
        }

        /// <summary>
        /// 특정 태그 가져오기
        /// </summary>
        public TagData? GetTag(string tagName)
        {
            return _tags.ContainsKey(tagName) ? _tags[tagName] : null;
        }

        /// <summary>
        /// 태그 이름 목록 가져오기
        /// </summary>
        public List<string> GetTagNames()
        {
            return _tags.Keys.ToList();
        }

        /// <summary>
        /// 랜덤 태그 가져오기
        /// </summary>
        public List<TagData> GetRandomTags(int count)
        {
            var allTags = _tags.Values.ToList();
            var selectedTags = new List<TagData>();

            // 무작위로 태그 선택
            for (int i = 0; i < Math.Min(count, allTags.Count); i++)
            {
                int index = _random.Next(allTags.Count);
                if (!selectedTags.Contains(allTags[index]))
                {
                    selectedTags.Add(allTags[index]);
                }
            }

            return selectedTags;
        }

        /// <summary>
        /// 특정 카테고리의 태그 가져오기
        /// </summary>
        public List<TagData> GetTagsByCategory(string category)
        {
            return _tags.Values
                .Where(t => t.Name.StartsWith(category))
                .ToList();
        }

        /// <summary>
        /// 데이터베이스 리셋
        /// </summary>
        public void Reset()
        {
            _startTime = DateTime.Now.AddHours(-2);
            InitializeTags();
            GenerateHistoricalData();
        }
    }
}

