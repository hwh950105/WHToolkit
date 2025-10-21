namespace WHToolkit.Database.Models
{
    /// <summary>
    /// 데이터베이스 조건절에서 사용되는 비교 연산자를 정의하는 열거형입니다.
    /// </summary>
    public enum ComparisonOperator
    {
        /// <summary>
        /// 같음 (=)
        /// </summary>
        Equal,

        /// <summary>
        /// 같지 않음 (!=)
        /// </summary>
        NotEqual,

        /// <summary>
        /// 작음 (&lt;)
        /// </summary>
        LessThan,

        /// <summary>
        /// 큼 (&gt;)
        /// </summary>
        GreaterThan,

        /// <summary>
        /// 작거나 같음 (&lt;=)
        /// </summary>
        LessThanOrEqual,

        /// <summary>
        /// 크거나 같음 (&gt;=)
        /// </summary>
        GreaterThanOrEqual,

        /// <summary>
        /// 포함 (LIKE '%value%')
        /// </summary>
        Like,

        /// <summary>
        /// 시작 (LIKE 'value%')
        /// </summary>
        StartWith,

        /// <summary>
        /// 끝남 (LIKE '%value')
        /// </summary>
        EndWith,

        /// <summary>
        /// 대소문자 구분 없이 같음 (UPPER(column) = UPPER(value))
        /// </summary>
        InEqual,

        /// <summary>
        /// 대소문자 구분 없이 포함 (UPPER(column) LIKE '%' + UPPER(value) + '%')
        /// </summary>
        InLike,

        /// <summary>
        /// 대소문자 구분 없이 시작 (UPPER(column) LIKE UPPER(value) + '%')
        /// </summary>
        InStartWith,

        /// <summary>
        /// 대소문자 구분 없이 끝남 (UPPER(column) LIKE '%' + UPPER(value))
        /// </summary>
        InEndWith
    }
}