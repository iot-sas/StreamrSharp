using System;
using Newtonsoft.Json;

namespace StreamrSharp
{
    public class Message<T>
    {
        public StreamMessage StreamMessage { get; private set; }
        public T             Data          { get; private set; }
        
        public Message(StreamMessage streamMessage)
        {
            Data = JsonConvert.DeserializeObject<T>(streamMessage.Content, JsonSettings.SerializeSettings);
            StreamMessage = streamMessage;
        }
    }
}
