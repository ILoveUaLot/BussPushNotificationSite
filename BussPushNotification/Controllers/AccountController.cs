using BussPushNotification.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BussPushNotification.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public UserManager<IdentityUser> UserManager { get; private set; }
        public SignInManager<IdentityUser> _signInManager;
        public Account AccountInfo { get; private set; } 
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            UserManager = userManager;
            _signInManager = signInManager;
        }
        
        [HttpGet("/Profile/{Id}")]
        public async Task<ViewResult> Profile(string id)
        {
            IdentityUser user = await UserManager.FindByIdAsync(id);
            AccountInfo = new Account() 
            { 
                Id = id,
                UserName = user.UserName,
                UserEmail = user.Email,
            };
            return View(AccountInfo);
        }

        //TODO:
        [HttpPost("/Profile/{Id}/Update")]
        public async Task<IActionResult> AccountUpdate(string id, Account acc)
        {
            if(ModelState.IsValid)
            {
                IdentityUser user = await UserManager.FindByIdAsync(AccountInfo.Id);
                user.UserName = acc.UserName;
                user.Email = acc.UserEmail;

                IdentityResult result = await UserManager.UpdateAsync(user);
                
                if(result.Succeeded && !String.IsNullOrEmpty(acc.UserPassword))
                {
                    await UserManager.RemovePasswordAsync(user);
                    result = await UserManager.AddPasswordAsync(user, acc.UserPassword);
                }

                if (result.Succeeded)
                {
                    AccountInfo = acc;
                    return RedirectToAction("Profile", new {id=AccountInfo.Id});
                }
                foreach(IdentityError err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }    
            }
            return View("Profile", acc);
        }


    }
}
