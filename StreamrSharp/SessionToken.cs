using System;
using System.Net;
using Newtonsoft.Json;
using RestSharp;

namespace StreamrSharp
{
    public class SessionToken
    {
    
        [JsonProperty("token")]
        public string      token       { get; private set; }
        [JsonProperty("expires")]
        public DateTime    expires     { get; private set; }
    
        public SessionToken()
        {
        }
        
        static public SessionToken Authenticate(string Username, string Password, string restURL)
        {
            var client = new RestClient(restURL);

            var request = new RestRequest("login/password", Method.POST);
            
            var jsonToSend = $"{{\"username\": \"{Username}\",\"password\": \"{Password}\"}}";
            request.AddParameter("application/json; charset=utf-8", jsonToSend, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;

            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<SessionToken>(response.Content);
            }

            return null;
        }
        
        static public SessionToken Authenticate(string APIKey, string restURL)
        {
            var client = new RestClient(restURL);

            var request = new RestRequest("login/apikey", Method.POST);
            
            var jsonToSend = $"{{\"apiKey\": \"{APIKey}\"}}";
            request.AddParameter("application/json; charset=utf-8", jsonToSend, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;

            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<SessionToken>(response.Content);
            }

            return null;
        }
    }
}
