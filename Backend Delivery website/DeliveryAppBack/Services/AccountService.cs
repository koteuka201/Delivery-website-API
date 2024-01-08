using DeliveryAppBack.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DeliveryAppBack.Services
{

    public class AccountService : IAccountService
    {
        private readonly AccountsDB _accountDB;
        private readonly IConfiguration _configuration;
        public AccountService(AccountsDB accountDB, IConfiguration configuration)
        {
            _accountDB = accountDB;
            _configuration = configuration;
        }
        public async Task <string> Register(UserModelRegister model)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
            if (_accountDB.Users.FirstOrDefault(user=>user.Email==model.Email) != null) 
            {
                return null;
            }
            UserDTO user = new UserDTO
            {
                Id = Guid.NewGuid(),
                FullName = model.FullName,
                Password = hashedPassword,
                Email = model.Email,
                AdressId=model.AdressId,
                BirthDate=model.BirthDate,
                Gender = model.Gender,
                PhoneNumber = model.PhoneNumber,

            };
            await _accountDB.Users.AddAsync(user);
            await _accountDB.SaveChangesAsync();
            var token = GenerateJwtToken(user);
            return token;
        }
        public async Task<string> Login(LoginModel model)
        {
            var user = _accountDB.Users.FirstOrDefault(u => u.Email == model.Email);
            if (BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                return GenerateJwtToken(user);
            }
            return null;
        }
        public async Task<UserDTO> GetUserProfile(string token)
        {
            var JWT = new JwtSecurityTokenHandler();
            var Token = JWT.ReadToken(token) as JwtSecurityToken;
            UserDTO userProfile = new UserDTO();
            var userId = Token.Claims.FirstOrDefault(claim => claim.Type == "userId").Value;
            userProfile= _accountDB.Users.FirstOrDefault(u => u.Id.ToString() == userId.ToString());
            return userProfile;

        }
        public async Task EditUserProfile(EditProfileModel model, string token)
        {
            UserDTO user = await GetUserProfile(token);
            user.FullName = model.FullName;
            user.BirthDate = model.BirthDate;
            user.Gender = model.Gender;
            user.AdressId = model.AdressId;
            user.PhoneNumber = model.PhoneNumber;

            await _accountDB.SaveChangesAsync();
        }
        public async Task LogOut(string token)
        {
            TokenModel Token = new TokenModel 
            {
                Token = token
            };

            await _accountDB.LogOutTokens.AddAsync(Token);
            await _accountDB.SaveChangesAsync();
        }
        private string GenerateJwtToken(UserDTO user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
            //new Claim(ClaimTypes.Email, user.Email)
            new Claim("userId", user.Id.ToString())
            };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
