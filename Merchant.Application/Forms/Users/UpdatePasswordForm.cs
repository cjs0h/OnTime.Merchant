using System.ComponentModel.DataAnnotations;

namespace Merchant.Application.Forms.Users;

public class UpdatePasswordForm
{
    [Required]
    public string OldPassword { get; set; }
    [Required]
    public string NewPassword { get; set; }
    [Required]
    [Compare(nameof(NewPassword), ErrorMessage = "Passwords do not match")]
    public string NewPasswordConfirm { get; set; }
}