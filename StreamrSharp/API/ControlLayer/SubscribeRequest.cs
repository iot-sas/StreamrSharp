using System;
using Newtonsoft.Json;

namespace StreamrSharp.API.ControlLayer
{
    public class SubscribeRequest : _ControlLayerBase, IControlLayer
    {
        [JsonProperty("stream")]
        public string StreamID           { get; private set; }
        [JsonProperty("partition")]
        public uint   StreamPartition    { get; private set; }


        public SubscribeRequest() : base(messageType.SubscribeRequest)
        {
        }
        
        public SubscribeRequest(string streamID, uint streamPartition = 0) : base(messageType.SubscribeRequest)
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
