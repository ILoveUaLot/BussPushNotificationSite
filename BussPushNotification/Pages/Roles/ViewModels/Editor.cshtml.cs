using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BussPushNotification.Pages.Roles
{
    public class EditorModel : AdminPageModel
    {
        public UserManager<IdentityUser> UserManager { get; set; }
        public RoleManager<IdentityRole> RoleManager { get; set; }
        public EditorModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        public IdentityRole Role { get; set; }

        public Task<IList<IdentityUser>> Members() => UserManager.GetUsersInRoleAsync(Role.Name);
        public async Task<IEnumerable<IdentityUser>> NonMembers() => UserManager.Users.ToList().Except(await Members());
        public async Task OnGetAsync(string id)
        {
            Role = await RoleManager.FindByIdAsync(id);
        }
        public async Task<IActionResult> OnPostAsync(string userid, string roleName)
        {
            Role = await RoleManager.FindByNameAsync(roleName);
            IdentityUser user = await UserManager.FindByIdAsync(userid);
            IdentityResult result;
            if(await UserManager.IsInRoleAsync(user, roleName))
            {
                result = await UserManager.RemoveFromRoleAsync(user, roleName);
            }
            else
            {
                result = await UserManager.AddToRoleAsync(user, roleName);
            }
            if (result.Succeeded)
            {
                return RedirectToPage();
            }
            else
            {
                foreach (IdentityError errs in result.Errors)
                {
                    ModelState.AddModelError("", errs.Description);
                }
                return Page();
            }
        }
    }
}
