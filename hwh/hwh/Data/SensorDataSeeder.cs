using System;
using System.Collections.Generic;
using WHToolkit.src.Database;

namespace hwh.Data
{
    /// <summary>
    /// sensor_data 테이블에 테스트 데이터를 생성하는 클래스
    /// </summary>
    public static class SensorDataSeeder
    {
        private static string GetConnectionString()
        {
            return System.IO.Directory.GetCurrentDirectory() + "\\example.db";
        }

        /// <summary>
        /// 센서 데이터 테이블 생성
        /// </summary>
        public static bool CreateSensorDataTable()
        {
            try
            {
                using (DbHelperLite db = new DbHelperLite(GetConnectionString()))
                {
                    string createTableQuery = @"
                        CREATE TABLE IF NOT EXISTS sensor_data (
                            id INTEGER PRIMARY KEY AUTOINCREMENT,
                            sensor_id TEXT NOT NULL,
                            sensor_type TEXT NOT NULL,
                            value REAL NOT NULL,
                            unit TEXT NOT NULL,
                            created_at DATETIME NOT NULL DEFAULT (datetime('now')),
                            status TEXT DEFAULT 'OK'
                        );";

                    db.ExecuteNonQuery(createTableQuery);

                    // 인덱스 생성 (성능 향상)
                    string createIndexQuery = @"
                        CREATE INDEX IF NOT EXISTS idx_sensor_data_type_time 
                        ON sensor_data(sensor_type, created_at);";

                    db.ExecuteNonQuery(createIndexQuery);

                    return true;
                }
            }
            catch (Exception ex)
            {
                Core.LogHelper.Error(ex, "센서 데이터 테이블 생성 실패");
                return false;
            }
        }

        /// <summary>
        /// 테스트 센서 데이터 생성 (최근 24시간)
        /// </summary>
        public static bool SeedTestData_old()
        {
            try
            {
                using (DbHelperLite db = new DbHelperLite(GetConnectionString()))
                {
                    // 기존 데이터 삭제 (선택사항)
                    // db.ExecuteNonQuery("DELETE FROM sensor_data");

                    var random = new Random();
                    DateTime now = DateTime.Now;
                    
                    // 센서 타입별 기준값 및 변동폭
                    var sensorConfigs = new Dictionary<string, (double baseValue, double range, string unit)>
                    {
                        { "temperature", (25.0, 10.0, "°C") },      // 온도: 20-30도
                        { "humidity", (60.0, 20.0, "%") },          // 습도: 50-70%
                        { "pressure", (1013.0, 20.0, "hPa") },      // 기압: 1003-1023 hPa
                        { "vibration", (0.5, 0.3, "mm/s") },        // 진동: 0.2-0.8 mm/s
                        { "flow", (150.0, 50.0, "L/min") }          // 유량: 125-175 L/min
                    };

                    // 최근 24시간 동안 5분 간격으로 데이터 생성
                    int totalPoints = 24 * 12; // 24시간 * 12 (5분 간격)
                    int insertedCount = 0;

                    foreach (var config in sensorConfigs)
                    {
                        string sensorType = config.Key;
                        double baseValue = config.Value.baseValue;
                        double range = config.Value.range;
                        string unit = config.Value.unit;
                        
                        // 센서 ID (센서 타입별로 2개씩)
                        string[] sensorIds = { $"{sensorType.ToUpper()}_01", $"{sensorType.ToUpper()}_02" };

                        foreach (string sensorId in sensorIds)
                        {
                            for (int i = totalPoints - 1; i >= 0; i--)
                            {
                                DateTime timestamp = now.AddMinutes(-i * 5);
                                
                                // 사인파 + 랜덤 노이즈로 자연스러운 변화 생성
                                double sineWave = Math.Sin(i * 0.1) * (range * 0.3);
                                double noise = (random.NextDouble() - 0.5) * (range * 0.4);
                                double value = baseValue + sineWave + noise;

                                // 정상 범위 내로 제한
                                value = Math.Max(baseValue - range / 2, Math.Min(baseValue + range / 2, value));

                                // 상태 결정 (대부분 OK, 간혹 WARN)
                                string status = random.NextDouble() > 0.95 ? "WARN" : "OK";

                                string insertQuery = $@"
                                    INSERT INTO sensor_data (sensor_id, sensor_type, value, unit, created_at, status)
                                    VALUES ('{sensorId}', '{sensorType}', {value:F2}, '{unit}', '{timestamp:yyyy-MM-dd HH:mm:ss}', '{status}')";

                                db.ExecuteNonQuery(insertQuery);
                                insertedCount++;
                            }
                        }
                    }

                    System.Diagnostics.Debug.WriteLine($"센서 데이터 {insertedCount}개 생성 완료!");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Core.LogHelper.Error(ex, "테스트 데이터 생성 실패");
                return false;
            }
        }


        public static bool SeedTestData()
        {
            try
            {
                using (DbHelperLite db = new DbHelperLite(GetConnectionString()))
                {
                    var random = new Random();

                    // 현재 시간 (로컬 KST 기준)
                    DateTime now = DateTime.Now;

                    // 총 데이터 포인트: 10초 단위 * 2주 = 120,960건
                    int totalPoints = 1000;

                    for (int x = 0; x < totalPoints; x++)
                    {
                        // x * 10초 전 시간
                        DateTime timestamp = now.AddSeconds(-x * 10);

                        // sensor_type 매칭
                        int typeIdx = random.Next(0, 4);
                        string sensorType = typeIdx switch
                        {
                            0 => "temperature",
                            1 => "humidity",
                            2 => "vibration",
                            3 => "co2",
                            _ => "temperature"
                        };

                        // type에 따라 단위 + 값 생성
                        string unit;
                        double value;

                        switch (sensorType)
                        {
                            case "temperature":
                                // 20.0 ~ 35.0°C
                                unit = "C";
                                value = 20.0 + (random.NextDouble() * 15.0); // 15.0 범위
                                break;

                            case "humidity":
                                // 40 ~ 70%
                                unit = "%";
                                value = 40.0 + (random.NextDouble() * 30.0);
                                break;

                            case "vibration":
                                // 0.01 ~ 0.08 mm/s
                                unit = "mm/s";
                                value = 0.01 + (random.NextDouble() * 0.07);
                                break;

                            case "co2":
                                // 400 ~ 900 ppm
                                unit = "ppm";
                                value = 400 + random.Next(0, 500);
                                break;

                            default:
                                unit = "";
                                value = 0;
                                break;
                        }

                        string insertQuery = $@"
                    INSERT INTO sensor_data (sensor_id, sensor_type, value, unit, created_at, status)
                    VALUES (
                        'SENSOR_01',
                        '{sensorType}',
                        {value:F2},
                        '{unit}',
                        '{timestamp:yyyy-MM-dd HH:mm:ss}',
                        'OK'
                    );";

                        db.ExecuteNonQuery(insertQuery);
                    }

                 //   System.Diagnostics.Debug.WriteLine($"센서 데이터 {totalPoints}개 생성 완료!");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Core.LogHelper.Error(ex, "테스트 데이터 Seed 실패");
                return false;
            }
        }




        /// <summary>
        /// 실시간 데이터 추가 (시뮬레이션용)
        /// </summary>
        public static bool AddRealtimeData()
        {
            try
            {
                using (DbHelperLite db = new DbHelperLite(GetConnectionString()))
                {
                    var random = new Random();
                    DateTime now = DateTime.Now;

                    var sensorConfigs = new Dictionary<string, (double baseValue, double range, string unit)>
                    {
                        { "temperature", (25.0, 10.0, "°C") },
                        { "humidity", (60.0, 20.0, "%") },
                        { "pressure", (1013.0, 20.0, "hPa") },
                        { "vibration", (0.5, 0.3, "mm/s") },
                        { "flow", (150.0, 50.0, "L/min") }
                    };

                    foreach (var config in sensorConfigs)
                    {
                        string sensorType = config.Key;
                        double baseValue = config.Value.baseValue;
                        double range = config.Value.range;
                        string unit = config.Value.unit;

                        string[] sensorIds = { $"{sensorType.ToUpper()}_01", $"{sensorType.ToUpper()}_02" };

                        foreach (string sensorId in sensorIds)
                        {
                            // 최근 값에서 조금씩 변화
                            double change = (random.NextDouble() - 0.5) * (range * 0.1);
                            double value = baseValue + change;
                            
                            string status = random.NextDouble() > 0.95 ? "WARN" : "OK";

                            string insertQuery = $@"
                                INSERT INTO sensor_data (sensor_id, sensor_type, value, unit, created_at, status)
                                VALUES ('{sensorId}', '{sensorType}', {value:F2}, '{unit}', '{now:yyyy-MM-dd HH:mm:ss}', '{status}')";

                            db.ExecuteNonQuery(insertQuery);
                        }
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                Core.LogHelper.Error(ex, "실시간 데이터 추가 실패");
                return false;
            }
        }

        /// <summary>
        /// 데이터 개수 확인
        /// </summary>
        public static int SetdeleteData()
        {
            try
            {
                using (DbHelperLite db = new DbHelperLite(GetConnectionString()))
                {
                    string query = "delete FROM sensor_data";
                    var result = db.ExecuteScalar(query);
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
            catch (Exception ex)
            {
                Core.LogHelper.Error(ex, "센서 데이터 삭제 실패");
                return 0;
            }
        }

        /// <summary>
        /// 초기화 (테이블 생성 + 테스트 데이터 생성)
        /// </summary>
        public static bool Initialize()
        {
            try
            {
                // 1. 테이블 생성
                if (!CreateSensorDataTable())
                {
                    Core.LogHelper.Error("센서 데이터 테이블 생성 실패");
                    return false;
                }
                else
                {
                    SetdeleteData();

                }



                if (!SeedTestData())
                {
                    Core.LogHelper.Error("테스트 데이터 Seed 실패");
                    return false;
                }

           

                return true;
            }
            catch (Exception ex)
            {
                Core.LogHelper.Error(ex, "센서 데이터 초기화 실패");
                return false;
            }
        }
    }
}

