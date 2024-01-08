using DeliveryAppBack.Models.Address;
using System.ComponentModel.DataAnnotations;

namespace DeliveryAppBack.Validators
{
    public class AddressValidationAttribute: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string idString)
            {
                if (Guid.TryParse(idString, out Guid id))
                {
                    using (var dbContext = new GarContext())
                    {
                        var address = dbContext.AsAddrObjs.FirstOrDefault(Id=> Id.Objectguid==id);
                        if (address != null)
                        {
                            return ValidationResult.Success;
                        }
                    }
                }

            }
            return new ValidationResult("AddressNotFound");
        }
    }
}
