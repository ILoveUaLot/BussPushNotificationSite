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
            double lowerLatitude = Area.GetLowerCorner().Coordinates["latitude"];
            double lowerLongitude = Area.GetLowerCorner().Coordinates["longitude"];
            double upperLatitude = Area.GetUpperCorner().Coordinates["latitude"];
            double upperLongitude = Area.GetUpperCorner().Coordinates["longitude"];
            double theta = lowerLatitude - upperLatitude;
            double distance = 60 * 1.1515 * (180 / Math.PI) * Math.Acos(
                Math.Sin(lowerLatitude * (Math.PI / 180) * Math.Sin(upperLatitude * (Math.PI / 180)) +
                Math.Cos(lowerLatitude * (Math.PI / 180) * Math.Cos(upperLatitude * (Math.PI / 180)) * Math.Cos(theta * (Math.PI / 180))
            );
            return Math.Round(distance * 1.609344, 2) / 2;
        }
    }

    public class Area
    {
        private readonly Dictionary<string, Point> Corners;
        public Area([JsonProperty("Envelope")][JsonConverter(typeof(EnvelopeToPointArrayConverter))] Point[] points)
        {
            Corners = new Dictionary<string, Point>
            {
                { "lowerCorner", points[0] },
                { "upperCorner", points[1] }
            };
        }
        public Point GetLowerCorner()
        {
            return Corners["lowerCorner"];
        }

        public Point GetUpperCorner()
        {
            return Corners["upperCorner"];
        }
    }
    public class Point
    {
        public Point([JsonProperty("pos")]string position)
        {
            string[] s = position.Split(" ");
            Coordinates = new Dictionary<string, double>
            {
                {"latitude", double.Parse(s[0]) },
                {"longitude", double.Parse(s[1]) }
            };
        }
        public readonly Dictionary<string, double> Coordinates;
    }
}
