using Microsoft.AspNetCore.Identity;

namespace BussPushNotification.Models
{
    public class User : IdentityUser
    {
        public Guid UserID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string UserPassword { get; set; } = string.Empty;
    }
}
