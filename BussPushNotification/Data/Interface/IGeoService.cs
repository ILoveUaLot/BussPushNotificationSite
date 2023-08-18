namespace BussPushNotification.Data.Interface
{
    public interface IGeoService
    {
        double GetDistance(IArea area);
        (string? country, string? region, string? settlement, string? street) ParsedAddress(string location);
    }
}
