using System.ComponentModel.DataAnnotations;
using Merchant.Common.Const;
using Merchant.Domain.Entities;
using Merchant.Domain.Interfaces.Repositories;

namespace Merchant.Application.Validations;

public class UniqueIdentifierValidation : ValidationAttribute
{
    private IUnitOfWork _unitOfWork;
    public Enums.UniqueIdentifier Type { get; set; }
    public bool CheckDbRecord { get; set; } = true;

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        _unitOfWork = (IUnitOfWork)validationContext.GetService(typeof(IUnitOfWork))!;
        switch (Type)
        {
            case Enums.UniqueIdentifier.UserName:
                if (value is null) return new ValidationResult("Please Enter a UserName");
                return CheckUserName(value.ToString()!) ? ValidationResult.Success! : new ValidationResult("UserName Already in use!");
            case Enums.UniqueIdentifier.PhoneNumber:
                if (value is null) return new ValidationResult("Please Enter a Phone number");
                var number = value.ToString();
                if (number is null || number.Length is < 10 or > 11)
                    return new ValidationResult("Please enter a valid phone number");
                if (number.StartsWith("0"))
                    number = number[1..];
                if (!number.StartsWith("77") && !number.StartsWith("78") && !number.StartsWith("75"))
                    return new ValidationResult("Please enter a valid IRAQI phone number");
                if (CheckDbRecord)
                    return CheckPhone(value.ToString()!)
                        ? ValidationResult.Success!
                        : new ValidationResult("Phone Number Already in use!");
                return ValidationResult.Success!;
            default:
                return ValidationResult.Success!;
        }
    }
    private bool CheckPhone(string value)
    {

        return _unitOfWork.Repository<User,Guid>().FindItemByCondition(x => x.PhoneNumber == value).Result == null;
    }
    private bool CheckUserName(string value)
    {
        return _unitOfWork.Repository<User,Guid>().FindItemByCondition(x => x.UserName == value).Result == null;
    }
}