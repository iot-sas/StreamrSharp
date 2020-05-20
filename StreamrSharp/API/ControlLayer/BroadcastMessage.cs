using System;
using System.Text;
using Newtonsoft.Json.Linq;
using StreamrSharp.API.MessageLayer;

namespace StreamrSharp.API.ControlLayer
{
    public class BroadcastMessage : _ControlLayerBase, IControlLayer
    {
        public StreamMessage streamMessage            { get; set; }

        static public uint requestId                         { get; private set; }
        
        public BroadcastMessage(ref String[] dataInput) : base(messageType.BroadcastMessage)
        {
            streamMessage = new StreamMessage(dataInput[3]);
        }

        
        public string ToMessage(SessionToken sessionToken, string requestID)
        {  
            throw new NotImplementedException();
        }
    }
}
