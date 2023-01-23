using System.ComponentModel.DataAnnotations;
using Merchant.Application.Validations;
using Merchant.Common.Const;

namespace Merchant.Application.Forms.Users;

public class UpdateUserForm
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public Enums.Gender? Gender { get; set; }
    public string? Address { get; set; }
    [DataType(DataType.Date)]
    [DoBDateValidation]
    public DateTime? DateOfBirth { get; set; }
}