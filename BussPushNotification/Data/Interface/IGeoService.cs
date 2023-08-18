namespace BussPushNotification.Data.Interface
{
    public interface IGeoService
    {
        double GetRadius(IArea area);
        (string country, string settlement, string region) ParsedAddress(string location);
    }
}
