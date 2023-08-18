using BussPushNotification.Data.Interface;
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
    }

    public class Area : IArea
    {
        public Area([JsonProperty("Envelope")][JsonConverter(typeof(EnvelopeToPointArrayConverter))] Point[] points)
        {
            LowerCorner = points[0];
            UpperCorner = points[1];
        }

        public ICoordinate LowerCorner { get; }

        public ICoordinate UpperCorner { get; }
    }
    public class Point : ICoordinate
    {
        public Point([JsonProperty("pos")]string position)
        {
            string[] s = position.Split(" ");
            var s0 = double.Parse(s[0], NumberStyles.Float, CultureInfo.InvariantCulture);
            var s1 = double.Parse(s[1], NumberStyles.Float, CultureInfo.InvariantCulture);
            Latitude = s0;
            Longitude = s1;
        }

        public double Latitude { get; }
        public double Longitude { get; }
    }
}
