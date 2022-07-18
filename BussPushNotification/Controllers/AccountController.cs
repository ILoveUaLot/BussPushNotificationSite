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
        public Account AccountInfo { get; private set; }
        public AccountController(UserManager<IdentityUser> userManager)
        {
            UserManager = userManager;
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

        [HttpPost("/Profile/{Id}")]
        public async Task<ViewResult> Profile(Account acc)
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
                    return View(AccountInfo);
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
