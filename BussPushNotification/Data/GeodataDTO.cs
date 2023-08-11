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
        private Point[] points;
        public Area([JsonProperty("Envelope")][JsonConverter(typeof(EnvelopeToPointArrayConverter))] Point[] points)
        {
            this.points = points;
        }


        public KeyValuePair<string, Point>[] Corners
        {
            get
            {
                return new[]
                {
                    new KeyValuePair<string, Point>("lowerCorner", points[0]),
                    new KeyValuePair<string, Point>("upperCorner", points[1])
                };
            }
        }
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
