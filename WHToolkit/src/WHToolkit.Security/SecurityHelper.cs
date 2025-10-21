using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WHToolkit.Security
{
    /// <summary>
    /// 보안 관련 암호화 및 해시 기능을 제공하는 헬퍼 클래스
    /// </summary>
    public static class SecurityHelper
    {
        //32바이트로 고정
        private static string key_salt = "HWH_2@25_1@3$5^7*9)".PadRight(32, '0');

        /// <summary>
        /// MD5 해시 알고리즘을 사용하여 입력 문자열을 암호화합니다.
        /// 솔트(salt)가 자동으로 추가되어 보안성을 강화합니다.
        /// </summary>
        /// <param name="input">암호화할 원본 문자열</param>
        /// <returns>MD5 해시로 변환된 16진수 문자열</returns>
        /// <remarks>
        /// 주의: MD5는 더 이상 보안에 안전하지 않은 알고리즘이므로,
        /// 보안이 중요한 경우 SHA256 이상의 알고리즘 사용을 권장합니다.
        /// </remarks>
        public static string ComputeMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input + key_salt);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// AES 알고리즘을 사용하여 평문을 암호화합니다.
        /// 암호화된 결과는 Base64 문자열로 반환됩니다.
        /// </summary>
        /// <param name="plainText">암호화할 평문 문자열</param>
        /// <returns>Base64로 인코딩된 암호화 문자열</returns>
        /// <remarks>
        /// AES-256 암호화를 사용하며, IV는 0으로 초기화됩니다.
        /// 운영 환경에서는 랜덤 IV 사용을 권장합니다.
        /// </remarks>
        public static string EncryptAES(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key_salt);
                aes.IV = new byte[16];

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (var sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        /// <summary>
        /// AES 알고리즘을 사용하여 암호화된 문자열을 복호화합니다.
        /// Base64로 인코딩되지 않은 문자열은 그대로 반환됩니다.
        /// </summary>
        /// <param name="cipherText">복호화할 암호화 문자열 (Base64 형식)</param>
        /// <returns>복호화된 평문 문자열</returns>
        /// <exception cref="Exception">복호화 실패 시 예외 발생</exception>
        /// <remarks>
        /// Base64 형식이 아닌 경우 원본 문자열을 그대로 반환합니다.
        /// </remarks>
        public static string DecryptAES(string cipherText)
        {
            try
            {

                if (!IsBase64String(cipherText))
                {
                    return cipherText;
                }

                using (Aes aes = Aes.Create())
                {
                    // 키와 IV 초기화
                    aes.Key = Encoding.UTF8.GetBytes(key_salt.PadRight(32, '0'));
                    aes.IV = new byte[16];


                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (var ms = new MemoryStream(Convert.FromBase64String(cipherText)))
                    {
                        using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                        {
                            using (var sr = new StreamReader(cs))
                            {
                                return sr.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 문자열이 유효한 Base64 형식인지 확인합니다.
        /// </summary>
        /// <param name="base64">검사할 문자열</param>
        /// <returns>유효한 Base64 형식이면 true, 그렇지 않으면 false</returns>
        /// <remarks>
        /// 빈 문자열이나 null인 경우 false를 반환합니다.
        /// </remarks>
        public static bool IsBase64String(string base64)
        {
            if (string.IsNullOrEmpty(base64))
                return false;

            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out _);
        }
    }
}
