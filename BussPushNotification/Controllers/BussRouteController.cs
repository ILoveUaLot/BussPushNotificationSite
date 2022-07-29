using BussPushNotification.Data.Interface;
using Newtonsoft.Json;
using BussPushNotification.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.ObjectPool;
using System.Net.Http.Headers;
using BussPushNotification.Data;
using System.Text.Json.Nodes;

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
                    cashe.Set(apiKey, await respone.Content.ReadAsStringAsync());
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
            if (cashe.TryGetValue(apiKey, out WorldStations))
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
        [HttpGet]
        public async IActionResult GetSchedule(string code)
        {
            var client = _httpClientFactory.CreateClient("schedule");
            try
            {
                HttpResponseMessage response = await client.GetAsync($"?apikey={apiKey}");
                
            }
        }
        // POST api/<BussRouteController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BussRouteController>/5
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