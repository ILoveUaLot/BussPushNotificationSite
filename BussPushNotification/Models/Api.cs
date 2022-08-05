using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BussPushNotification.Models;

public class Api
{
    [Key]
    public string Apiname { get; set; } = null!;

    public string? Apikey { get; set; }
}
