﻿using System.ComponentModel.DataAnnotations;

namespace MotorcycleStore.WebApp.MVC.Models;

public class UserLoginViewModel
{
    [Required(ErrorMessage = "The {0} field is required")]
    [EmailAddress(ErrorMessage = "The {0} field is in an invalid format")]
    public string Email { get; set; }

    [Required(ErrorMessage = "The {0} field is required")]
    [StringLength(100, ErrorMessage = "The {0} field must be between {2} and {1} characters", MinimumLength = 6)]
    public string Password { get; set; }
}
