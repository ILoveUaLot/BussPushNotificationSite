using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BussPushNotification.ViewModels
{
    public class RegisterViewModel
    {
        [BindProperty(SupportsGet =true)]
        public string? ReturnUrl { get; set; }
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }
        [Required]
        public string UserPassword { get; set; }
        [Required]
        [Compare("UserPassword", ErrorMessage = "Passwords do not match")]
        public string RepeatedPassword { get; set; }

    }
}
