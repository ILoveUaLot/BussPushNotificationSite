using System.ComponentModel.DataAnnotations;

namespace BussPushNotification.Models
{
    public class User
    {
        public long? UserID { get; private set; }
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string UserEmail { get; set; } = string.Empty;
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string UserPassword { get; set; } = string.Empty;
        [Required]
        [Compare("UserPassword", ErrorMessage = "Passwords do not match.")]
        public string RepeatedPassword { get; set; } = string.Empty;
    }
}
