using System.ComponentModel.DataAnnotations;

namespace Merchant.Application.Validations;

public class DoBDateValidation : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var parsed = DateTime.TryParse(value.ToString(), out var date);
        if (!parsed)
            return new ValidationResult("Invalid Date");
        else
        {
            //change below as per requirement
            var min = DateTime.Now.AddYears(-17); //for min 17 age
            var max = DateTime.Now.AddYears(-100); //for max 100 age
            const string msg = $"You have to be older than 17 years old to use our service";
            try
            {
                if (date > min || date < max)
                    return new ValidationResult(msg);
                else
                    return ValidationResult.Success!;
            }
            catch (Exception e)
            {
                return new ValidationResult(e.Message);
            }
        }
    }
}