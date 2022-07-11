using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BussPushNotification.Pages.Users
{
    public class EditorModel : PageModel
    {
        public UserManager<IdentityUser> UserManager { get; set; }
        public EditorModel(UserManager<IdentityUser> userManager)
        {
            UserManager = userManager;
        }
        [BindProperty]
        public string Id { get; set; } = string.Empty;
        [BindProperty]
        public string Name { get; set; } = string.Empty;
        [BindProperty]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [BindProperty]
        public string? Password { get; set; }
        public async Task OnGetAsync(string id)
        {
            IdentityUser user = await UserManager.FindByIdAsync(id);
            Id = user.Id;
            Name = user.UserName;
            Email = user.Email;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await UserManager.FindByIdAsync(Id);
                user.UserName = Name;
                user.Email = Email;
                IdentityResult result = await UserManager.UpdateAsync(user);
                if(result.Succeeded && !String.IsNullOrEmpty(Password))
                {
                    await UserManager.RemovePasswordAsync(user);
                    result = await UserManager.AddPasswordAsync(user, Password);
                }
                if (result.Succeeded) return RedirectToPage("List");
                foreach (IdentityError errs in result.Errors)
                {
                    ModelState.AddModelError("", errs.Description);
                }
            }
            return Page();
        }
    }
}
