using System.Text.Json;
using System.Text.Json.Serialization;

namespace WHToolkit.IO
{
    /// <summary>
    /// JSON 파일 읽기/쓰기를 위한 헬퍼 클래스
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// 기본 JSON 직렬화 옵션 (들여쓰기, 유니코드 허용)
        /// </summary>
        public static JsonSerializerOptions DefaultOptions { get; } = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters = { new JsonStringEnumConverter() }
        };

        /// <summary>
        /// JSON 파일을 읽어서 객체로 변환
        /// </summary>
        /// <typeparam name="T">변환할 타입</typeparam>
        /// <param name="filePath">JSON 파일 경로</param>
        /// <param name="options">JSON 직렬화 옵션 (null이면 기본 옵션 사용)</param>
        /// <returns>역직렬화된 객체</returns>
        public static async Task<T?> ReadAsync<T>(string filePath, JsonSerializerOptions? options = null)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"파일을 찾을 수 없습니다: {filePath}");
            }

            using var stream = File.OpenRead(filePath);
            return await JsonSerializer.DeserializeAsync<T>(stream, options ?? DefaultOptions);
        }

        /// <summary>
        /// JSON 파일을 동기적으로 읽어서 객체로 변환
        /// </summary>
        /// <typeparam name="T">변환할 타입</typeparam>
        /// <param name="filePath">JSON 파일 경로</param>
        /// <param name="options">JSON 직렬화 옵션 (null이면 기본 옵션 사용)</param>
        /// <returns>역직렬화된 객체</returns>
        public static T? Read<T>(string filePath, JsonSerializerOptions? options = null)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"파일을 찾을 수 없습니다: {filePath}");
            }

            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(json, options ?? DefaultOptions);
        }

        /// <summary>
        /// 객체를 JSON 파일로 저장
        /// </summary>
        /// <typeparam name="T">저장할 객체의 타입</typeparam>
        /// <param name="filePath">저장할 파일 경로</param>
        /// <param name="data">저장할 객체</param>
        /// <param name="options">JSON 직렬화 옵션 (null이면 기본 옵션 사용)</param>
        public static async Task WriteAsync<T>(string filePath, T data, JsonSerializerOptions? options = null)
        {
            // 디렉토리가 없으면 생성
            string? directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using var stream = File.Create(filePath);
            await JsonSerializer.SerializeAsync(stream, data, options ?? DefaultOptions);
        }

        /// <summary>
        /// 객체를 동기적으로 JSON 파일로 저장
        /// </summary>
        /// <typeparam name="T">저장할 객체의 타입</typeparam>
        /// <param name="filePath">저장할 파일 경로</param>
        /// <param name="data">저장할 객체</param>
        /// <param name="options">JSON 직렬화 옵션 (null이면 기본 옵션 사용)</param>
        public static void Write<T>(string filePath, T data, JsonSerializerOptions? options = null)
        {
            // 디렉토리가 없으면 생성
            string? directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string json = JsonSerializer.Serialize(data, options ?? DefaultOptions);
            File.WriteAllText(filePath, json);
        }

        /// <summary>
        /// 객체를 JSON 문자열로 변환
        /// </summary>
        /// <typeparam name="T">변환할 객체의 타입</typeparam>
        /// <param name="data">변환할 객체</param>
        /// <param name="options">JSON 직렬화 옵션 (null이면 기본 옵션 사용)</param>
        /// <returns>JSON 문자열</returns>
        public static string Serialize<T>(T data, JsonSerializerOptions? options = null)
        {
            return JsonSerializer.Serialize(data, options ?? DefaultOptions);
        }

        /// <summary>
        /// JSON 문자열을 객체로 변환
        /// </summary>
        /// <typeparam name="T">변환할 타입</typeparam>
        /// <param name="json">JSON 문자열</param>
        /// <param name="options">JSON 직렬화 옵션 (null이면 기본 옵션 사용)</param>
        /// <returns>역직렬화된 객체</returns>
        public static T? Deserialize<T>(string json, JsonSerializerOptions? options = null)
        {
            return JsonSerializer.Deserialize<T>(json, options ?? DefaultOptions);
        }

        /// <summary>
        /// JSON 파일이 존재하는지 확인
        /// </summary>
        /// <param name="filePath">확인할 파일 경로</param>
        /// <returns>파일 존재 여부</returns>
        public static bool Exists(string filePath)
        {
            return File.Exists(filePath);
        }

        /// <summary>
        /// JSON 파일 삭제
        /// </summary>
        /// <param name="filePath">삭제할 파일 경로</param>
        public static void Delete(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        /// <summary>
        /// JSON 파일을 읽어서 객체로 변환, 파일이 없으면 기본값 반환
        /// </summary>
        /// <typeparam name="T">변환할 타입</typeparam>
        /// <param name="filePath">JSON 파일 경로</param>
        /// <param name="defaultValue">파일이 없을 때 반환할 기본값</param>
        /// <param name="options">JSON 직렬화 옵션 (null이면 기본 옵션 사용)</param>
        /// <returns>역직렬화된 객체 또는 기본값</returns>
        public static async Task<T> ReadOrDefaultAsync<T>(string filePath, T defaultValue, JsonSerializerOptions? options = null)
        {
            if (!File.Exists(filePath))
            {
                return defaultValue;
            }

            try
            {
                var result = await ReadAsync<T>(filePath, options);
                return result ?? defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// JSON 파일을 읽어서 객체로 변환, 파일이 없으면 기본값 생성 후 저장
        /// </summary>
        /// <typeparam name="T">변환할 타입</typeparam>
        /// <param name="filePath">JSON 파일 경로</param>
        /// <param name="defaultValue">파일이 없을 때 생성할 기본값</param>
        /// <param name="options">JSON 직렬화 옵션 (null이면 기본 옵션 사용)</param>
        /// <returns>역직렬화된 객체 또는 기본값</returns>
        public static async Task<T> ReadOrCreateAsync<T>(string filePath, T defaultValue, JsonSerializerOptions? options = null)
        {
            if (!File.Exists(filePath))
            {
                await WriteAsync(filePath, defaultValue, options);
                return defaultValue;
            }

            try
            {
                var result = await ReadAsync<T>(filePath, options);
                return result ?? defaultValue;
            }
            catch
            {
                await WriteAsync(filePath, defaultValue, options);
                return defaultValue;
            }
        }
    }
}
