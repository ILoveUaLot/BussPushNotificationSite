using BussPushNotification.Infrastructure;
using Newtonsoft.Json;
using System.Globalization;

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

        public GeoMetaData(Point center, Area area, string address)
        {
            Center = center;
            Area = area;
            Address = address;
        }

        public double GetRadius()
        {
            double lowerLatitude = Area.GetLowerCorner().Coordinates["latitude"];
            double lowerLongitude = Area.GetLowerCorner().Coordinates["longitude"];
            double upperLatitude = Area.GetUpperCorner().Coordinates["latitude"];
            double upperLongitude = Area.GetUpperCorner().Coordinates["longitude"];
            double theta = lowerLatitude - upperLatitude;

            double degree = (Math.PI / 180);
            double distance = 60 * 1.1515 * (180 / Math.PI) * Math.Acos(
                Math.Sin(lowerLatitude * degree) * Math.Sin(upperLatitude * degree) +
                Math.Cos(lowerLatitude * degree) * Math.Cos(upperLatitude * degree) * Math.Cos(theta * degree)
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
            var s0 = double.Parse(s[0], NumberStyles.Float, CultureInfo.InvariantCulture);
            var s1 = double.Parse(s[1], NumberStyles.Float, CultureInfo.InvariantCulture);
            Coordinates = new Dictionary<string, double>
            {
                {"latitude", s0},
                {"longitude", s1}
            };
        }
        public readonly Dictionary<string, double> Coordinates;
    }
}
