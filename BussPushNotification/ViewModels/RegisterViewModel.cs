using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BussPushNotification.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,100}$", 
                            ErrorMessage = "Password must have 1 uppercase letter, 1 lowercase letter, 1 number")]
        public string UserPassword { get; set; }
        [Required]
        [Compare("UserPassword", ErrorMessage = "Passwords do not match")]
        public string RepeatedPassword { get; set; }

    }
}
