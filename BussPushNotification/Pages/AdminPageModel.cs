using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BussPushNotification.Pages
{
    [Authorize(Roles = "Admins")]
    public class AdminPageModel : PageModel
    {
    }
}
