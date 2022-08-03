using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BussPushNotification.Data
{
    public class ScheduleRoot
    {
        public string? Date { get; set; }
        public Pagination Pagination { get; set; }
        public Station Station { get; set; }
        [JsonProperty("schedule")]
        public Schedule[] Schedules { get; set; }
        [JsonProperty("interval_schedule")]
        public Interval_Schedule[] Interval_Schedules { get; set; }
        public Schedule_direction Schedule_Direction { get; set; }
        public Directions Directions { get; set; }
    }
    public class Pagination
    {
        public int Total { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
    }
    public class Station
    {
        public string Code { get; set; }
        public string Station_Type { get; set; }
        public string Station_Type_Name { get; set; }
        public string Title { get; set; }
        public string Popular_title { get; set; }
        public string Short_title { get; set; }
        public Codes Codes { get; set; }
        public string Transport_type { get; set; }
        public string Type { get; set; }
    }

    public class Schedule
    {
        public string Except_days { get; set; }
        public string Arrival { get; set; }
        public Thread Thread { get; set; }
        public bool Is_fuzzy { get; set; }
        public string Days { get; set; }
        [StringLength(1000)]
        public string Stops { get; set; }
        public string Departure { get; set; }
    }
    public class Interval_Schedule : Schedule
    {

        private IntervalThread _thread;
        [JsonProperty("thread")]
        public new IntervalThread Thread
        {
            get { return _thread; }
            set { _thread = value; }
        }

    }

    public class Interval
    {
        public string Density { get; set; }
        public string Title { get; set; }
        public string Begin_time { get; set; }
        public string End_time { get; set; }
    }
    public class Schedule_direction
    {
        public string Code { get; set; }
        public string Title { get; set; }
    }
    public class Thread
    {
        [StringLength(100)]
        public string UID { get; set; }
        public string Title { get; set; }
        public string Number { get; set; }
        public string Short_title { get; set; }
        public string Transport_type { get; set; }
        public string? Express_type { get; set; }
    }

    public class IntervalThread : Thread
    {
        public Interval Interval { get; set; }
    }
    public class Carrier_codes
    {
        public string icao { get; set; }
        public string sirena { get; set; }
        public string iata { get; set; }
    }
    public class Directions
    {
        public string Code { get; set; }
        public string Title { get; set; }
    }
}
