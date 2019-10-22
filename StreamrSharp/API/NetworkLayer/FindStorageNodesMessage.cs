using System;
namespace StreamrSharp.API.NetworkLayer
{
    public class FindStorageNodesMessage : _NetworkLayerBase
    {
        public FindStorageNodesMessage() : base(messageType.FindStorageNodesMessage)
        {
        }
    }
}
