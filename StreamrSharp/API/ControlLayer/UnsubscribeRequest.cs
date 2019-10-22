using System;
using Newtonsoft.Json;

namespace StreamrSharp.API.ControlLayer
{
    public class UnsubscribeRequest : _ControlLayerBase, IControlLayer
    {
    
        [JsonProperty("stream")]
        public string StreamID           { get; private set; }
        [JsonProperty("partition")]
        public uint   StreamPartition    { get; private set; }

        public UnsubscribeRequest() : base(messageType.UnsubscribeRequest)
        {
        }
        
        public UnsubscribeRequest(string streamID, uint streamPartition = 0) : base(messageType.UnsubscribeRequest)
        {
            StreamID = streamID;
            StreamPartition = streamPartition;
        }
        
        public string ToMessage(SessionToken sessionToken)
        {
            return  $"[{Version},{(int)MessageType},\"{StreamID}\",{StreamPartition},\"{sessionToken.token}\"]";
        }
        
    }
}
