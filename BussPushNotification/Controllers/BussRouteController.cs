using BussPushNotification.Data.Interface;
using Newtonsoft.Json;
using BussPushNotification.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.ObjectPool;
using System.Net.Http.Headers;
using BussPushNotification.Data;
using System.Text.Json.Nodes;
using System.Runtime.CompilerServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BussPushNotification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BussRouteController : ControllerBase
    {
        IApiRepositroy db;
        private readonly IHttpClientFactory _httpClientFactory;
        string? apiKey;
        IMemoryCache cashe;
        public BussRouteController(IApiRepositroy db, IHttpClientFactory httpClientFactory, IMemoryCache memory)
        {
            this.db = db;
            apiKey = db.GetItemAsync("schedule").Result.Apikey;
            _httpClientFactory = httpClientFactory;
            cashe = memory;
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<string> GetStationList(string casheKey)
        {
            if (!cashe.TryGetValue(casheKey, out var stationList))
            {
                var client = _httpClientFactory.CreateClient("station_listAPI");
                try
                {
                    HttpResponseMessage respone = await client.GetAsync($"?apikey={apiKey}");
                    if (respone.IsSuccessStatusCode)
                    {

                        stationList = await respone.Content.ReadAsStringAsync();
                        cashe.Set(casheKey, stationList);
                    }
                    else throw new Exception("Failed to fetch station list");

                }
                catch (Exception ex)
                {
                    throw new BadHttpRequestException(ex.Message);
                }
            }
            return stationList as string;
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<string> GetSchedules(string casheKey, string stationCode)
        {
            if(!cashe.TryGetValue(casheKey, out var schedules))
            {
                var client = _httpClientFactory.CreateClient("schedule");
                try
                {
                    HttpResponseMessage respone = await client.GetAsync($"?apikey={apiKey}&station={stationCode}&transport_types=bus&limit=500");
                    if (respone.IsSuccessStatusCode)
                    {
                        schedules = await respone.Content.ReadAsStringAsync();
                        cashe.Set(casheKey, schedules);
                    }
                    else throw new Exception("Failed to fetch schedules");
                }
                catch(Exception ex)
                {
                    throw new BadHttpRequestException(ex.Message);
                }
            }
            return schedules as string;
        }
        
        // GET api/<BussRouteController>/5
        [HttpGet("stations/{Country}/{Region}/{Settlement}")]
        public async Task<IActionResult> GetStations(string Country, string Region, string Settlement)
        {
            string WorldStations = await GetStationList("StationList");

            Country = Country.ToLowerInvariant();
            Region = Region.ToLowerInvariant();
            Settlement = Settlement.ToLowerInvariant();
            if (!string.IsNullOrEmpty(WorldStations))
            {
                Root StationList = JsonConvert.DeserializeObject<Root>(WorldStations);
                var res = StationList.Countries
                                        .Where(c => c.Title.ToLowerInvariant() == Country)
                                        .SelectMany(c => c.Regions)
                                        .Where(r => r.Title.ToLowerInvariant().Contains(Region))
                                        .SelectMany(r => r.Settlements)
                                        .Where(s => s.Title.ToLowerInvariant() == Settlement)
                                        .SelectMany(s => s.Stations)
                                        .Where(s => s.Station_type == "bus_stop")
                                        .ToList();
                if (res.Count == 0) return NotFound();
                return Ok(res);
            }
            else
            {
                return NotFound("Cant to receive station list for requested location");
            }
        }

        [HttpGet("codes/{code}")]
        public async Task<IActionResult> GetBusRootCodes(string code)
        {
            string schedule = await GetSchedules("Schedules", code);

            if (!string.IsNullOrEmpty(schedule))
            {
                ScheduleRoot scheduleRoot = JsonConvert.DeserializeObject<ScheduleRoot>(schedule);
                var BusCodes = scheduleRoot.Schedules.Select(x => x.Thread.Number)
                        .Union(scheduleRoot.Interval_Schedules.Select(x => x.Thread.Number));
                return Ok(BusCodes);
            }
            else
            {
                return NotFound("Cant to receive schedules for this station");
            }
        }
        [HttpGet("schedules/{StationCode}/{BussRootCode}")]
        public async Task<IActionResult> GetBusSchedule(string StationCode,string BussRootCode)
        {
            string schedule = await GetSchedules("Schedules", StationCode);
            if (!string.IsNullOrEmpty(schedule))
            {
                ScheduleRoot scheduleRoot = JsonConvert.DeserializeObject<ScheduleRoot>(schedule);
                var res = scheduleRoot.Schedules
                        .Where(x => x.Thread.Number == BussRootCode)
                        .Select(x => new { x.Thread.Number, x.Arrival });
                if (res.Count() == 0)
                {
                    var res2 = scheduleRoot.Interval_Schedules
                         .Where(x => x.Thread.Number == BussRootCode)
                         .Select(x => new { x.Thread.Number, x.Thread.Interval });

                    return res2.Count() == 0 ? NotFound("Unable to find schedule for this root")
                                      : Ok(res2);
                }
                return Ok(res);
            }
            else
            {
                return NotFound();
            }
        }
        // POST api/<BussRouteController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BussRouteController>/5 s9815656
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BussRouteController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}