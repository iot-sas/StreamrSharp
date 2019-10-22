using System;
namespace StreamrSharp.API.NetworkLayer
{
    public class _NetworkLayerBase
    {
    
        public enum messageType
        {
            StatusMessage           = 0,
            InstructionMessage      = 1,
            FindStorageNodesMessage = 2,
            StorageNodesMessage     = 3,
            WrapperMessage          = 4
        }

        public string       Version        { get; private set; } = "1";
        public messageType  MessageType    { get; private set; }
    
        public _NetworkLayerBase(messageType MsgType)
        {
            MessageType = MsgType;
        }
    }
}
