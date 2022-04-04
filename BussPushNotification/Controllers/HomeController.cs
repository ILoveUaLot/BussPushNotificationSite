using Microsoft.AspNetCore.Mvc;

namespace BussPushNotification.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
