using BussPushNotification.Data.Interface;
using BussPushNotification.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Route = BussPushNotification.Models.Route;
namespace BussPushNotification.Controllers
{
    public class RouteController : Controller
    {
        public readonly IHttpClientFactory _httpClient;
        public readonly RouteApiSettings _apiSettings;
        public RouteController(IOptions<RouteApiSettings> apiSettings, IHttpClientFactory httpClient, IApiRepository)
        {
            _httpClient = httpClient;
            _apiSettings = apiSettings.Value;

        }

        [HttpGet("/Routes")]
        public async Task<IActionResult> Routes()
        {
            List<Route> routes = new List<Route>()
            {
                new Route("Гатчина, Въезд","К18", "8:30", TimeSpan.FromMinutes(10))
            };
            return View(routes);
        }

        [HttpGet("/Routes/Add")]
        public ViewResult AddRoutes()
        {
            return View();
        }

        [HttpPost("/Routes/Add")]
        public async Task<IActionResult> FindStations(string country, string region, string settlement, string street)
        {
            string apiUrl = $"{_apiSettings.BaseUrl}/api/BussRoute/";
            var client = _httpClient.CreateClient();
            client.BaseAddress = new Uri(apiUrl);

            string jsonStations = await client.GetAsync($"stations/{country}/{region}/{settlement}").Result.Content.ReadAsStringAsync();
            
            List<StationDetails> stationsList= JsonConvert.DeserializeObject<List<StationDetails>>(jsonStations);

            var tasks = stationsList.Select(station =>
                Task.Run(async () =>
                {
                    var response = await client.GetAsync($"StationRoutes/{station.Сodes.yandex_code}");
                    if (response.IsSuccessStatusCode)
                    {
                        station.BussRoutes.Add(await response.Content.ReadAsStringAsync());
                    }
                })).ToArray();

            await Task.WhenAll(tasks);
            return Ok(stationsList);
        }
    }
}
