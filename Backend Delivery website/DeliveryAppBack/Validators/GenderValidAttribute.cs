using System.ComponentModel.DataAnnotations;

namespace DeliveryAppBack.Validators
{
    public class GenderValidAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string gender && (gender.ToLower() == "male" || gender.ToLower() == "female"))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Wrong format of gender. Use male or Female!");
        }
    }
}
