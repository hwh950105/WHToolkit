using Microsoft.Data.SqlClient;

namespace WHToolkit.Database.Extensions
{
    /// <summary>
    /// 데이터베이스 관련 확장 메서드를 제공합니다.
    /// </summary>
    public static class DatabaseExtensions
    {
        /// <summary>
        /// SqlErrorCollection에서 특정 에러 번호가 포함되어 있는지 확인합니다.
        /// </summary>
        /// <param name="errors">SQL 에러 컬렉션</param>
        /// <param name="number">확인할 에러 번호</param>
        /// <returns>에러 번호가 존재하면 true, 그렇지 않으면 false</returns>
        public static bool HasNumber(this SqlErrorCollection errors, int number)
        {
            foreach (SqlError error in errors)
            {
                if (error.Number == number) return true;
            }

            return false;
        }
    }
}