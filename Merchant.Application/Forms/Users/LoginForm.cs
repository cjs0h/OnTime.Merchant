using System.ComponentModel.DataAnnotations;

namespace Merchant.Application.Forms.Users;

public class LoginForm
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}