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
        string WorldStations;
        IMemoryCache cashe;
        public BussRouteController(IApiRepositroy db, IHttpClientFactory httpClientFactory, IMemoryCache memory)
        {
            this.db = db;
            apiKey = db.GetItemAsync("schedule").Result.Apikey;
            _httpClientFactory = httpClientFactory;
            cashe = memory;
        }

        // GET: api/<BussRouteController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var client = _httpClientFactory.CreateClient("station_listAPI");
            try
            {

                HttpResponseMessage respone = await client.GetAsync($"?apikey={apiKey}");
                if (respone.IsSuccessStatusCode)
                {
                    cashe.Set("StationList", await respone.Content.ReadAsStringAsync());
                    return Ok();
                }
                else return NotFound();
                
            }
            catch (Exception ex)
            {
                throw new BadHttpRequestException(ex.Message);
            }
        }

        // GET api/<BussRouteController>/5
        [HttpGet("{Country}/{Region}/{Settlement}")]
        public IActionResult GetStations(string Country, string Region, string Settlement)
        {
            Country = Country.ToLowerInvariant();
            Region = Region.ToLowerInvariant();
            Settlement = Settlement.ToLowerInvariant();
            if (cashe.TryGetValue("StationList", out WorldStations))
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
                return NotFound("First you need to get world stations");
            }
        }
        
        [HttpGet("{code}")]
        public async Task<IActionResult> GetBusRootCodes(string code)
        {
            var client = _httpClientFactory.CreateClient("schedule");
            try
            {
                HttpResponseMessage response = await client.GetAsync($"?apikey={apiKey}&station={code}&transport_types=bus&limit=500");
                if (response.IsSuccessStatusCode)
                {
                    var schedule = await response.Content.ReadAsStringAsync();
                    var res = JsonConvert.DeserializeObject<ScheduleRoot>(schedule);
                    cashe.Set("Schedules", res);

                    var BusCodes = res.Schedules.Select(x => x.Thread.Number)
                        .Union(res.Interval_Schedules.Select(x=>x.Thread.Number));
                    return Ok(BusCodes);
                }
                else
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                throw new BadHttpRequestException(ex.Message);
            }
        }
        [HttpGet("{StationCode}/{BussRootCode}")]
        public IActionResult GetBusSchedule(string BussRootCode)
        {
            try
            {
                var CashedSchedule = cashe.Get("Schedules");
                if (CashedSchedule is ScheduleRoot)
                {
                    var res = (CashedSchedule as ScheduleRoot).Schedules.Where(x => x.Thread.Number == BussRootCode).Select(x => new { x.Thread.Number, x.Arrival });
                    if (res.Count() == 0)
                    {
                        var res2 = (CashedSchedule as ScheduleRoot).Interval_Schedules
                             .Where(x => x.Thread.Number == BussRootCode)
                             .Select(x => new {x.Thread.Number, x.Thread.Interval});

                        if (res2.Count() == 0)
                            return NotFound("Unable to find schedule for this root");
                        else
                            return Ok(res2);
                    }
                    return Ok(res);

                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                throw new BadHttpRequestException(ex.Message);
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