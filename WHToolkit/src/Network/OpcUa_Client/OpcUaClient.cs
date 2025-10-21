using System.Net;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Configuration;
namespace WHToolkit.Network.OpcUa_Client
{
    /// <summary>
    /// OPC UA 서버와 통신하는 클라이언트 클래스
    /// </summary>
    public class OpcUaClient : IDisposable
    {
        private Session _session;
        private string _endpointUrl;
        private ApplicationConfiguration _configuration;
        private string _applicationName;
        private bool _isInitializing;
        private List<Subscription> _subscriptions = new();
        private bool _disposed;
        private HashSet<string> _subscribedTags = new(); // 기존 구독 태그 저장
        private System.Timers.Timer _reconnectTimer;
        private bool _isReconnecting = false; // 중복 재연결 방지

        /// <summary>
        /// 현재 구독 중인 태그 목록
        /// </summary>
        public HashSet<string> SubscribedTags => _subscribedTags; // 외부에서 읽기 가능하게

        /// <summary>
        /// 재연결 실패 시 발생하는 이벤트
        /// </summary>
        public event EventHandler ReconnectFailed;

        /// <summary>
        /// 재연결 성공 시 발생하는 이벤트
        /// </summary>
        public event EventHandler Reconnected;

        /// <summary>
        /// OPC UA 클라이언트 생성자
        /// </summary>
        /// <param name="endpointUrl">OPC UA 서버 엔드포인트 URL</param>
        /// <param name="applicationName">클라이언트 애플리케이션 이름</param>
        public OpcUaClient(string endpointUrl, string applicationName)
        {
            _endpointUrl = endpointUrl;
            _applicationName = applicationName;
        }

        /// <summary>
        /// OPC UA 세션을 비동기로 초기화합니다.
        /// </summary>
        /// <returns>초기화 성공 여부</returns>
        /// <exception cref="Exception">초기화 중 오류 발생 시</exception>
        public async Task<bool> InitializeAsync()
        {
            if (_isInitializing)
                return false;

            _isInitializing = true;

            try
            {
                // 설정 초기화
                InitializeConfiguration();

                // 서버와 세션 초기화
                var selectedEndpoint = CoreClientUtils.SelectEndpoint(_endpointUrl, useSecurity: false);
                var endpointConfiguration = EndpointConfiguration.Create(_configuration);
                var configuredEndpoint = new ConfiguredEndpoint(null, selectedEndpoint, endpointConfiguration);

                _session = await Session.Create(
                    _configuration,
                    configuredEndpoint,
                    true,
                    _applicationName,
                    60000,
                    null,
                    null).ConfigureAwait(false);

                // KeepAlive 이벤트 등록
                _session.KeepAlive += OnKeepAlive;

                return true;
            }
            catch (Exception ex)
            {
                // Debug.WriteLine($"OPC UA 초기화 오류: {ex.Message}");
                return false;
            }
            finally
            {
                _isInitializing = false;
            }
        }

        /// <summary>
        /// OPC UA 애플리케이션 설정을 초기화합니다.
        /// </summary>
        private void InitializeConfiguration()
        {
            if (_configuration != null)
                return;

            _configuration = new ApplicationConfiguration()
            {
                // 애플리케이션 이름 및 URI 설정
                ApplicationName = _applicationName,  // 애플리케이션의 이름을 "OPCClientTest"로 설정
                ApplicationUri = Utils.Format(@"urn:{0}:OPCClientTest", System.Net.Dns.GetHostName()),  // 애플리케이션 URI를 현재 호스트 이름과 함께 설정
                ApplicationType = ApplicationType.Client,  // 애플리케이션 타입을 클라이언트로 설정

                // 보안 설정
                SecurityConfiguration = new SecurityConfiguration
                {
                    ApplicationCertificate = new CertificateIdentifier(),  // 애플리케이션 인증서를 식별하기 위한 기본 설정
                    TrustedPeerCertificates = new CertificateTrustList(),  // 신뢰할 수 있는 피어 인증서 리스트
                    RejectedCertificateStore = new CertificateStoreIdentifier(),  // 거부된 인증서를 저장할 스토어
                    AddAppCertToTrustedStore = true,  // 애플리케이션 인증서를 신뢰할 수 있는 인증서 저장소에 자동으로 추가
                    NonceLength = 32,  // 인증서 인증 시 사용되는 Nonce의 길이를 32로 설정
                    AutoAcceptUntrustedCertificates = true,  // 신뢰할 수 없는 인증서를 자동으로 수락할지 여부 (테스트 목적으로 사용 가능)
                    RejectSHA1SignedCertificates = false,  // SHA1로 서명된 인증서를 거부하지 않음 (SHA1은 더 이상 안전하지 않지만 필요에 따라 허용)
                    MinimumCertificateKeySize = 2048,  // 최소 인증서 키 크기를 2048비트로 설정
                    SendCertificateChain = true  // 인증서 체인을 서버에 전송하여 인증서 검증을 수행
                },

                // 전송 설정 (OPC UA의 전송 옵션)
                TransportConfigurations = new TransportConfigurationCollection(),  // 전송 구성을 위한 컬렉션
                TransportQuotas = new TransportQuotas { OperationTimeout = 15000 },  // 전송 제한 설정, 작업 시간 초과를 15초로 설정

                // 클라이언트 설정
                ClientConfiguration = new ClientConfiguration { DefaultSessionTimeout = 180000 },  // 기본 세션 타임아웃을 60초로 설정

                // 추적 설정 (디버그나 로깅 용도로 사용)
                TraceConfiguration = new TraceConfiguration(),  // 추적 설정, 추적 로그를 기록할 수 있는 설정 (기본값)
            };

            _configuration.Validate(ApplicationType.Client).GetAwaiter().GetResult();
            if (_configuration.SecurityConfiguration.AutoAcceptUntrustedCertificates)
            {
                _configuration.CertificateValidator.CertificateValidation += CertificateValidator_CertificateValidation; // 인증서오류
            }
        }
        /// <summary>
        /// 인증서 유효성 검사 이벤트 핸들러
        /// </summary>
        /// <param name="sender">인증서 검증기</param>
        /// <param name="e">인증서 검증 이벤트 인자</param>
        private void CertificateValidator_CertificateValidation(CertificateValidator sender, CertificateValidationEventArgs e)
        {
            try
            {
                e.Accept = _configuration.SecurityConfiguration.AutoAcceptUntrustedCertificates;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                //
            }
        }

        /// <summary>
        /// OPC UA 서버 연결 상태
        /// </summary>
        public bool IsConnected => _session != null && _session.Connected;

        /*        public async Task ReconnectAsync()
                {
                    // Debug.WriteLine("OPC UA 재연결을 시도합니다...");
                    for (int attempt = 1; attempt <= 5; attempt++)
                    {
                        // Debug.WriteLine($"재연결 시도 {attempt}번...");

                        if (await InitializeAsync())
                        {
                            // Debug.WriteLine("OPC UA 서버와 성공적으로 다시 연결되었습니다!");



                            Reconnected?.Invoke(this, EventArgs.Empty);
                            return;
                        }

                        await Task.Delay(5000); // 5초 대기
                    }

                    // Debug.WriteLine("OPC UA 재연결 실패.");
                    ReconnectFailed?.Invoke(this, EventArgs.Empty);
                }*/

        /// <summary>
        /// OPC UA 서버에 재연결을 시도합니다.
        /// 실패 시 자동으로 재시도 타이머를 시작합니다.
        /// </summary>
        public async Task ReconnectAsync()
        {
            // Debug.WriteLine("OPC UA 재연결을 시도합니다...");


            if (InitializeAsync().GetAwaiter().GetResult())
            {
                // Debug.WriteLine("OPC UA 서버와 성공적으로 다시 연결되었습니다!");
                Reconnected?.Invoke(this, EventArgs.Empty);
                return;
            }

            await Task.Delay(5000); // 5초 대기
            // Debug.WriteLine("OPC UA 재연결 실패. 5분 후 다시 시도합니다.");
            StartReconnectTimer();
        }


        /// <summary>
        /// 재연결 타이머를 시작합니다.
        /// 1분마다 재연결을 시도합니다.
        /// </summary>
        private void StartReconnectTimer()
        {
            if (_reconnectTimer != null)
            {
                _reconnectTimer.Stop();
                _reconnectTimer.Dispose();
            }

            _reconnectTimer = new System.Timers.Timer(60000); // 1분 (60,000ms)
            _reconnectTimer.Elapsed += async (sender, e) =>
            {
                if (_isReconnecting) return; // 중복 방지

                _isReconnecting = true;
                // Debug.WriteLine("5분 경과, OPC UA 재연결을 다시 시도합니다...");

                if (await InitializeAsync())
                {
                    // Debug.WriteLine("OPC UA 서버와 성공적으로 다시 연결되었습니다!");

                    // 기존 구독 태그 재구독
                    foreach (var tag in _subscribedTags)
                    {
                        SubscribeToTag(tag, 900, null);
                    }

                    Reconnected?.Invoke(this, EventArgs.Empty);

                    _reconnectTimer.Stop();
                    _reconnectTimer.Dispose();
                    _reconnectTimer = null;
                }

                _isReconnecting = false;
            };
            _reconnectTimer.AutoReset = true;
            _reconnectTimer.Start();
        }


        /// <summary>
        /// KeepAlive 이벤트 핸들러. 연결 상태를 모니터링합니다.
        /// </summary>
        /// <param name="session">OPC UA 세션</param>
        /// <param name="e">KeepAlive 이벤트 인자</param>
        private void OnKeepAlive(ISession session, KeepAliveEventArgs e)
        {
            if (ServiceResult.IsBad(e.Status))
            {
                _session.KeepAlive -= OnKeepAlive;
                // Debug.WriteLine("OPC UA 연결이 끊겼습니다.");
                Disconnect();
                ReconnectAsync();

            }
        }

        /// <summary>
        /// OPC UA 서버와의 연결을 종료합니다.
        /// 모든 구독을 삭제하고 세션을 닫습니다.
        /// </summary>
        public void Disconnect()
        {
            if (_session != null)
            {
                foreach (var subscription in _subscriptions)
                {
                    subscription.Delete(true);
                }

                _session.Close();
                if (_session != null) _session.Dispose();

                _session = null;
                // Debug.WriteLine("OPC UA 세션이 종료되었습니다.");
            }
        }
        /*        public Subscription SubscribeToTag(string nodeId, int publishingInterval, MonitoredItemNotificationEventHandler handler)
                {

                    if (_session == null || !_session.Connected)
                    {
                        throw new InvalidOperationException("OPC UA 서버에 연결되지 않았습니다.");
                    }

                    var subscription = new Subscription(_session.DefaultSubscription)
                    {
                        PublishingInterval = publishingInterval,
                        PublishingEnabled = true
                    };

                    var monitoredItem = new MonitoredItem(subscription.DefaultItem)
                    {
                        StartNodeId = new NodeId(nodeId),
                        AttributeId = Attributes.Value,
                        MonitoringMode = MonitoringMode.Reporting,
                    };

                    monitoredItem.Notification += handler;
                    subscription.AddItem(monitoredItem);
                    _session.AddSubscription(subscription);
                    subscription.Create();

                    if (!subscription.Created)
                    {
                        throw new Exception($"노드 {nodeId} 구독 생성 실패.");
                    }

                    _subscriptions.Add(subscription);
                    // Debug.WriteLine($"노드 {nodeId}에 대한 구독이 생성되었습니다.");


                    return subscription;
                }*/

        /// <summary>
        /// 특정 노드에 대한 구독을 생성합니다.
        /// </summary>
        /// <param name="nodeId">구독할 노드 ID</param>
        /// <param name="publishingInterval">발행 간격 (밀리초)</param>
        /// <param name="handler">데이터 변경 시 호출될 이벤트 핸들러</param>
        /// <returns>생성된 구독 객체, 이미 구독 중이면 null</returns>
        /// <exception cref="InvalidOperationException">서버에 연결되지 않은 경우</exception>
        /// <exception cref="Exception">구독 생성 실패 시</exception>
        public Subscription SubscribeToTag(string nodeId, int publishingInterval, MonitoredItemNotificationEventHandler handler)
        {
            if (_session == null || !_session.Connected)
            {
                throw new InvalidOperationException("OPC UA 서버에 연결되지 않았습니다.");
            }

            if (!_subscribedTags.Contains(nodeId)) // 중복 구독 방지
            {
                var subscription = new Subscription(_session.DefaultSubscription)
                {
                    PublishingInterval = publishingInterval,
                    PublishingEnabled = true
                };

                var monitoredItem = new MonitoredItem(subscription.DefaultItem)
                {
                    StartNodeId = new NodeId(nodeId),
                    AttributeId = Attributes.Value,
                    MonitoringMode = MonitoringMode.Reporting,
                };

                monitoredItem.Notification += handler;
                subscription.AddItem(monitoredItem);
                _session.AddSubscription(subscription);
                subscription.Create();

                if (!subscription.Created)
                {
                    throw new Exception($"노드 {nodeId} 구독 생성 실패.");
                }

                _subscriptions.Add(subscription);
                _subscribedTags.Add(nodeId); // 구독된 태그 저장

                // Debug.WriteLine($"노드 {nodeId}에 대한 구독이 생성되었습니다.");
                return subscription;
            }

            return null;
        }


        /// <summary>
        /// 모든 구독을 삭제합니다.
        /// </summary>
        public void UnsubscribeAll()
        {
            if (_subscriptions.Count > 0)
            {
                foreach (var subscription in _subscriptions)
                {
                    subscription.Delete(true);
                }
                _subscriptions.Clear();
                // Debug.WriteLine("모든 구독이 삭제되었습니다.");
            }
        }

        /// <summary>
        /// 특정 노드의 구독을 삭제합니다.
        /// </summary>
        /// <param name="nodeId">구독 삭제할 노드 ID</param>
        public void Unsubscribe(string nodeId)
        {
            var subscription = _subscriptions.Find(sub =>
                sub.MonitoredItems.Any(item => item.StartNodeId.ToString() == nodeId));

            if (subscription != null)
            {
                subscription.Delete(true);
                _subscriptions.Remove(subscription);
                // Debug.WriteLine($"노드 {nodeId}에 대한 구독이 삭제되었습니다.");
            }
        }


        /// <summary>
        /// 단일 노드의 값을 읽습니다.
        /// </summary>
        /// <param name="nodeId">읽을 노드 ID</param>
        /// <returns>노드의 값</returns>
        /// <exception cref="InvalidOperationException">서버에 연결되지 않은 경우</exception>
        /// <exception cref="Exception">읽기 실패 시</exception>
        public object ReadTag(string nodeId)
        {
            if (_session == null)
            {
                throw new InvalidOperationException("OPC UA 서버에 연결되지 않았습니다.");
            }

            NodeId nodeToRead = new NodeId(nodeId);
            DataValue value = _session.ReadValue(nodeToRead);

            if (value.StatusCode == StatusCodes.Good)
            {
                return value.Value;
            }

            throw new Exception($"노드 {nodeId}의 값을 읽을 수 없습니다.");
        }

        /// <summary>
        /// 단일 노드의 값을 OPCDataValue 객체로 읽습니다.
        /// </summary>
        /// <param name="nodeId">읽을 노드 ID</param>
        /// <returns>노드 데이터를 포함한 OPCDataValue 객체</returns>
        /// <exception cref="InvalidOperationException">서버에 연결되지 않은 경우</exception>
        /// <exception cref="Exception">읽기 실패 시</exception>
        public OPCDataValue ReadTagOpcData(string nodeId)
        {
            if (_session == null)
            {
                throw new InvalidOperationException("OPC UA 서버에 연결되지 않았습니다.");
            }

            // 읽을 노드를 설정
            NodeId nodeToRead = new NodeId(nodeId);
            DataValue value = _session.ReadValue(nodeToRead);

            // DataValue가 Good일 경우 OPCDataValue 객체로 반환
            if (value.StatusCode == StatusCodes.Good)
            {
                return new OPCDataValue
                {
                    NodeId = nodeId,
                    Value = value.Value,
                    StatusCode = value.StatusCode,
                    SourceTimestamp = value.SourceTimestamp,
                    ServerTimestamp = value.ServerTimestamp
                };
            }

            // 값 읽기에 실패한 경우 예외를 발생시킴
            throw new Exception($"노드 {nodeId}의 값을 읽을 수 없습니다: {value.StatusCode}");
        }
        /// <summary>
        /// 여러 노드의 값을 한 번에 읽습니다.
        /// </summary>
        /// <param name="nodeIds">읽을 노드 ID 목록</param>
        /// <returns>노드 ID와 값의 딕셔너리</returns>
        /// <exception cref="InvalidOperationException">서버에 연결되지 않은 경우</exception>
        public Dictionary<string, object> ReadTags(List<string> nodeIds)
        {
            if (_session == null)
            {
                throw new InvalidOperationException("OPC UA 서버에 연결되지 않았습니다.");
            }

            ReadValueIdCollection nodesToRead = new ReadValueIdCollection();
            foreach (string nodeId in nodeIds)
            {
                nodesToRead.Add(new ReadValueId
                {
                    NodeId = new NodeId(nodeId),
                    AttributeId = Attributes.Value
                });
            }

            // 값을 읽음
            DataValueCollection results;
            DiagnosticInfoCollection diagnosticInfos;
            _session.Read(
                null,
                0,
                TimestampsToReturn.Both,
                nodesToRead,
                out results,
                out diagnosticInfos
            );

            // 결과 처리
            Dictionary<string, object> tagValues = new Dictionary<string, object>();
            for (int i = 0; i < nodeIds.Count; i++)
            {
                if (StatusCode.IsGood(results[i].StatusCode))
                {
                    tagValues[nodeIds[i]] = results[i].Value;
                }
                else
                {
                    tagValues[nodeIds[i]] = "OPCNG";
                    // Debug.WriteLine($"노드 {nodeIds[i]} 값을 읽지 못했습니다: {results[i].StatusCode}");
                }
            }

            return tagValues;
        }
        /// <summary>
        /// 여러 노드의 값을 OPCDataValue 리스트로 읽습니다.
        /// </summary>
        /// <param name="nodeIds">읽을 노드 ID 목록</param>
        /// <returns>OPCDataValue 객체 리스트</returns>
        /// <exception cref="InvalidOperationException">서버에 연결되지 않은 경우</exception>
        public List<OPCDataValue> ReadTagsOpcData(List<string> nodeIds)
        {
            if (_session == null)
            {
                throw new InvalidOperationException("OPC UA 서버에 연결되지 않았습니다.");
            }

            // 읽을 노드 목록 구성
            ReadValueIdCollection nodesToRead = new ReadValueIdCollection();
            foreach (string nodeId in nodeIds)
            {
                nodesToRead.Add(new ReadValueId
                {
                    NodeId = new NodeId(nodeId),
                    AttributeId = Attributes.Value
                });
            }

            // 값을 읽음
            DataValueCollection results;
            DiagnosticInfoCollection diagnosticInfos;
            _session.Read(
                null,
                0,
                TimestampsToReturn.Both,
                nodesToRead,
                out results,
                out diagnosticInfos
            );

            // 결과를 저장할 리스트 생성
            List<OPCDataValue> tagValues = new List<OPCDataValue>();

            // 결과 처리
            for (int i = 0; i < nodeIds.Count; i++)
            {
                OPCDataValue dataValue = new OPCDataValue
                {
                    NodeId = nodeIds[i],  // NodeId 추가
                    Value = results[i].Value,
                    StatusCode = results[i].StatusCode,
                    SourceTimestamp = results[i].SourceTimestamp,
                    ServerTimestamp = results[i].ServerTimestamp
                };

                tagValues.Add(dataValue);

                if (!StatusCode.IsGood(results[i].StatusCode))
                {
                    // Debug.WriteLine($"노드 {nodeIds[i]} 값을 읽지 못했습니다: {results[i].StatusCode}");
                }
            }

            return tagValues;
        }
        /// <summary>
        /// 여러 노드의 값을 비동기로 OPCDataValue 리스트로 읽습니다.
        /// </summary>
        /// <param name="nodeIds">읽을 노드 ID 목록</param>
        /// <returns>OPCDataValue 객체 리스트</returns>
        /// <exception cref="InvalidOperationException">서버에 연결되지 않은 경우</exception>
        public async Task<List<OPCDataValue>> ReadTagsOpcDataAsync(List<string> nodeIds)
        {
            if (_session == null)
            {
                throw new InvalidOperationException("OPC UA 서버에 연결되지 않았습니다.");
            }

            // 읽을 노드 목록 구성
            ReadValueIdCollection nodesToRead = new ReadValueIdCollection();
            foreach (string nodeId in nodeIds)
            {
                nodesToRead.Add(new ReadValueId
                {
                    NodeId = new NodeId(nodeId),
                    AttributeId = Attributes.Value
                });
            }

            // 값을 비동기로 읽음
            var readResult = await Task.Run(() =>
            {
                DataValueCollection results;
                DiagnosticInfoCollection diagnosticInfos;
                _session.Read(
                    null,
                    0,
                    TimestampsToReturn.Both,
                    nodesToRead,
                    out results,
                    out diagnosticInfos
                );
                return (results, diagnosticInfos);
            });

            var (results, _) = readResult;

            // 결과를 저장할 리스트 생성
            List<OPCDataValue> tagValues = new List<OPCDataValue>();

            // 결과 처리
            for (int i = 0; i < nodeIds.Count; i++)
            {
                OPCDataValue dataValue = new OPCDataValue
                {
                    NodeId = nodeIds[i],  // NodeId 추가
                    Value = results[i].Value,
                    StatusCode = results[i].StatusCode,
                    SourceTimestamp = results[i].SourceTimestamp,
                    ServerTimestamp = results[i].ServerTimestamp
                };

                tagValues.Add(dataValue);

                if (!StatusCode.IsGood(results[i].StatusCode))
                {
                    // Debug.WriteLine($"노드 {nodeIds[i]} 값을 읽지 못했습니다: {results[i].StatusCode}");
                }
            }

            return tagValues;
        }

        /// <summary>
        /// 단일 노드에 값을 씁니다.
        /// 값은 자동으로 노드의 데이터 타입으로 변환됩니다.
        /// </summary>
        /// <param name="nodeId">쓸 노드 ID</param>
        /// <param name="valueToWrite">쓸 값</param>
        /// <exception cref="InvalidOperationException">서버에 연결되지 않은 경우</exception>
        /// <exception cref="Exception">쓰기 실패 시</exception>
        public void WriteTag(string nodeId, object valueToWrite)
        {
            if (_session == null)
            {
                throw new InvalidOperationException("OPC UA 서버에 연결되지 않았습니다.");
            }

            try
            {
                // 노드에 필요한 데이터 형식이 무엇인지 먼저 확인 (이 예제에서는 UInt16로 가정)
                NodeId nodeToWrite = new NodeId(nodeId);

                // 기존 노드의 데이터 타입을 읽어와야 할 경우 (데이터 타입이 이미 명확하면 생략 가능)
                DataValue nodeValue = _session.ReadValue(nodeToWrite);
                Type expectedType = nodeValue.Value.GetType();

                // valueToWrite의 타입이 expectedType과 일치하는지 확인하고 변환 시도
                object convertedValue = ConvertValueToType(valueToWrite, expectedType);

                WriteValue writeValue = new WriteValue
                {
                    NodeId = nodeToWrite,
                    AttributeId = Attributes.Value,
                    Value = new DataValue(new Variant(convertedValue))
                };

                StatusCodeCollection results = null;
                DiagnosticInfoCollection diagnosticInfos = null;
                _session.Write(null, new WriteValueCollection { writeValue }, out results, out diagnosticInfos);

                if (results[0] != StatusCodes.Good)
                {
                    throw new Exception($"노드 {nodeId}에 쓰기 실패: {results[0]}");
                }

                // Debug.WriteLine($"노드 {nodeId}에 값 {convertedValue}를 성공적으로 작성했습니다.");
            }
            catch (Exception ex)
            {
                // Debug.WriteLine($"쓰기 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 단일 노드에 비동기로 값을 씁니다.
        /// 값은 자동으로 노드의 데이터 타입으로 변환됩니다.
        /// </summary>
        /// <param name="nodeId">쓸 노드 ID</param>
        /// <param name="valueToWrite">쓸 값</param>
        /// <exception cref="InvalidOperationException">서버에 연결되지 않은 경우</exception>
        /// <exception cref="Exception">쓰기 실패 시</exception>
        public async Task WriteTagAsync(string nodeId, object valueToWrite)
        {
            if (_session == null)
            {
                throw new InvalidOperationException("OPC UA 서버에 연결되지 않았습니다.");
            }

            try
            {
                // 노드 ID 생성
                NodeId nodeToWrite = new NodeId(nodeId);

                // 기존 노드 데이터 타입 확인
                DataValue nodeValue = await Task.Run(() => _session.ReadValue(nodeToWrite));
                Type expectedType = nodeValue.Value.GetType();

                // valueToWrite의 타입을 expectedType으로 변환
                object convertedValue = ConvertValueToType(valueToWrite, expectedType);

                // 쓰기 작업 생성
                WriteValue writeValue = new WriteValue
                {
                    NodeId = nodeToWrite,
                    AttributeId = Attributes.Value,
                    Value = new DataValue(new Variant(convertedValue))
                };

                // OPC UA Write 호출 비동기 처리
                await Task.Run(() =>
                {
                    StatusCodeCollection results = null;
                    DiagnosticInfoCollection diagnosticInfos = null;

                    _session.Write(null, new WriteValueCollection { writeValue }, out results, out diagnosticInfos);

                    if (results[0] != StatusCodes.Good)
                    {
                        throw new Exception($"노드 {nodeId}에 쓰기 실패: {results[0]}");
                    }
                });

                // Debug.WriteLine($"노드 {nodeId}에 값 {convertedValue}를 성공적으로 작성했습니다.");
            }
            catch (Exception ex)
            {
                // Debug.WriteLine($"쓰기 실패: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 여러 노드에 값을 한 번에 씁니다.
        /// </summary>
        /// <param name="nodeIds">쓸 노드 ID 목록</param>
        /// <param name="valuesToWrite">쓸 값 목록</param>
        /// <exception cref="InvalidOperationException">서버에 연결되지 않은 경우</exception>
        /// <exception cref="ArgumentException">노드 ID와 값의 개수가 일치하지 않는 경우</exception>
        public void WriteTags(List<string> nodeIds, List<object> valuesToWrite)
        {
            if (_session == null)
            {
                throw new InvalidOperationException("OPC UA 서버에 연결되지 않았습니다.");
            }

            if (nodeIds.Count != valuesToWrite.Count)
            {
                throw new ArgumentException("노드 ID와 쓰기 값의 개수가 일치해야 합니다.");
            }

            // 쓰기 요청 생성
            WriteValueCollection nodesToWrite = new WriteValueCollection();
            for (int i = 0; i < nodeIds.Count; i++)
            {
                // 노드 ID에 맞는 데이터 타입을 확인하고 값을 변환
                NodeId nodeToWrite = new NodeId(nodeIds[i]);
                DataValue nodeValue = _session.ReadValue(nodeToWrite);
                Type expectedType = nodeValue.Value.GetType();

                // 값을 서버가 기대하는 타입으로 변환
                object convertedValue = ConvertValueToType(valuesToWrite[i], expectedType);

                nodesToWrite.Add(new WriteValue
                {
                    NodeId = nodeToWrite,
                    AttributeId = Attributes.Value,
                    Value = new DataValue(new Variant(convertedValue))
                });
            }

            // 값을 쓰기
            StatusCodeCollection results;
            DiagnosticInfoCollection diagnosticInfos;
            _session.Write(
                null,
                nodesToWrite,
                out results,
                out diagnosticInfos
            );

            // 결과 처리
            for (int i = 0; i < nodeIds.Count; i++)
            {
                if (results[i] == StatusCodes.Good)
                {
                    // Debug.WriteLine($"노드 {nodeIds[i]}에 성공적으로 값이 작성되었습니다.");
                }
                else
                {
                    // Debug.WriteLine($"노드 {nodeIds[i]}에 쓰기 실패: {results[i]}");
                }
            }
        }
        /// <summary>
        /// 딕셔너리를 사용하여 여러 노드에 값을 한 번에 씁니다.
        /// </summary>
        /// <param name="nodeData">노드 ID를 키로, 쓸 값을 값으로 하는 딕셔너리</param>
        /// <exception cref="InvalidOperationException">서버에 연결되지 않은 경우</exception>
        public void WriteTags(Dictionary<string, object> nodeData)
        {
            if (_session == null)
            {
                throw new InvalidOperationException("OPC UA 서버에 연결되지 않았습니다.");
            }

            // 쓰기 요청 생성
            WriteValueCollection nodesToWrite = new WriteValueCollection();

            foreach (var kvp in nodeData)
            {
                string nodeId = kvp.Key;
                object valueToWrite = kvp.Value;

                // 노드 ID에 맞는 데이터 타입을 확인하고 값을 변환
                NodeId nodeToWrite = new NodeId(nodeId);
                DataValue nodeValue = _session.ReadValue(nodeToWrite); // 노드에서 기대하는 타입 확인
                Type expectedType = nodeValue.Value.GetType();

                // 값을 서버가 기대하는 타입으로 변환
                object convertedValue = ConvertValueToType(valueToWrite, expectedType);

                // WriteValue 객체 생성하여 추가
                nodesToWrite.Add(new WriteValue
                {
                    NodeId = nodeToWrite,
                    AttributeId = Attributes.Value,
                    Value = new DataValue(new Variant(convertedValue))
                });
            }

            // 값을 쓰기
            StatusCodeCollection results;
            DiagnosticInfoCollection diagnosticInfos;
            _session.Write(
                null,
                nodesToWrite,
                out results,
                out diagnosticInfos
            );

            // 결과 처리
            int index = 0;
            foreach (var kvp in nodeData)
            {
                if (results[index] == StatusCodes.Good)
                {
                    // Debug.WriteLine($"노드 {kvp.Key}에 성공적으로 값이 작성되었습니다.");
                }
                else
                {
                    // Debug.WriteLine($"노드 {kvp.Key}에 쓰기 실패: {results[index]}");
                }
                index++;
            }
        }

        /// <summary>
        /// 값을 지정된 타입으로 변환합니다.
        /// </summary>
        /// <param name="valueToWrite">변환할 값</param>
        /// <param name="expectedType">목표 타입</param>
        /// <returns>변환된 값</returns>
        /// <exception cref="InvalidCastException">타입 변환 실패 시</exception>
        private object ConvertValueToType(object valueToWrite, Type expectedType)
        {
            try
            {
                // valueToWrite의 타입이 이미 예상 타입과 일치하면 변환 필요 없음
                if (valueToWrite.GetType() == expectedType)
                {
                    return valueToWrite;
                }

                // 타입 변환 시도
                return Convert.ChangeType(valueToWrite, expectedType);
            }
            catch (Exception ex)
            {
                throw new InvalidCastException($"값을 {expectedType.Name}으로 변환할 수 없습니다: {ex.Message}");
            }
        }

        /// <summary>
        /// 리소스를 해제합니다.
        /// 서버 연결을 종료하고 모든 구독을 삭제합니다.
        /// </summary>
        public void Dispose()
        {
            if (_disposed)
                return;

            Disconnect();
            _disposed = true;
            // Debug.WriteLine("OPCUAHelper 리소스가 정리되었습니다.");
        }
    }


  
}
