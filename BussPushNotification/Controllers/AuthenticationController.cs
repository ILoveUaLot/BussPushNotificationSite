using BussPushNotification.Data;
using BussPushNotification.Data.Interface;
using BussPushNotification.Models;
using BussPushNotification.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BussPushNotification.Controllers
{
    public class AuthenticationController : Controller
    {
        private SignInManager<IdentityUser> _signInManager;
        public AuthenticationController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel userModel)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result
                = await _signInManager.PasswordSignInAsync(userModel.UserName, userModel.UserPassword, false, false);
                if (result.Succeeded)
                {
                    return View(userModel.ReturnUrl ?? "/");
                }
                ModelState.AddModelError("", "Invalid username or password");
            }
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
                    UserPassword = userModel.UserPassword
                };
                return View("Login");
            }
            return View();
        }
    }
}
