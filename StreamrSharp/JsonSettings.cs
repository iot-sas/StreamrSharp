using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StreamrSharp
{
    static public class JsonSettings
    {

        public static JsonSerializerSettings SerializeSettings;

        static JsonSettings()
        {
            SerializeSettings = new JsonSerializerSettings()
            {
                Converters = new List<JsonConverter>() { new StreamrDateTimeConverter() },            
            };
        }

    }
    
    public class StreamrDateTimeConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }
    
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var t = (long)reader.Value;
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(t);
        }
    
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue( ((DateTime)value - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc) ).TotalMilliseconds.ToString("0"));
        }
    }
    
}
