using DeliveryAppBack.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryAppBack.Services
{
    public interface IAccountService
    {
        public Task<string> Register(UserModelRegister model);
        //Task<string> Register(UserModelRegister model);
        public Task<string> Login(LoginModel model);
        public Task<UserDTO> GetUserProfile(string token);//userDto type
        public Task EditUserProfile(EditProfileModel model, string token);
        public Task LogOut(string token);
    }
}
