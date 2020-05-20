using System;
namespace StreamrSharp.API.ControlLayer
{
    public class ResendResponseResent : _ControlLayerBase, IControlLayer
    {
        public ResendResponseResent(ref string[] dataInput): base(messageType.ResendResponseResent)
        {
        }

        public string ToMessage(SessionToken sessionToken, string requestID)
        {
            throw new NotImplementedException();
        }
        
    }
}
