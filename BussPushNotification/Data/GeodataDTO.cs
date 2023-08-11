using BussPushNotification.Infrastructure;
using Newtonsoft.Json;

namespace BussPushNotification.Data
{
    public class GeoMetaData
    {
        [JsonProperty("Point")]
        public Point Center { get; set; }

        [JsonProperty("boundedBy")]
        public Area Area { get; set; }

        [JsonProperty("text")]
        public string Address { get; set; }
    }

    public class Area
    {
        [JsonProperty("Envelope")]
        [JsonConverter(typeof(EnvelopeToPointArrayConverter))]
        public Point[] Points { get; set; }
    }
    public class Point
    {
        private string position;
        public Point([JsonProperty("pos")]string position)
        {
            this.position = position;
        }

        public KeyValuePair<string, string>[] Coordinates
        {
            get
            {
                var s = position.Split(" ");
                return new [] { KeyValuePair.Create("latitude", s[0]), KeyValuePair.Create("longitude", s[1]) };
            }
        }
    }
}
