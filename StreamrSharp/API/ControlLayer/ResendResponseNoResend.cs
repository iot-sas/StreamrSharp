using System;
namespace StreamrSharp.API.ControlLayer
{
    public class ResendResponseNoResend : _ControlLayerBase, IControlLayer
    {
        public ResendResponseNoResend(ref string[] dataInput): base(messageType.ResendResponseNoResend)
        {
        }

        public string ToMessage(SessionToken sessionToken)
        {
            throw new NotImplementedException();
        }
        
    }
}
