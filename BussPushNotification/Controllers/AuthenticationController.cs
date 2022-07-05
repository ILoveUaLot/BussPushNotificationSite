using BussPushNotification.Data;
using BussPushNotification.Data.Interface;
using BussPushNotification.Models;
using BussPushNotification.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BussPushNotification.Controllers
{
    public class AuthenticationController : Controller
    {
        IRepository<User> db;
        public AuthenticationController(IRepository<User> repository)
        {
            db = repository;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Login(LoginViewModel userModel)
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
