using BussPushNotification.Data;
using Newtonsoft.Json;

namespace BussPushNotification.Models
{
    public class StationDetails
    {
        public string Title { get; set; }
        [JsonProperty("codes")]
        public Codes Сodes { get; set; }

        public List<string> BussRoutes { get; set; } = new List<string>();
    }
}
