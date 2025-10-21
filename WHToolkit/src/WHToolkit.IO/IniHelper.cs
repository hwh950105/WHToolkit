using System.Runtime.InteropServices;
using System.Text;

namespace WHToolkit.IO
{
    /// <summary>
    /// INI 파일 읽기/쓰기를 위한 헬퍼 클래스
    /// </summary>
    public class IniHelper
    {
        private readonly string _filePath;

        // Windows API 사용
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(
            string section,
            string key,
            string defaultValue,
            StringBuilder returnValue,
            int size,
            string filePath);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileSection(
            string section,
            byte[] returnValue,
            int size,
            string filePath);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern int WritePrivateProfileString(
            string section,
            string key,
            string value,
            string filePath);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileSectionNames(
            byte[] returnValue,
            int size,
            string filePath);

        /// <summary>
        /// IniHelper 생성자
        /// </summary>
        /// <param name="filePath">INI 파일 경로</param>
        public IniHelper(string filePath)
        {
            _filePath = Path.GetFullPath(filePath);

            // 디렉토리가 없으면 생성
            string? directory = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        /// <summary>
        /// INI 파일 경로
        /// </summary>
        public string FilePath => _filePath;

        /// <summary>
        /// INI 파일이 존재하는지 확인
        /// </summary>
        public bool Exists => File.Exists(_filePath);

        /// <summary>
        /// 값 읽기
        /// </summary>
        /// <param name="section">섹션 이름</param>
        /// <param name="key">키 이름</param>
        /// <param name="defaultValue">기본값 (키가 없을 때 반환)</param>
        /// <returns>읽은 값</returns>
        public string Read(string section, string key, string defaultValue = "")
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var sb = new StringBuilder(1024);
                GetPrivateProfileString(section, key, defaultValue, sb, sb.Capacity, _filePath);
                return sb.ToString();
            }
            else
            {
                // Linux/Mac에서는 수동 파싱
                return ReadManual(section, key, defaultValue);
            }
        }

        /// <summary>
        /// 정수값 읽기
        /// </summary>
        /// <param name="section">섹션 이름</param>
        /// <param name="key">키 이름</param>
        /// <param name="defaultValue">기본값</param>
        /// <returns>읽은 정수값</returns>
        public int ReadInt(string section, string key, int defaultValue = 0)
        {
            string value = Read(section, key, defaultValue.ToString());
            return int.TryParse(value, out int result) ? result : defaultValue;
        }

        /// <summary>
        /// 실수값 읽기
        /// </summary>
        /// <param name="section">섹션 이름</param>
        /// <param name="key">키 이름</param>
        /// <param name="defaultValue">기본값</param>
        /// <returns>읽은 실수값</returns>
        public double ReadDouble(string section, string key, double defaultValue = 0.0)
        {
            string value = Read(section, key, defaultValue.ToString());
            return double.TryParse(value, out double result) ? result : defaultValue;
        }

        /// <summary>
        /// 불린값 읽기
        /// </summary>
        /// <param name="section">섹션 이름</param>
        /// <param name="key">키 이름</param>
        /// <param name="defaultValue">기본값</param>
        /// <returns>읽은 불린값</returns>
        public bool ReadBool(string section, string key, bool defaultValue = false)
        {
            string value = Read(section, key, defaultValue.ToString()).ToLower();
            return value == "true" || value == "1" || value == "yes" || value == "on";
        }

        /// <summary>
        /// 값 쓰기
        /// </summary>
        /// <param name="section">섹션 이름</param>
        /// <param name="key">키 이름</param>
        /// <param name="value">저장할 값</param>
        public void Write(string section, string key, string value)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WritePrivateProfileString(section, key, value, _filePath);
            }
            else
            {
                // Linux/Mac에서는 수동 쓰기
                WriteManual(section, key, value);
            }
        }

        /// <summary>
        /// 정수값 쓰기
        /// </summary>
        /// <param name="section">섹션 이름</param>
        /// <param name="key">키 이름</param>
        /// <param name="value">저장할 정수값</param>
        public void Write(string section, string key, int value)
        {
            Write(section, key, value.ToString());
        }

        /// <summary>
        /// 실수값 쓰기
        /// </summary>
        /// <param name="section">섹션 이름</param>
        /// <param name="key">키 이름</param>
        /// <param name="value">저장할 실수값</param>
        public void Write(string section, string key, double value)
        {
            Write(section, key, value.ToString());
        }

        /// <summary>
        /// 불린값 쓰기
        /// </summary>
        /// <param name="section">섹션 이름</param>
        /// <param name="key">키 이름</param>
        /// <param name="value">저장할 불린값</param>
        public void Write(string section, string key, bool value)
        {
            Write(section, key, value ? "true" : "false");
        }

        /// <summary>
        /// 키 삭제
        /// </summary>
        /// <param name="section">섹션 이름</param>
        /// <param name="key">삭제할 키 이름</param>
        public void DeleteKey(string section, string key)
        {
            Write(section, key, null!);
        }

        /// <summary>
        /// 섹션 삭제
        /// </summary>
        /// <param name="section">삭제할 섹션 이름</param>
        public void DeleteSection(string section)
        {
            Write(section, null!, null!);
        }

        /// <summary>
        /// 섹션의 모든 키 가져오기
        /// </summary>
        /// <param name="section">섹션 이름</param>
        /// <returns>키 목록</returns>
        public List<string> GetKeys(string section)
        {
            var keys = new List<string>();

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                byte[] buffer = new byte[32768];
                GetPrivateProfileSection(section, buffer, buffer.Length, _filePath);

                string result = Encoding.Unicode.GetString(buffer);
                string[] lines = result.Split('\0');

                foreach (string line in lines)
                {
                    if (string.IsNullOrEmpty(line)) break;
                    int index = line.IndexOf('=');
                    if (index > 0)
                    {
                        keys.Add(line.Substring(0, index).Trim());
                    }
                }
            }
            else
            {
                keys = GetKeysManual(section);
            }

            return keys;
        }

        /// <summary>
        /// 모든 섹션 이름 가져오기
        /// </summary>
        /// <returns>섹션 이름 목록</returns>
        public List<string> GetSections()
        {
            var sections = new List<string>();

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                byte[] buffer = new byte[32768];
                GetPrivateProfileSectionNames(buffer, buffer.Length, _filePath);

                string result = Encoding.Unicode.GetString(buffer);
                string[] lines = result.Split('\0');

                foreach (string line in lines)
                {
                    if (string.IsNullOrEmpty(line)) break;
                    sections.Add(line);
                }
            }
            else
            {
                sections = GetSectionsManual();
            }

            return sections;
        }

        /// <summary>
        /// 섹션의 모든 키-값 쌍 가져오기
        /// </summary>
        /// <param name="section">섹션 이름</param>
        /// <returns>키-값 딕셔너리</returns>
        public Dictionary<string, string> GetSection(string section)
        {
            var result = new Dictionary<string, string>();
            var keys = GetKeys(section);

            foreach (var key in keys)
            {
                result[key] = Read(section, key);
            }

            return result;
        }

        /// <summary>
        /// INI 파일 삭제
        /// </summary>
        public void Delete()
        {
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }

        #region Manual Implementation for Linux/Mac

        private string ReadManual(string section, string key, string defaultValue)
        {
            if (!File.Exists(_filePath))
                return defaultValue;

            var lines = File.ReadAllLines(_filePath);
            bool inSection = false;

            foreach (var line in lines)
            {
                string trimmed = line.Trim();

                if (trimmed.StartsWith($"[{section}]", StringComparison.OrdinalIgnoreCase))
                {
                    inSection = true;
                    continue;
                }

                if (inSection)
                {
                    if (trimmed.StartsWith("["))
                        break;

                    int index = trimmed.IndexOf('=');
                    if (index > 0)
                    {
                        string lineKey = trimmed.Substring(0, index).Trim();
                        if (lineKey.Equals(key, StringComparison.OrdinalIgnoreCase))
                        {
                            return trimmed.Substring(index + 1).Trim();
                        }
                    }
                }
            }

            return defaultValue;
        }

        private void WriteManual(string section, string key, string value)
        {
            var lines = File.Exists(_filePath) ? File.ReadAllLines(_filePath).ToList() : new List<string>();
            bool sectionFound = false;
            bool keyFound = false;
            int sectionIndex = -1;

            for (int i = 0; i < lines.Count; i++)
            {
                string trimmed = lines[i].Trim();

                if (trimmed.StartsWith($"[{section}]", StringComparison.OrdinalIgnoreCase))
                {
                    sectionFound = true;
                    sectionIndex = i;
                    continue;
                }

                if (sectionFound)
                {
                    if (trimmed.StartsWith("["))
                    {
                        if (!keyFound)
                        {
                            lines.Insert(i, $"{key}={value}");
                            keyFound = true;
                        }
                        break;
                    }

                    int index = trimmed.IndexOf('=');
                    if (index > 0)
                    {
                        string lineKey = trimmed.Substring(0, index).Trim();
                        if (lineKey.Equals(key, StringComparison.OrdinalIgnoreCase))
                        {
                            lines[i] = $"{key}={value}";
                            keyFound = true;
                            break;
                        }
                    }
                }
            }

            if (!sectionFound)
            {
                lines.Add($"[{section}]");
                lines.Add($"{key}={value}");
            }
            else if (!keyFound)
            {
                lines.Insert(sectionIndex + 1, $"{key}={value}");
            }

            File.WriteAllLines(_filePath, lines);
        }

        private List<string> GetKeysManual(string section)
        {
            var keys = new List<string>();
            if (!File.Exists(_filePath))
                return keys;

            var lines = File.ReadAllLines(_filePath);
            bool inSection = false;

            foreach (var line in lines)
            {
                string trimmed = line.Trim();

                if (trimmed.StartsWith($"[{section}]", StringComparison.OrdinalIgnoreCase))
                {
                    inSection = true;
                    continue;
                }

                if (inSection)
                {
                    if (trimmed.StartsWith("["))
                        break;

                    int index = trimmed.IndexOf('=');
                    if (index > 0)
                    {
                        keys.Add(trimmed.Substring(0, index).Trim());
                    }
                }
            }

            return keys;
        }

        private List<string> GetSectionsManual()
        {
            var sections = new List<string>();
            if (!File.Exists(_filePath))
                return sections;

            var lines = File.ReadAllLines(_filePath);

            foreach (var line in lines)
            {
                string trimmed = line.Trim();
                if (trimmed.StartsWith("[") && trimmed.EndsWith("]"))
                {
                    sections.Add(trimmed.Substring(1, trimmed.Length - 2));
                }
            }

            return sections;
        }

        #endregion
    }
}
