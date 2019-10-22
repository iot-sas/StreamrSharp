using System;
namespace StreamrSharp.API.ControlLayer
{
    public class ResendRangeRequest : _ControlLayerBase, IControlLayer
    {
        public ResendRangeRequest(ref string[] dataInput): base(messageType.ResendFromRequest)
        {
        }

        public string ToMessage(SessionToken sessionToken)
        {
            throw new NotImplementedException();
        }
        
    }
}
