using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BussPushNotification.Controllers
{
    public class AccountController : Controller
    {
        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }
    }
}
