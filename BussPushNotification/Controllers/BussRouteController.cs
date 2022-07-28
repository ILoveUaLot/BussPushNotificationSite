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
        public BussRouteController(IApiRepositroy db, IHttpClientFactory httpClientFactory)
        {
            this.db = db;
            apiKey = db.GetItemAsync("schedule").Result.Apikey;
            _httpClientFactory = httpClientFactory;
        }

        // GET: api/<BussRouteController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var client = _httpClientFactory.CreateClient("station_listAPI");
            try
            {
                //HttpResponseMessage response = await client.GetAsync($"?apikey={apiKey}");
                //if (response.IsSuccessStatusCode)
                //{
                //    // Get the response stream
                //    var stream = await response.Content.ReadAsStreamAsync();

                //    // Determine the content type based on the response headers or set it explicitly
                //    var contentType = response.Content.Headers.ContentType?.MediaType ?? "application/octet-stream";

                //    // Return the file content as a FileStreamResult
                //    return new FileStreamResult(stream, new MediaTypeHeaderValue(contentType).ToString())
                //    {
                //        FileDownloadName = "cities.json"
                //    };
                //}
                //else return NotFound();
                HttpResponseMessage response = await client.GetAsync($"?apikey={apiKey}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var res = JsonConvert.DeserializeObject<Root>(jsonResponse);
                    return Content(jsonResponse, "application/json");
                }
                else return NotFound();
            }
            catch (Exception ex)
            {
                throw new BadHttpRequestException(ex.Message);
            }
        }

        // GET api/<BussRouteController>/5
        [HttpGet("{id}")]
        public async Task<string> Get(string id)
        {
            return db.GetItemAsync(id).Result.Apikey;
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
