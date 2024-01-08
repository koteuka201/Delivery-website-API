using DeliveryAppBack.Validators;

namespace DeliveryAppBack.Models.User
{
    public class EditProfileModel
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        [AddressValidation]
        public string AdressId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
