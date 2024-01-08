using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace DeliveryAppBack.Validators
{
    public class PhoneValidAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string pattern = @"^\+7\d{3}\d{7}$";
            string phoneNumber = value.ToString();
            if (Regex.IsMatch(phoneNumber, pattern))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Wrong format of phone number. Use format +7xxxxxxxxxx.");
            }

        }
    }
}
