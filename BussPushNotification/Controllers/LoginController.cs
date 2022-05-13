using BussPushNotification.Data;
using BussPushNotification.Data.Interface;
using BussPushNotification.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BussPushNotification.Controllers
{
    public class LoginController : Controller
    {
        IRepository<User> db;
        public LoginController(IRepository<User> repository)
        {
            db = repository;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public ViewResult SignUpForm()
        {
            return View();
        }

        [HttpPost]
        public ViewResult SignUpForm(User user)
        {
            if(ModelState.IsValid)
            {
                Console.WriteLine("sad");
                return View("Login");
            }
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            return View();
        }
    }
}
