using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route = BussPushNotification.Models.Route;
namespace BussPushNotification.Controllers
{
    public class RouteController : Controller
    {
        // GET: RouteController
        public ActionResult Index()
        {
            return View();
        }

        // GET: RouteController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RouteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RouteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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

        // POST: RouteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RouteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RouteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
