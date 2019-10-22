using System;
namespace StreamrSharp.API.ControlLayer
{
    public class _ControlLayerBase 
    {

        public enum messageType
        {
            BroadcastMessage = 0,
            UnicastMessage = 1,
            SubscribeResponse = 2,
            UnsubscribeResponse = 3,
            ResendResponseResending = 4,
            ResendResponseResent = 5,
            ResendResponseNoResend = 6,
            ErrorResponse = 7,
            PublishRequest = 8,
            SubscribeRequest = 9,
            UnsubscribeRequest = 10,
            ResendLastRequest = 11,
            ResendFromRequest = 12,
            ResendRangeRequest = 13
        }

        public uint         Version { get; set; } = 1;
        public messageType  MessageType    { get; private set; }
    
        public _ControlLayerBase(messageType MsgType)
        {
            MessageType = MsgType;
        }
    }
}
