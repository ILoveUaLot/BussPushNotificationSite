using BussPushNotification.Data;
using BussPushNotification.Data.Interface;
using BussPushNotification.Models;
using BussPushNotification.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace BussPushNotification.Controllers
{
    public class AuthenticationController : Controller
    {
        private SignInManager<IdentityUser> _signInManager;
        public UserManager<IdentityUser> UserManager { get; set; }
        public AuthenticationController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            UserManager = userManager;
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
                    return userModel.ReturnUrl != null ? RedirectToPage(userModel.ReturnUrl)
                        : RedirectToAction("Profile","Account", new {id = User.FindFirstValue(ClaimTypes.NameIdentifier)
                        });
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
        public async Task<IActionResult> SignUpForm(RegisterViewModel userModel)
        {
            if(ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser { UserName = userModel.UserName, Email = userModel.UserEmail };
                IdentityResult result = await UserManager.CreateAsync(user, userModel.UserPassword);
                if(result.Succeeded)
                {
                    return View(userModel.ReturnUrl ?? "/");
                }
                foreach(IdentityError err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return View();
        }
    }
}
