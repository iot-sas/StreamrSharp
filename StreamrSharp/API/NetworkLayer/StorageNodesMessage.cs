using System;
namespace StreamrSharp.API.NetworkLayer
{
    public class StorageNodesMessage : _NetworkLayerBase
    {
        public StorageNodesMessage(): base(messageType.StorageNodesMessage)
        {
        }
    }
}
