using System;

namespace WHToolkit.Database.Attributes
{
    /// <summary>
    /// 클래스나 구조체가 데이터베이스와의 연계를 저장 프로시저로 수행함을 나타내는 특성입니다.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class ProcedureAttribute : Attribute
    {
        /// <summary>
        /// SELECT 작업에 사용할 저장 프로시저 이름
        /// </summary>
        public string Select { get; set; } = string.Empty;

        /// <summary>
        /// INSERT 작업에 사용할 저장 프로시저 이름
        /// </summary>
        public string Insert { get; set; } = string.Empty;

        /// <summary>
        /// UPDATE 작업에 사용할 저장 프로시저 이름
        /// </summary>
        public string Update { get; set; } = string.Empty;

        /// <summary>
        /// DELETE 작업에 사용할 저장 프로시저 이름
        /// </summary>
        public string Delete { get; set; } = string.Empty;

        /// <summary>
        /// EXISTS 확인에 사용할 저장 프로시저 이름
        /// </summary>
        public string Exists { get; set; } = string.Empty;

        /// <summary>
        /// ProcedureAttribute의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="select">SELECT 저장 프로시저 이름</param>
        /// <param name="insert">INSERT 저장 프로시저 이름</param>
        /// <param name="update">UPDATE 저장 프로시저 이름</param>
        /// <param name="delete">DELETE 저장 프로시저 이름</param>
        /// <param name="exists">EXISTS 저장 프로시저 이름</param>
        public ProcedureAttribute(string select = "", string insert = "", string update = "", string delete = "", string exists = "")
        {
            Select = select;
            Insert = insert;
            Update = update;
            Delete = delete;
            Exists = exists;
        }
    }
}