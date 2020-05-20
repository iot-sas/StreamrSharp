using System;
namespace StreamrSharp.API.ControlLayer
{
    public class ResendResponseResending : _ControlLayerBase, IControlLayer
    {
        public ResendResponseResending(ref string[] dataInput): base(messageType.ResendResponseResending)
        {
        }

        public string ToMessage(SessionToken sessionToken, string requestID)
        {
            throw new NotImplementedException();
        }
        
    }
}
