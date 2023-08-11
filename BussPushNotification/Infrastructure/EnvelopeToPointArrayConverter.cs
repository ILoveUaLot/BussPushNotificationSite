using BussPushNotification.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BussPushNotification.Infrastructure
{
    public class EnvelopeToPointArrayConverter : JsonConverter<Point[]>
    {
        public override Point[]? ReadJson(JsonReader reader, Type objectType, Point[]? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject envelope = JObject.Load(reader);

            string lowerCorner = envelope["lowerCorner"].ToString();
            string upperCorner = envelope["upperCorner"].ToString();

            return new[] { new Point(lowerCorner), new Point(upperCorner) };
        }

        public override void WriteJson(JsonWriter writer, Point[]? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
