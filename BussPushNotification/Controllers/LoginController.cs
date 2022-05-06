using Microsoft.AspNetCore.Mvc;

namespace BussPushNotification.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ViewResult SignUpForm()
        {
            return View();
        }
    }
}
