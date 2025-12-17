using hwh.Controls.TrendChartControl;
using hwh.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WHToolkit.Security;
using WHToolkit.src.Database;

namespace hwh
{
    static public class DbCall
    {
        private static string GetConnectionString()
        {
            return System.IO.Directory.GetCurrentDirectory() + "\\example.db";
        }

        /// <summary>
        /// 사용자 로그인
        /// </summary>
        public static bool GetLogin(LoginRequest loginRequest)
        {
            try
            {
                using (DbHelperLite db = new DbHelperLite(GetConnectionString()))
                {
                    string query = $"SELECT * FROM TB_USER WHERE USERNAME = '{loginRequest.Username}' AND PASSWORD = '{loginRequest.Password}' AND STATUS = 'ACTIVE'";
                    var users = db.ExecuteList<UserModel>(query);
                    return users.Count > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 사용자 회원가입
        /// </summary>
        public static bool RegisterUser(RegisterRequest registerRequest)
        {
            try
            {
                using (DbHelperLite db = new DbHelperLite(GetConnectionString()))
                {
                    // 중복 확인
                    string checkQuery = $"SELECT COUNT(*) FROM TB_USER WHERE USERNAME = '{registerRequest.Username}'";
                    var countObj = db.ExecuteScalar(checkQuery);
                    int count = countObj != null ? Convert.ToInt32(countObj) : 0;
                    
                    if (count > 0)
                    {
                        return false; // 이미 존재하는 아이디
                    }

                    // 회원가입
                    string email = string.IsNullOrEmpty(registerRequest.Email) ? "NULL" : $"'{registerRequest.Email}'";
                    string phone = string.IsNullOrEmpty(registerRequest.Phone) ? "NULL" : $"'{registerRequest.Phone}'";

                   


                    string insertQuery = $@"
                        INSERT INTO TB_USER (USERNAME, PASSWORD, NAME, EMAIL, PHONE, ROLE, STATUS, CREATED_AT, UPDATED_AT)
                        VALUES ('{registerRequest.Username}', '{registerRequest.Password}', '{registerRequest.Name}', {email}, {phone}, 'USER', 'ACTIVE', 
                                datetime('now', 'localtime'), datetime('now', 'localtime'))";

                    int rowsAffected = db.ExecuteNonQuery(insertQuery);
                    return rowsAffected > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 사용자 정보 조회
        /// </summary>
        public static UserModel? GetUserByUsername(string username)
        {
            try
            {
                using (DbHelperLite db = new DbHelperLite(GetConnectionString()))
                {
                    string query = $"SELECT * FROM TB_USER WHERE USERNAME = '{username}' AND STATUS = 'ACTIVE'";
                    var users = db.ExecuteList<UserModel>(query);
                    return users.FirstOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 모든 사용자 목록 조회
        /// </summary>
        public static List<UserModel> GetAllUsers()
        {
            try
            {
                using (DbHelperLite db = new DbHelperLite(GetConnectionString()))
                {
                    string query = "SELECT * FROM TB_USER where STATUS = 'ACTIVE' ORDER BY CREATED_AT asc";
                    return db.ExecuteList<UserModel>(query);
                }
            }
            catch (Exception)
            {
                return new List<UserModel>();
            }
        }

        /// <summary>
        /// 모든 사용자 목록 DataTable로 조회 (DataGridView 바인딩용)
        /// </summary>
        public static DataTable GetAllUsersAsDataTable()
        {
            try
            {
                using (DbHelperLite db = new DbHelperLite(GetConnectionString()))
                {
                    string query = @"
                        SELECT 
                            USER_ID as 'ID', 
                            USERNAME as '아이디', 
                            NAME as '이름', 
                            EMAIL as '이메일', 
                            PHONE as '연락처', 
                            ROLE as '권한', 
                            CASE STATUS 
                                WHEN 'ACTIVE' THEN '활성'
                                WHEN 'SUSPENDED' THEN '정지됨'
                                WHEN 'DELETED' THEN '삭제됨'
                                ELSE '알 수 없음'
                            END as '상태',
                            strftime('%Y-%m-%d %H:%M', CREATED_AT) as '가입일'
                        FROM TB_USER 
                        ORDER BY USER_ID asc";
                    
                    return db.ExecuteDataTable(query);
                }
            }
            catch (Exception)
            {
                return new DataTable();
            }
        }

        /// <summary>
        /// 사용자 삭제 (상태 변경)
        /// </summary>
                public static bool DeleteUser(int userId)
                {
                    try
                    {
                        using (DbHelperLite db = new DbHelperLite(GetConnectionString()))
                        {
                            string query = $"UPDATE TB_USER SET STATUS = 'DELETED', UPDATED_AT = datetime('now', 'localtime') WHERE USER_ID = {userId}";
                            int rowsAffected = db.ExecuteNonQuery(query);
                            return rowsAffected > 0;
                        }
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }

                public static bool UpdateUser(int userId, string name, string? email, string? phone, string? newPassword)
                {
                    try
                    {
                        using (DbHelperLite db = new DbHelperLite(GetConnectionString()))
                        {
                            string emailValue = string.IsNullOrEmpty(email) ? "NULL" : $"'{email}'";
                            string phoneValue = string.IsNullOrEmpty(phone) ? "NULL" : $"'{phone}'";
                            
                            string query;
                            if (!string.IsNullOrEmpty(newPassword))
                            {
                                // 비밀번호도 함께 변경
                                query = $@"
                                    UPDATE TB_USER 
                                    SET NAME = '{name}', 
                                        EMAIL = {emailValue}, 
                                        PHONE = {phoneValue},
                                        PASSWORD = '{newPassword}',
                                        UPDATED_AT = datetime('now', 'localtime')
                                    WHERE USER_ID = {userId}";
                            }
                            else
                            {
                                // 비밀번호는 변경하지 않음
                                query = $@"
                                    UPDATE TB_USER 
                                    SET NAME = '{name}', 
                                        EMAIL = {emailValue}, 
                                        PHONE = {phoneValue},
                                        UPDATED_AT = datetime('now', 'localtime')
                                    WHERE USER_ID = {userId}";
                            }

                            int rowsAffected = db.ExecuteNonQuery(query);
                            return rowsAffected > 0;
                        }
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }

        /// <summary>
        /// 센서 타입 목록 조회 (중복 제거)
        /// </summary>
        public static List<string> GetSensorTypes()
        {
            try
            {
                using (DbHelperLite db = new DbHelperLite(GetConnectionString()))
                {
                    string query = "SELECT DISTINCT sensor_type FROM sensor_data ORDER BY sensor_type";
                    DataTable dt = db.ExecuteDataTable(query);
                    return dt.AsEnumerable().Select(row => row.Field<string>("sensor_type")!).ToList();
                }
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }

        /// <summary>
        /// 특정 센서 타입의 최신 값 조회
        /// </summary>
        public static double GetLatestSensorValue(string sensorType)
        {
            try
            {
                using (DbHelperLite db = new DbHelperLite(GetConnectionString()))
                {
                    string query = $"SELECT value FROM sensor_data WHERE sensor_type = '{sensorType}' ORDER BY created_at DESC LIMIT 1";
                    var result = db.ExecuteScalar(query);
                    return result != null ? Convert.ToDouble(result) : 0.0;
                }
            }
            catch (Exception)
            {
                return 0.0;
            }
        }

        /// <summary>
        /// 특정 센서 타입의 시간 범위별 데이터 조회
        /// </summary>
        public static DataTable GetSensorDataByTimeRange(string sensorType, DateTime startTime, DateTime endTime)
        {
            try
            {
                using (DbHelperLite db = new DbHelperLite(GetConnectionString()))
                {
                    string startStr = startTime.ToString("yyyy-MM-dd HH:mm:ss");
                    string endStr = endTime.ToString("yyyy-MM-dd HH:mm:ss");
                    
                    string query = $@"
                        SELECT created_at, value 
                        FROM sensor_data 
                        WHERE sensor_type = '{sensorType}' 
                          AND created_at BETWEEN '{startStr}' AND '{endStr}'
                        ORDER BY created_at ASC";
                    
                    return db.ExecuteDataTable(query);
                }
            }
            catch (Exception)
            {
                return new DataTable();
            }
        }

        public static List<DataPoint> GetSensorDataByTimeRangelist(string sensorType, DateTime startTime, DateTime endTime)
        {
            try
            {
                using (DbHelperLite db = new DbHelperLite(GetConnectionString()))
                {
                    string startStr = startTime.ToString("yyyy-MM-dd HH:mm:ss");
                    string endStr = endTime.ToString("yyyy-MM-dd HH:mm:ss");

                    string query = $@"
                        SELECT created_at as Timestamp, value as Value
                        FROM sensor_data 
                        WHERE sensor_type = '{sensorType}' 
                          AND created_at BETWEEN '{startStr}' AND '{endStr}'
                        ORDER BY created_at ASC";

                     var _2 = db.ExecuteDataTable(query);
                     var _1 = db.ExecuteDataSet(query);

                    return db.ExecuteList<DataPoint>(query);
                }
            }
            catch (Exception)
            {
                return new List<DataPoint>();
            }
        }


        /// <summary>
        /// 특정 센서 타입의 단위 조회
        /// </summary>
        public static string GetSensorUnit(string sensorType)
        {
            try
            {
                using (DbHelperLite db = new DbHelperLite(GetConnectionString()))
                {
                    string query = $"SELECT unit FROM sensor_data WHERE sensor_type = '{sensorType}' LIMIT 1";
                    var result = db.ExecuteScalar(query);
                    return result?.ToString() ?? "";
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// 센서 데이터 전체 조회 (DataGridView 바인딩용)
        /// </summary>
        public static DataTable GetAllSensorDataAsDataTable()
        {
            try
            {
                using (DbHelperLite db = new DbHelperLite(GetConnectionString()))
                {
                    string query = @"
                        SELECT 
                            id as 'ID',
                            sensor_id as '센서ID',
                            sensor_type as '센서타입',
                            value as '측정값',
                            unit as '단위',
                            strftime('%Y-%m-%d %H:%M:%S', created_at) as '측정시각',
                            status as '상태'
                        FROM sensor_data 
                        ORDER BY created_at DESC
                        LIMIT 1000";
                    
                    return db.ExecuteDataTable(query);
                }
            }
            catch (Exception)
            {
                return new DataTable();
            }
        }

    }
}

