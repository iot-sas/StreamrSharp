using System;
namespace StreamrSharp.API.NetworkLayer
{
    public class InstructionMessage : _NetworkLayerBase
    {
        public InstructionMessage() : base(messageType.InstructionMessage)
        {
        }
    }
}
