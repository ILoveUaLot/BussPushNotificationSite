using System.ComponentModel.DataAnnotations;

namespace BussPushNotification.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string UserPassword { get; set; } = string.Empty;
    }
}
