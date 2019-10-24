# Streamr Sharp

 - .NET Streamr websocket client
 -  Generics support

**NOTE:  This is in development, expect breaking changes.**

#### Make connection  
```
using (var streamrClient = new StreamrClient("https://www.streamr.com/api/v1", "wss://www.streamr.com/api/v1/ws"))  
    {  
        var token = SessionToken.Authenticate("WrSBitdlTNW2dnlDzI4PHwVtmClbsIT2eVgYxUl0x2ew", streamrClient.RestURL);  
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
    public double? Long { get; set; }
    
    [JsonProperty("oday")]        
    public DateTime? Oday { get; set; }

    [JsonProperty("lat")]
    public double? Lat { get; set; }

    [JsonProperty("odo")]
    public long? Odo { get; set; }

    [JsonProperty("oper")]
    public long? Oper { get; set; }

    [JsonProperty("desi")]
    public string Desi { get; set; }

    [JsonProperty("veh")]
    public long? Veh { get; set; }

    [JsonProperty("tst")]
    public DateTime? Tst { get; set; }

    [JsonProperty("dir")]
    public string Dir { get; set; }

    [JsonProperty("tsi")]
    public long? Tsi { get; set; }

    [JsonProperty("hdg")]
    public long? Hdg { get; set; }

    [JsonProperty("start")]
    public string Start { get; set; }

    [JsonProperty("dl")]
    public long? Dl { get; set; }

    [JsonProperty("jrn")]
    public long? Jrn { get; set; }

    [JsonProperty("line")]
    public long? Line { get; set; }

    [JsonProperty("spd")]
    public double? Spd { get; set; }

    [JsonProperty("drst")]
    public long? Drst { get; set; }

    [JsonProperty("acc")]
    public double? Acc { get; set; }
}
```

#### Subscribe to stream
```
var HelsinkiTrams = "7wa7APtlTq6EC5iTCBy6dw";

var Stream = new Stream<trackerMessage>(streamrClient);  
Stream.Subscribe(HelsinkiTrams);  
Stream.Message+= (sender, e) =>
{
   Console.WriteLine($"{e.Data.GPS.lat} {e.Data.GPS.lon}");
};
```
