using BussPushNotification.Data;
using BussPushNotification.Data.Interface;
using BussPushNotification.Models;
using BussPushNotification.ViewModels;
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
        public ViewResult SignUpForm(RegisterViewModel userModel)
        {
            if(ModelState.IsValid)
            {
                User user = new User()
                {
                    UserID = Guid.NewGuid(),
                    UserName = userModel.UserName,
                    UserEmail = userModel.UserEmail,
                    UserPassword = userModel.UserPassword,
                };
                db.Create(user);
                db.Save();
                return View("Login");
            }
            return View();
        }
    }
}
