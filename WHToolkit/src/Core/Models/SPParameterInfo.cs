namespace WHToolkit.Database
{
    /// <summary>
    /// 저장 프로시저 매개변수 정보를 나타내는 클래스입니다.
    /// </summary>
    internal class SPParameterInfo
    {
        /// <summary>
        /// 매개변수 이름
        /// </summary>
        public string PARAMETER_NAME { get; set; } = string.Empty;

        /// <summary>
        /// 매개변수 타입
        /// </summary>
        public string TYPE { get; set; } = string.Empty;

        /// <summary>
        /// 매개변수 길이
        /// </summary>
        public int LENGTH { get; set; }

        /// <summary>
        /// NULL 허용 여부
        /// </summary>
        public bool NULLABLE { get; set; }

        /// <summary>
        /// 정밀도
        /// </summary>
        public int PREC { get; set; }

        /// <summary>
        /// 스케일
        /// </summary>
        public double SCALE { get; set; }

        /// <summary>
        /// 매개변수 순서
        /// </summary>
        public int PARAM_ORDER { get; set; }

        /// <summary>
        /// 콜레이션
        /// </summary>
        public string COLLATION { get; set; } = string.Empty;

        /// <summary>
        /// NULL 값을 가진 매개변수를 생성합니다.
        /// </summary>
        /// <returns>NULL 값의 DataParameter</returns>
        public DataParameter CreateNullParameter()
        {
            return new DataParameter(PARAMETER_NAME, null);
        }
    }
}