using System;
namespace StreamrSharp.API.ControlLayer
{
    public class ErrorResponse : _ControlLayerBase, IControlLayer
    {
        public ErrorResponse(ref String[] dataInput) : base(messageType.ErrorResponse)
        {
        }
        
        public string ToMessage(SessionToken sessionToken)
        {
            throw new NotImplementedException();
        }

    }
}
