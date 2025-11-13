using System;

namespace WHToolkit.Tests.Models
{
    /// <summary>
    /// 사용자 모델 (모든 DB 공통)
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int? Age { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Name} ({Email}) - Age: {Age}, Active: {IsActive}, Created: {CreatedDate:yyyy-MM-dd HH:mm:ss}";
        }
    }
}

