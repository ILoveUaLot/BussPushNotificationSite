using BussPushNotification.Data.Interface;

namespace BussPushNotification.Infrastructure
{
    public class GeoService : IGeoService
    {
        public double GetDistance(IArea area)
        {
            double R = 6371.0; // radius of earth in km

            double lat1 = area.LowerCorner.Latitude * Math.PI / 180.0;
            double lon1 = area.LowerCorner.Longitude * Math.PI / 180.0;
            double lat2 = area.UpperCorner.Latitude * Math.PI / 180.0;
            double lon2 = area.UpperCorner.Longitude * Math.PI / 180.0;

            double dlat = lat2 - lat1;
            double dlon = lon2 - lon1;

            double a = Math.Sin(dlat / 2) * Math.Sin(dlat / 2) +
                       Math.Cos(lat1) * Math.Cos(lat2) *
                       Math.Sin(dlon / 2) * Math.Sin(dlon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c;
        }

        public (string country, string settlement, string region) ParsedAddress(string location)
        {
            
        }
    }
}
