using System;
using System.Collections.Generic;
using System.Linq;
using Coordinates.Measurements.Elements;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Coordinates.Measurements.Helpers.Serialization
{
    /// <summary>
    /// TODO This class is not being used due to .NET Core issue with attributes
    /// </summary>
    public class BaseElementJsonConverter : JsonConverter
    {
        private readonly Type[] _types = { typeof(BaseElement) };

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JToken t = JToken.FromObject(value);

            if (t.Type != JTokenType.Object)
            {
                t.WriteTo(writer);
            }
            else
            {
                JObject o = (JObject)t;
                IList<string> propertyNames = o.Properties().Select(p => p.Name).ToList();

                o.AddFirst(new JProperty("Keys", new JArray(propertyNames)));

                o.WriteTo(writer);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Won't enter if CanRead is false.");
        }

        public override bool CanRead => false;

        public override bool CanConvert(Type objectType)
        {
            return _types.Any(t => t == objectType);
        }
    }
}
