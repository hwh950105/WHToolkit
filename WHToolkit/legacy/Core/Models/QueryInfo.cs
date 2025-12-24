using System.Collections.Generic;

namespace WHToolkit.Database.Models
{
    /// <summary>
    /// 쿼리 실행에 필요한 정보를 담는 클래스입니다.
    /// </summary>
    public class QueryInfo
    {
        /// <summary>
        /// 매개변수 사용 여부
        /// </summary>
        public bool IsParameter { get; set; } = true;

        /// <summary>
        /// Identity 컬럼 존재 여부
        /// </summary>
        public bool IsIdentity { get; set; } = false;

        /// <summary>
        /// 기본키 컬럼 목록
        /// </summary>
        public List<string> PrimaryKey { get; set; } = new List<string>();

        /// <summary>
        /// 속성 이름 목록
        /// </summary>
        public List<string> PropNames { get; set; } = new List<string>();

        /// <summary>
        /// 컬럼 이름 목록
        /// </summary>
        public List<string> ColumnNames
        {
            get
            {
                List<string> result = new List<string>();
                PropNames.ForEach(prop => { result.Add(ColNameByPropName[prop]); });
                return result;
            }
        }

        /// <summary>
        /// 속성명 -> 컬럼명 매핑 딕셔너리
        /// </summary>
        public Dictionary<string, string> ColNameByPropName { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// 값 딕셔너리
        /// </summary>
        public Dictionary<string, object> Values { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// Identity 키 컬럼명
        /// </summary>
        public string IdentityKey { get; set; } = string.Empty;

        /// <summary>
        /// 테이블명
        /// </summary>
        public string TableName { get; set; } = string.Empty;

        /// <summary>
        /// WHERE 절
        /// </summary>
        public string Where { get; set; } = string.Empty;

        /// <summary>
        /// GROUP BY 절
        /// </summary>
        public string GroupBy { get; set; } = string.Empty;

        /// <summary>
        /// ORDER BY 절
        /// </summary>
        public string OrderBy { get; set; } = string.Empty;

        /// <summary>
        /// 시작 행 번호 (페이징용)
        /// </summary>
        public int StartRow { get; set; } = 0;

        /// <summary>
        /// 페이지 크기 (페이징용)
        /// </summary>
        public int RowSize { get; set; }
    }
}