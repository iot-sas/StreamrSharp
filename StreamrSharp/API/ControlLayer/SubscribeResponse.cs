using System;
namespace StreamrSharp.API.ControlLayer
{
    public class SubscribeResponse : _ControlLayerBase, IControlLayer
    {
        public SubscribeResponse(ref string[] dataInput): base(messageType.SubscribeResponse)
        {
            Console.ReadLine();
        }
        
        public string ToMessage(SessionToken sessionToken, string requestID)
        {
            throw new NotImplementedException();
        }
        
    }
}
