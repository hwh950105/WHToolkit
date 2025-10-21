using System;

namespace WHToolkit.Database.Attributes
{
    /// <summary>
    /// 필드나 속성이 데이터베이스 테이블의 기본키임을 나타내는 특성입니다.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class PrimaryKeyAttribute : Attribute
    {
        /// <summary>
        /// Identity 컬럼 여부 (자동 증가)
        /// </summary>
        public bool IsIdentity { get; set; }

        /// <summary>
        /// 선택적 기본키 여부
        /// </summary>
        public bool IsOptional { get; set; }

        /// <summary>
        /// 시퀀스 이름 (Oracle, PostgreSQL 등에서 사용)
        /// </summary>
        public string SequenceName { get; set; } = string.Empty;

        /// <summary>
        /// 삭제 시 제거 대상 여부
        /// </summary>
        public bool IsRemove { get; set; }

        /// <summary>
        /// 정렬 방향 (Asc 또는 Desc)
        /// </summary>
        public string Direction { get; set; } = string.Empty;

        /// <summary>
        /// PrimaryKeyAttribute의 새 인스턴스를 초기화합니다.
        /// </summary>
        public PrimaryKeyAttribute() { }

        /// <summary>
        /// Identity 여부를 지정하여 PrimaryKeyAttribute의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="isIdentity">Identity 컬럼 여부</param>
        public PrimaryKeyAttribute(bool isIdentity)
        {
            IsIdentity = isIdentity;
        }

        /// <summary>
        /// Identity 여부와 선택적 여부를 지정하여 PrimaryKeyAttribute의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="isIdentity">Identity 컬럼 여부</param>
        /// <param name="isOptional">선택적 기본키 여부</param>
        public PrimaryKeyAttribute(bool isIdentity, bool isOptional)
        {
            IsIdentity = isIdentity;
            IsOptional = isOptional;
        }

        /// <summary>
        /// 시퀀스 이름을 지정하여 PrimaryKeyAttribute의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="sequenceName">시퀀스 이름</param>
        public PrimaryKeyAttribute(string sequenceName)
        {
            SequenceName = sequenceName;
        }

        /// <summary>
        /// 모든 옵션을 지정하여 PrimaryKeyAttribute의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="isIdentity">Identity 컬럼 여부</param>
        /// <param name="isOptional">선택적 기본키 여부</param>
        /// <param name="sequenceName">시퀀스 이름</param>
        /// <param name="isRemove">삭제 시 제거 대상 여부</param>
        public PrimaryKeyAttribute(bool isIdentity = false, bool isOptional = false, string? sequenceName = null, bool isRemove = false)
        {
            IsIdentity = isIdentity;
            IsOptional = isOptional;
            IsRemove = isRemove;
            SequenceName = sequenceName ?? string.Empty;
        }
    }
}