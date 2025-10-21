using Opc.Ua;

namespace WHToolkit.Network.OpcUa_Client
{
    /// <summary>
    /// OPC UA 노드의 데이터 값을 표현하는 클래스
    /// </summary>
    public class OPCDataValue
    {
        /// <summary>
        /// OPC UA 노드 ID
        /// </summary>
        public string NodeId { get; set; }  // NodeId 속성 추가

        /// <summary>
        /// 노드의 실제 값
        /// </summary>
        public object Value { get; set; }  // 태그 값

        /// <summary>
        /// 읽기 작업의 상태 코드
        /// </summary>
        public StatusCode StatusCode { get; set; }  // 상태 코드

        /// <summary>
        /// 데이터 소스의 타임스탬프
        /// </summary>
        public DateTime SourceTimestamp { get; set; }  // 소스 타임스탬프

        /// <summary>
        /// OPC UA 서버의 타임스탬프
        /// </summary>
        public DateTime ServerTimestamp { get; set; }  // 서버 타임스탬프

        /// <summary>
        /// 상태 코드의 문자열 표현
        /// </summary>
        public string strbStatusCode { get { return StatusCode.ToString(); } }

        /// <summary>
        /// 소스 타임스탬프의 문자열 표현
        /// </summary>
        public string strSourceTimestamp { get { return SourceTimestamp.ToString(); } }

        /// <summary>
        /// 서버 타임스탬프의 문자열 표현
        /// </summary>
        public string strServerTimestamp { get { return ServerTimestamp.ToString(); } }

        /// <summary>
        /// OPC UA 데이터 값의 문자열 표현을 반환합니다.
        /// </summary>
        /// <returns>NodeId, Value, StatusCode, Timestamp 정보를 포함한 문자열</returns>
        public override string ToString()
        {
            return $"NodeId: {NodeId}, Value: {Value}, StatusCode: {StatusCode}, SourceTimestamp: {SourceTimestamp}, ServerTimestamp: {ServerTimestamp}";
        }
    }
}
