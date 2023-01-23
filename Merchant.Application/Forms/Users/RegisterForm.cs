using System.ComponentModel.DataAnnotations;
using Merchant.Application.Validations;
using Merchant.Common.Const;

namespace Merchant.Application.Forms.Users;

public class RegisterForm
{
    [Required]
    [UniqueIdentifierValidation(Type = Enums.UniqueIdentifier.UserName)]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
    public string PasswordConfirm { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [EmailAddress]
    [Compare(nameof(Email), ErrorMessage = "Email do not match")]
    public string EmailConfirm { get; set; }
    [Required]
    [Phone]
    [UniqueIdentifierValidation(Type = Enums.UniqueIdentifier.PhoneNumber)]
    public string PhoneNumber { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public Enums.Gender Gender { get; set; }
    [Required]
    public string? Address { get; set; }
    [Required]
    [DataType(DataType.Date)]
    [DoBDateValidation]
    public DateTime DateOfBirth { get; set; }
}