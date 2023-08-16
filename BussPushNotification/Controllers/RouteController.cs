using BussPushNotification.Data;
using BussPushNotification.Data.Interface;
using BussPushNotification.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
        public readonly IApiRepository apiKeyRepository;
        public readonly IMemoryCache cache;
        public RouteController(IOptions<RouteApiSettings> apiSettings, IHttpClientFactory httpClient, IApiRepository apiRepository, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _apiSettings = apiSettings.Value;
            apiKeyRepository = apiRepository;
            this.cache = cache;
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

        public async Task<GeoMetaData> GetLocation(string location)
        {
            HttpClient client = _httpClient.CreateClient("geocoder");
            location = "Гатчина проспект 25 октября";
            string apikey = (await apiKeyRepository.GetItemAsync("geocoder")).Apikey;
            HttpResponseMessage response = await client.GetAsync($"?apikey={apikey}&geocode={location}&format=json");
            if (response.IsSuccessStatusCode)
            {
                string res = await response.Content.ReadAsStringAsync();
                JObject geoJson = JObject.Parse(res);

                var metaData = geoJson.SelectTokens("response.GeoObjectCollection.featureMember[0].GeoObject").Select(x => new
                {
                    Point = x.SelectToken("Point"),
                    BoundedBy = x.SelectToken("boundedBy"),
                    Text = x.SelectToken("metaDataProperty.GeocoderMetaData.text")
                }).First();
                GeoMetaData geo = new GeoMetaData(metaData.Point.ToObject<Point>(),
                                                  metaData.BoundedBy.ToObject<Area>(),
                                                  metaData.Text.ToString());
                return geo;
            }
            throw new Exception("Cant find location");
        }
    }
}
