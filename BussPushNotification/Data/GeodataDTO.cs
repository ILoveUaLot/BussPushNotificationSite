using BussPushNotification.Infrastructure;
using Newtonsoft.Json;

namespace BussPushNotification.Data
{
    public class GeoMetaData
    {
        [JsonProperty("Point")]
        public readonly Point Center;

        [JsonProperty("boundedBy")]
        public readonly Area Area;

        [JsonProperty("text")]
        public readonly string Address;

        public double GetRadius()
        {
            double theta = Area.GetLowerCorner()[0]. - Area.Corners["upperCorner"].Value.Coordinates["Latitude"];
        }
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

        public Dictionary<string,double> Coordinates
        {
            get
            {
                string[] s = position.Split(" ");
                double latitude = double.Parse(s[0]);
                double longitude = double.Parse(s[1]);

                return new Dictionary<string, double>
                {
                    { "latitude", latitude },
                    { "longitude", longitude }
                };
            }
        }
    }
}
