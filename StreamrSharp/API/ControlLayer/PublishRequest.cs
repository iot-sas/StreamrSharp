using System;
using System.Text;

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

        
        
        public string ToMessage(SessionToken sessionToken)
        {        
            var sb = new StringBuilder();
            sb.Append("[");
            sb.Append(Version);
            sb.Append(",");
            sb.Append((uint)MessageType);
            sb.Append(",");
            sb.Append(streamMessage.ToJson());

            return sb.ToString();
        }
        
    }
}
