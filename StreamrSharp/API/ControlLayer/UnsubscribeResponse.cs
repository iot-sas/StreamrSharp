using System;
using Newtonsoft.Json;

namespace StreamrSharp.API.ControlLayer
{
    public class UnsubscribeResponse : _ControlLayerBase, IControlLayer
    {
    
        [JsonProperty("stream")]
        public string StreamID           { get; private set; }
        [JsonProperty("partition")]        
        public uint   StreamPartition    { get; private set; }

        public UnsubscribeResponse(ref string[] dataInput): base(messageType.UnsubscribeRequest)
        {
        }
        
        public string ToMessage(SessionToken sessionToken)
        {
            throw new NotImplementedException();
        }
        
    }
}
