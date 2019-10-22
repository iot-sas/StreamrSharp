using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using RestSharp;
using StreamrSharp.API.ControlLayer;
using StreamrSharp.API.NetworkLayer;
using Websocket.Client;

namespace StreamrSharp
{
    public class StreamrClient : IDisposable
    {

        public string RestURL               { get; private set; }
        public string WebSocketURL          { get; private set; }
    
        public StreamrClient(String restURL="https://www.streamr.com/api/v1", string webSocketURL="wss://www.streamr.com/api/v1/ws")
        {
            RestURL = restURL;
            WebSocketURL = webSocketURL;
        }


        public WebsocketClient websocketClient { get; private set; }

        public SessionToken sessionToken { get; private set; }
        //public Uri uri { get; private set; }
     
        public event EventHandler<IControlLayer> NetworkMessage;
        public event EventHandler<BroadcastMessage> BroadcastMessage;
        
        StringBuilder sb = new StringBuilder();
        
        
        public void Connect(SessionToken sessionTokenClass=null)
        {
            var sclient = new StreamrClient();

            sessionToken = sessionTokenClass;

            websocketClient = new WebsocketClient(new Uri(WebSocketURL));

            websocketClient.ReconnectTimeoutMs = (int)TimeSpan.FromSeconds(30).TotalMilliseconds;
            websocketClient.ReconnectionHappened.Subscribe(type =>
                Console.WriteLine($"Reconnection happened, type: {type}"));


            websocketClient.MessageReceived.Subscribe(msg =>
                {
                    int ptS = 0;
                    int ptE;

                    while (ptS < msg.Text.Length)
                    {
                        ptE = msg.Text.IndexOf("][", ptS);
                        if (ptE == -1)
                        {
                            if (msg.Text.Last() == ']')
                            {
                                ptE = msg.Text.Length - 1;
                            }
                            else
                            {
                                sb.Append(msg.Text.Substring(ptS, msg.Text.Length - ptS));
                                break;
                            }
                        }

                        if (sb.Length > 0)  //Previous data in buffer
                        {
                            sb.Append(msg.Text.Substring(ptS, ptE - ptS + 1));  //Add next message to buffer
                            string message = sb.ToString(1,sb.Length-2);
                            ProcessMessage(ref message); //Process
                            sb.Clear();
                        }
                        else
                        {
                            string message = msg.Text.Substring(ptS + 1, ptE - ptS - 1);
                            ProcessMessage(ref message);
                        }
                        ptS = ptE + 1;
                    }
                });
            websocketClient.Start();
        
        }

        void ProcessMessage(ref string message)
        {

            var data = message.Split(new char[] { ',' }, 4);
           
            uint version = 0;
            if (!uint.TryParse(data[0], out version))
            {
                throw new Exception("Message error");
            }
            
            int msgType;
            if (!Int32.TryParse(data[1], out msgType))
            {
                throw new Exception("Message error");
            }
            

            IControlLayer MsgClass = null;
            
            switch ((_ControlLayerBase.messageType)msgType)
            {

                case _ControlLayerBase.messageType.BroadcastMessage:
                    MsgClass = new BroadcastMessage(ref data);
                    BroadcastMessage?.Invoke(this,(BroadcastMessage)MsgClass);
                    break;
                case _ControlLayerBase.messageType.UnicastMessage:
          //          MsgClass = new UnicastMessage(ref data);
          //          break;
                case _ControlLayerBase.messageType.SubscribeResponse:
                    MsgClass = JsonConvert.DeserializeObject<SubscribeRequest>(data[3]);
                    break;
                case _ControlLayerBase.messageType.UnsubscribeResponse:
                    MsgClass = JsonConvert.DeserializeObject<UnsubscribeResponse>(data[3]);
                    break;
          //      case _ControlLayerBase.messageType.ResendResponseResending:
          //          MsgClass = new ResendResponseResending(ref data);
          //          break;
          //      case _ControlLayerBase.messageType.ResendResponseResent:
          //          MsgClass = new ResendResponseResent(ref data);
          //          break;
          //      case _ControlLayerBase.messageType.ResendResponseNoResend:
          //          MsgClass = new ResendResponseNoResend(ref data);
          //          break;
                case _ControlLayerBase.messageType.ErrorResponse:
                    MsgClass = new ErrorResponse(ref data);
                    break;
          //      case _ControlLayerBase.messageType.PublishRequest:
          //          MsgClass = new PublishRequest(ref data);
          //          break;
                case _ControlLayerBase.messageType.SubscribeRequest:
                    MsgClass = JsonConvert.DeserializeObject<SubscribeRequest>(data[3]);
                    break;
                case _ControlLayerBase.messageType.UnsubscribeRequest:
                    MsgClass = JsonConvert.DeserializeObject<UnsubscribeRequest>(data[3]);
                    break;
        //      case _ControlLayerBase.messageType.ResendLastRequest:
        //          MsgClass = new ResendLastRequest(ref data);
        //          break;
        //      case _ControlLayerBase.messageType.ResendFromRequest:
        //          MsgClass = new ResendFromRequest(ref data);
        //          break;
        //      case _ControlLayerBase.messageType.ResendRangeRequest:
        //          MsgClass = new ResendRangeRequest(ref data);
        //          break;
                default:
                    Console.WriteLine($"Unsupported message: {(_ControlLayerBase.messageType)msgType} {message}");
                    break;
            }

            MsgClass.Version = version;
            NetworkMessage?.Invoke(this, MsgClass);
        }

        public void Send(IControlLayer messageClass,string Sender = "sender", SessionToken token = null)
        {
            websocketClient.Send(messageClass.ToMessage(token ?? sessionToken));
        }

        public void Dispose()
        {
            if (websocketClient != null) websocketClient.Dispose();
        }
        
    }
}
