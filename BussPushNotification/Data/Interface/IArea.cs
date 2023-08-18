namespace BussPushNotification.Data.Interface
{
    public interface IArea
    {
        ICoordinate LowerCorner { get; }
        ICoordinate UpperCorner { get; }
    }
}
