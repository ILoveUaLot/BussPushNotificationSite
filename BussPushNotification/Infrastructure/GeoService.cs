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

        public (string? country, string? region, string? settlement, string? street) ParsedAddress(string location)
        {
            if (!string.IsNullOrEmpty(location))
            {
                var splittedLocation = location.Split(',');
                if(splittedLocation.Length >= 3)
                {
                    string country = splittedLocation[0].Trim();
                    string region = splittedLocation[1].Trim();
                    string settlement = splittedLocation[2].Trim();
                    string street = splittedLocation[3].Trim();
                    return (country, region, settlement, street);
                }
            }
            else
            {
                return (null,null,null,null);
            }
        }
    }
}
