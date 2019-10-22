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
  
#### Unsubmscribe
```
  var unsubscribe = new UnsubscribeRequest(HelsinkiTrams);  
  streamrClient.Send(unsubscribe);  
}
```  

  
## Using Generics  

#### Create a message class
```
public class trackerMessage  
{  
   public String vehicleName;  
   public DateTime dateTime;  
   public gps GPS;  
  
   public class gps  
   {  
      public double lat;  
      public double lon;  
      public int speed;  
      public long altitude;  
   }  
}  
```
#### Subscribe to stream
```
   var myStreamId = "XXXXXXXXX";  
  
   var Stream = new Stream<trackerMessage>(streamrClient);  
   Stream.Subscribe(myStreamId);  
   Stream.Message+= (sender, e) =>  
   {  
       Console.WriteLine($"{e.Data.GPS.lat} {e.Data.GPS.lon}");  
   };
```
