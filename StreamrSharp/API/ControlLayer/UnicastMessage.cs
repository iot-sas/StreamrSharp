using System;
namespace StreamrSharp.API.ControlLayer
{
    public class UnicastMessage : _ControlLayerBase, IControlLayer
    {
        public UnicastMessage(ref string[] dataInput): base(messageType.UnicastMessage)
        {
        }
        public string ToMessage(SessionToken sessionToken)
        {
            throw new NotImplementedException();
        }
        
    }
}
