using System;
namespace StreamrSharp.API.NetworkLayer
{
    public class StatusMessage : _NetworkLayerBase
    {
        public StatusMessage() : base(messageType.StatusMessage)
        {
        }
    }
}
