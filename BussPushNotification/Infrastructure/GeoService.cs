using BussPushNotification.Data.Interface;

namespace BussPushNotification.Infrastructure
{
    public class GeoService
    {
        public double GetRadius(IArea area)
        {
            double lowerLatitude = area.LowerCorner.Latitude;
            double lowerLongitude = area.LowerCorner.Longitude;
            double upperLatitude = area.UpperCorner.Latitude;
            double upperLongitude = area.UpperCorner.Longitude;
            double theta = lowerLatitude - upperLatitude;

            double degree = (Math.PI / 180);
            double distance = 60 * 1.1515 * (180 / Math.PI) * Math.Acos(
                Math.Sin(lowerLatitude * degree) * Math.Sin(upperLatitude * degree) +
                Math.Cos(lowerLatitude * degree) * Math.Cos(upperLatitude * degree) * Math.Cos(theta * degree)
            );
            return Math.Round(distance * 1.609344, 2) / 2;
        }
    }
}
