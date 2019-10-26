# Streamr Sharp

 - .NET Streamr websocket client
 -  Generics support, with Stream helper class

**NOTE:  This is in development, expect breaking changes.**

#### Make connection  
```
using (var streamrClient = new StreamrClient("https://www.streamr.com/api/v1", "wss://www.streamr.com/api/v1/ws"))  
    {  
        var token = SessionToken.Authenticate("WrSBitdlTNW2dxxxxxxxxxxxxxxxxxxxxxxxxxx", streamrClient.RestURL);  
        streamrClient.Connect(token);
```
  
#### Hook into data stream event  
```
streamrClient.NetworkMessage += (sender, e) =>  
    {  
         switch (e.MessageType)  
         {  
            case _ControlLayerBase.messageType.BroadcastMessage:  
                Console.WriteLine(((BroadcastMessage)e).streamMessage.Content);  
                break;  
            case default:  
                Console.WriteLine(e.GetType());  
                break;  
        }  
    };  
```
  
#### Subscribe
```
  var HelsinkiTrams = "7wa7APtlTq6EC5iTCBy6dw";
  var subscribe = new SubscribeRequest(HelsinkiTrams);  
  streamrClient.Send(subscribe);  
          
  Thread.Sleep(5000);  
```
  
#### Unsubscribe
```
  var unsubscribe = new UnsubscribeRequest(HelsinkiTrams);  
  streamrClient.Send(unsubscribe);  
}
```  

  
## Using Generics  

#### Create a message class
```
    public partial class HelsinkiTrams
    {
        [JsonProperty("long")]
        public double? Longitude { get; set; }
        public DateTime? oday { get; set; }
        [JsonProperty("lat")]
        public double? Latitude { get; set; }
        public long? odo { get; set; }
        public long? oper { get; set; }
        public string desi { get; set; }
        public long? veh { get; set; }
        public DateTime? tst { get; set; }
        public string dir { get; set; }
        public long? tsi { get; set; }
        public long? hdg { get; set; }
        public string start { get; set; }
        public long? dl { get; set; }
        public long? jrn { get; set; }
        public long? line { get; set; }
        [JsonProperty("spd")]
        public double? Speed { get; set; }        
        public long? drst { get; set; }
        public double? acc { get; set; }
    }
```

#### Subscribe to stream
```
var HelsinkiTrams = "7wa7APtlTq6EC5iTCBy6dw";

var Stream = new Stream<trackerMessage>(streamrClient);  
Stream.Subscribe(HelsinkiTrams);  
Stream.Message+= (sender, e) =>
{
   Console.WriteLine($"{e.Data.GPS.Latitude} {e.Data.GPS.Longitude} {e.Data.GPS.Speed}");
};
```
