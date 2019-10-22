using System;
using System.Net;
using System.Reflection;
using Newtonsoft.Json;
using RestSharp;
using StreamrSharp.API.ControlLayer;

namespace StreamrSharp
{
    public class Stream<T>
    {

        StreamrClient Client;

        public string        StreamID                  { get; private set; }
        public string        StreamName                { get; private set; }
        public string        StreamDescription         { get; private set; }
        public T             MessageClass              { get; private set; }
        
        public event EventHandler<Message<T>> Message;

        public Stream(StreamrClient client)
        {
            Client = client;
        }
    


        public void Subscribe(string id)
        {
            StreamID = id;
            var subscribe = new SubscribeRequest(StreamID);
            Client.Send(subscribe);
            
            Client.BroadcastMessage += (sender, e) =>
            {
                if (e.streamMessage.StreamID == StreamID)
                {
                    var message = new Message<T>(e.streamMessage);                
                    Message?.Invoke(this,message);
                }
            };
        }

        public void Unsubscribe()
        {
            var unsubscribe = new UnsubscribeRequest(StreamID);
            Client.Send(unsubscribe);
        }

        public bool CreateStream(string StreamName, string StreamDescription)
        {
        
            var jsonClass = PopulateClass(typeof(T));
            var json = JsonConvert.SerializeObject(jsonClass).Replace("0", "\"number\"");
                
            var client = new RestClient(Client.RestURL);

            var request = new RestRequest("streams", Method.POST);
            request.AddHeader("Authorization", $"Bearer {Client.sessionToken.token}");
            var jsonToSend = $"{{\"name\":\"{StreamName}\",\"description\":\"{StreamDescription}\",\"config\":{json}}}";
            request.AddParameter("application/json; charset=utf-8", jsonToSend, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;

            var response = client.Execute(request);
            return (response.StatusCode == HttpStatusCode.OK);
        }


        object PopulateClass(Type classType)
        {
            var jsonClass = Activator.CreateInstance(classType);
    
            var fields = classType.GetFields();
            foreach (var field in fields)
            {

                if (field.FieldType == typeof(string))
                {
                    field.SetValue(jsonClass, "string");
                }
                else if (field.FieldType == typeof(string[]))
                {
                    field.SetValue(jsonClass, new String[] { "string", "string" });
                }
                else if (field.FieldType.IsClass)
                {
                    var newclass = PopulateClass(field.FieldType);
                    field.SetValue(jsonClass, newclass); 
                }
            }
            
            return jsonClass;        
        }
        
    }
}
