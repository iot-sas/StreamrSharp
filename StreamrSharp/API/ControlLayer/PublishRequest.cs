using System.Text;
using Newtonsoft.Json.Linq;

namespace StreamrSharp.API.ControlLayer
{
    public class PublishRequest : _ControlLayerBase, IControlLayer
    {

        public StreamMessage streamMessage { get; set; }
    
        public PublishRequest(ref string[] dataInput) : base(messageType.PublishRequest)
        {
        }
        
        public PublishRequest(StreamMessage streamMessage) : base(messageType.PublishRequest)
        {
            this.streamMessage = streamMessage;
        }

        
        //[31, [...msgIdFields], [...msgRefFields], 27, 0, "contentData", 1, "0x29c057786Fa..."]
        public string ToMessage(SessionToken sessionToken, string requestID)
        {        
                // [version, type, requestId, streamMessage, sessionToken]
            var sb = new StringBuilder();
            sb.Append("[");
            sb.Append(Version);
            sb.Append(",");
            sb.Append((uint)MessageType);
            // sb.Append(",\"");
            // sb.Append(requestID);
            // sb.Append("\",");

            //  sb.Append(",42,");
              //sb.Append(",\"42\",");
              
            sb.Append(",");
            sb.Append(streamMessage.ToJson());
            sb.Append(",\"");
            sb.Append(sessionToken.token);
            sb.Append("\"]");

            return sb.ToString();
        }
        
        
    }
}
