using DeliveryAppBack.Enums;
using DeliveryAppBack.Validators;
using System.ComponentModel.DataAnnotations;

namespace DeliveryAppBack.Models.User
{
    public class UserModelRegister
    {
        public string FullName { get; set; }
        [PasswordValid]
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [AddressValidation]
        public string AdressId { get; set; }
        public DateTime BirthDate { get; set; }
        [GenderValid]
        public string Gender { get; set; }
        [PhoneValid]
        public string PhoneNumber { get; set; }
    }
}
