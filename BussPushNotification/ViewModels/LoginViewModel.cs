using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BussPushNotification.ViewModels
{
    public class LoginViewModel
    {
        [FromQuery(Name = "ReturnUrl")]
        [BindProperty(SupportsGet = true)]
        public string? ReturnUrl { get; set; }
            
        
        [Required]
        [BindProperty]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [BindProperty]
        public string UserPassword { get; set; } = string.Empty;
    }
}
