namespace BussPushNotification.Models
{
    public class Route
    {
        public readonly string Title;
        public readonly string RouteNumber;
        public readonly string Arrival;
        public TimeSpan delay { get; set; } = TimeSpan.FromMinutes(10);
        public Route(string title, string routeNumber, string arrival, TimeSpan delay)
        {
            Title = title;
            RouteNumber = routeNumber;
            Arrival = arrival;
            this.delay = delay;
        }
    }
}
