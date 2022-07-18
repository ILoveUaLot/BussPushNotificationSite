using Microsoft.AspNetCore.Identity;

namespace BussPushNotification.Models
{
    public class Account
    {
        public string Id { get; init; }
        public byte[]? UserAvatar { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string UserPassword { get; set; } = string.Empty;
    }
    
}
