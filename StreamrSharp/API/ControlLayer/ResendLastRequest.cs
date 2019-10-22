using System;
namespace StreamrSharp.API.ControlLayer
{
    public class ResendLastRequest : _ControlLayerBase, IControlLayer
    {
        public ResendLastRequest(ref string[] dataInput): base(messageType.ResendFromRequest)
        {
        }

        public string ToMessage(SessionToken sessionToken)
        {
            throw new NotImplementedException();
        }
        
    }
}
