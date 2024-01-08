using DeliveryAppBack.Models.User;
using DeliveryAppBack.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DeliveryAppBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly AccountsDB _accountsDB;
        public UserController(IAccountService accountService, AccountsDB accountsDB)
        {
            _accountService = accountService;
            _accountsDB = accountsDB;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserModelRegister model)
        {
            if (model == null)
            {
                return BadRequest("Invalid request");
            }
            var token = await _accountService.Register(model);
            if (token == null)
            {
                return BadRequest("Registration failed");
            }
            return Ok(new { Token = token });
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var token = await _accountService.Login(loginModel);
            if (token == null)
            {
                return BadRequest("Wrong email or password");
            }
            return Ok(new { Token = token });
        }
        [HttpPost("LogOut")]
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var validLogout = _accountsDB.LogOutTokens.FirstOrDefault(u => u.Token == token);
            if (validLogout != null){
                return BadRequest("Already unauthorize");
            }
            await _accountService.LogOut(token);
            return Ok("User unauthorize!");
        }
        [HttpGet("GetProfile")]
        [Authorize]
        public async Task<IActionResult> GetUserProfile()
        {
            string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var validLogout = _accountsDB.LogOutTokens.FirstOrDefault(u => u.Token == token);
            if (validLogout != null)
            {
                return BadRequest("User unauthorize!");
            }
            UserDTO user = await _accountService.GetUserProfile(token);//Добавить проверку на Null
            return Ok(user);

        }
        [HttpPut("EditProfile")]
        [Authorize]
        public async Task <IActionResult> EditUserProfile(EditProfileModel model)//Добавить проверку на Null
        {
            string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var validLogout = _accountsDB.LogOutTokens.FirstOrDefault(u => u.Token == token);
            if (validLogout != null)
            {
                return BadRequest("User unauthorize!");
            }
            await _accountService.EditUserProfile(model, token);

            return Ok();

        }
    }
}
