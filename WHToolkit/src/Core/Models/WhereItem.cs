namespace WHToolkit.Database.Models
{
    /// <summary>
    /// WHERE 조건절의 개별 항목을 나타내는 클래스입니다.
    /// </summary>
    public class WhereItem
    {
        /// <summary>
        /// 비교 연산자
        /// </summary>
        public ComparisonOperator Operator { get; set; } = ComparisonOperator.Equal;

        /// <summary>
        /// 컬럼 이름
        /// </summary>
        public string ColumnName { get; set; } = string.Empty;

        /// <summary>
        /// 속성 이름
        /// </summary>
        public string PropName { get; set; } = string.Empty;

        /// <summary>
        /// 비교 값
        /// </summary>
        public object? Value { get; set; }

        /// <summary>
        /// WHERE 조건을 SQL 문자열로 변환합니다 (값 직접 포함).
        /// </summary>
        /// <returns>SQL WHERE 조건 문자열</returns>
        [Obsolete("This method is vulnerable to SQL injection. Use ToString(paramName) with parameterized queries instead.", error: true)]
        public override string ToString()
        {
            var valueStr = Value?.ToString() ?? "NULL";
            var isString = Value?.GetType() == typeof(string);

            return Operator switch
            {
                ComparisonOperator.Equal => $"{ColumnName} = {FormatValue(valueStr, isString)}",
                ComparisonOperator.NotEqual => $"{ColumnName} != {FormatValue(valueStr, isString)}",
                ComparisonOperator.LessThan => $"{ColumnName} < {FormatValue(valueStr, isString)}",
                ComparisonOperator.GreaterThan => $"{ColumnName} > {FormatValue(valueStr, isString)}",
                ComparisonOperator.LessThanOrEqual => $"{ColumnName} <= {FormatValue(valueStr, isString)}",
                ComparisonOperator.GreaterThanOrEqual => $"{ColumnName} >= {FormatValue(valueStr, isString)}",
                ComparisonOperator.Like => $"{ColumnName} LIKE '%{valueStr}%'",
                ComparisonOperator.StartWith => $"{ColumnName} LIKE '{valueStr}%'",
                ComparisonOperator.EndWith => $"{ColumnName} LIKE '%{valueStr}'",
                ComparisonOperator.InEqual => $"UPPER({ColumnName}) = UPPER({FormatValue(valueStr, isString)})",
                ComparisonOperator.InLike => $"UPPER({ColumnName}) LIKE '%' + UPPER('{valueStr}') + '%'",
                ComparisonOperator.InStartWith => $"UPPER({ColumnName}) LIKE UPPER('{valueStr}') + '%'",
                ComparisonOperator.InEndWith => $"UPPER({ColumnName}) LIKE '%' + UPPER('{valueStr}')",
                _ => $"{ColumnName} = {FormatValue(valueStr, isString)}"
            };
        }

        /// <summary>
        /// WHERE 조건을 매개변수를 사용하는 SQL 문자열로 변환합니다.
        /// </summary>
        /// <param name="paramName">매개변수 이름</param>
        /// <returns>매개변수를 사용하는 SQL WHERE 조건 문자열</returns>
        public string ToString(string paramName)
        {
            return Operator switch
            {
                ComparisonOperator.Equal => $"{ColumnName} = {paramName}",
                ComparisonOperator.NotEqual => $"{ColumnName} != {paramName}",
                ComparisonOperator.LessThan => $"{ColumnName} < {paramName}",
                ComparisonOperator.GreaterThan => $"{ColumnName} > {paramName}",
                ComparisonOperator.LessThanOrEqual => $"{ColumnName} <= {paramName}",
                ComparisonOperator.GreaterThanOrEqual => $"{ColumnName} >= {paramName}",
                ComparisonOperator.Like => $"{ColumnName} LIKE '%' + {paramName} + '%'",
                ComparisonOperator.StartWith => $"{ColumnName} LIKE {paramName} + '%'",
                ComparisonOperator.EndWith => $"{ColumnName} LIKE '%' + {paramName}",
                ComparisonOperator.InEqual => $"UPPER({ColumnName}) = UPPER({paramName})",
                ComparisonOperator.InLike => $"UPPER({ColumnName}) LIKE '%' + UPPER({paramName}) + '%'",
                ComparisonOperator.InStartWith => $"UPPER({ColumnName}) LIKE UPPER({paramName}) + '%'",
                ComparisonOperator.InEndWith => $"UPPER({ColumnName}) LIKE '%' + UPPER({paramName})",
                _ => $"{ColumnName} = {paramName}"
            };
        }

        /// <summary>
        /// 값의 타입에 따라 적절한 형식으로 포맷합니다.
        /// </summary>
        /// <param name="value">포맷할 값</param>
        /// <param name="isString">문자열 타입 여부</param>
        /// <returns>포맷된 값</returns>
        private static string FormatValue(string value, bool isString)
        {
            if (value == "NULL") return "NULL";
            return isString ? $"'{value}'" : value;
        }
    }
}