using System.ComponentModel.DataAnnotations;

namespace BussPushNotification.Models
{
    public class User
    {
        public Guid UserID { get; private set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string UserPassword { get; set; } = string.Empty;
    }
}
