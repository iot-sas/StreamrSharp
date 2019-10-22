using System;
using StreamrSharp.API.ControlLayer;

namespace StreamrSharp.API.NetworkLayer
{
    public class WrapperMessage : _NetworkLayerBase
    {
        public IControlLayer ControlLayer { get; private set; }
        public string        Sender       { get; private set; }
        
        public WrapperMessage(IControlLayer controlLayer, string sender) : base(messageType.WrapperMessage)
        {
            ControlLayer = controlLayer;
            Sender = sender;
        }
        
        public string ToMessage(SessionToken sessionToken)
        {
            //[version, type, source, controlLayerPayload]
        
            var msg =  $"[\"{Version}\",{(int)messageType.WrapperMessage},\"{Sender}\",{ControlLayer.ToMessage(sessionToken)}]";
            return msg;
        }
                
    }
}
