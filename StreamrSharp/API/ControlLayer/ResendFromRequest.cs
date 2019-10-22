using System;
namespace StreamrSharp.API.ControlLayer
{
    public class ResendFromRequest : _ControlLayerBase, IControlLayer
    {
        public ResendFromRequest(ref string[] dataInput): base(messageType.ResendFromRequest)
        {
        }
        
        public string ToMessage(SessionToken sessionToken)
        {
            throw new NotImplementedException();
        }

    }
}
