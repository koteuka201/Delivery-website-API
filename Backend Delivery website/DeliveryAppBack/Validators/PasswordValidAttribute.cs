using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace DeliveryAppBack.Validators
{
    public class PasswordValidAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string pattern = @".*\d+.*";
            string password = value.ToString();
            if (Regex.IsMatch(password, pattern))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Password must have minimum one digit");
            }

        }
    }
}
