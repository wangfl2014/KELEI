using Newtonsoft.Json;
using System;

namespace KELEI.Commons.Helper
{
    public class LongJsonConvert : JsonConverter

    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            //  Convert.ToInt64(reader.Value);
            return reader.Value;
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());


        }
        public override bool CanConvert(Type objectType)
        {
            if ((objectType == typeof(Int64)))
            {
                return true;
            }
            return false;
        }
    }
}
