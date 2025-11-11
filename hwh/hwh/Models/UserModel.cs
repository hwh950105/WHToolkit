namespace hwh.Models
{
    /// <summary>
    /// 사용자 정보 모델 (TB_USER 테이블)
    /// </summary>
    public class UserModel
    {
        public int USER_ID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string Role { get; set; } = "USER";
        public string Status { get; set; } = "ACTIVE";
        public DateTime CREATED_AT { get; set; }
        public DateTime UPDATED_AT { get; set; }
    }

    /// <summary>
    /// 로그인 요청 모델
    /// </summary>
    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    /// <summary>
    /// 회원가입 요청 모델
    /// </summary>
    public class RegisterRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PasswordConfirm { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}

