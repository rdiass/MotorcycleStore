using System.ComponentModel.DataAnnotations;

namespace MS.Identity.API.Models;

public class User
{
    [Required(ErrorMessage = "This field {0} is mandatory")]
    [EmailAddress(ErrorMessage = "Invalid Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "This field {0} is mandatory")]
    [StringLength(100, ErrorMessage = "The field {0} need to be between {2} and {1} characters", MinimumLength = 6)]
    public string Password { get; set; }

    [Required(ErrorMessage = "This field {0} is mandatory")]
    [Compare("Password", ErrorMessage = "The passwords are different")]
    public string PasswordConfirmation { get; set; }
}
