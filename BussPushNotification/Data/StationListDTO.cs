using Newtonsoft.Json;

namespace BussPushNotification.Data
{
    public class Root
    {
        public Countries[] Countries { get; set; }
    }
    public class Countries
    {
        [JsonProperty("regions")]
        public Regions[] Regions { get; set; }
        public Codes Codes { get; set; }
        public string Title { get; set; }
    }
    public class Regions 
    {
        public Settlements[] Settlements { get; set; }
        public Codes Codes { get; set; }
        public string Title { get; set; }
    }
    public class Settlements
    {
        public string Title { get; set; }
        public Codes Codes { get; set; }
        public Stations[] Stations { get; set; }
    }
    public class Stations
    {
        public string Direction { get; set; }
        public Codes Codes { get; set; }
        public string Station_type { get; set; }
        public string Title { get; set; }
        public double? Longitude { get; set; }
        public string Transport_type { get; set; }
        public double? Latitude { get; set; }
    }
    public class Codes
    {
        public string yandex_code { get; set; }
        public string? esr_code { get; set; }
    }
}
