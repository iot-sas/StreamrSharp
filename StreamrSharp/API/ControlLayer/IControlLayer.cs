using System;
using static StreamrSharp.API.ControlLayer._ControlLayerBase;

namespace StreamrSharp.API.ControlLayer
{
    public interface IControlLayer
    {
    
        uint         Version        { get; set; }
        messageType  MessageType    { get; }

        string ToMessage(SessionToken sessionToken, string requestID);
    }
}
