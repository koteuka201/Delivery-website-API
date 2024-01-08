using DeliveryAppBack.Enums;
using DeliveryAppBack.Models.Dish;
using DeliveryAppBack.Models.User;
using DeliveryAppBack.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace DeliveryAppBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;
        private readonly AccountsDB _accountsDB;
        public DishController(IDishService dishService, AccountsDB accountsDB)
        {
            _dishService = dishService;
            _accountsDB = accountsDB;
        }

        [HttpGet("DishMenu")]
        public async Task<IActionResult> GetMenu([FromQuery(Name = "category")] List<DishCategory> category, bool vegeterian, DishSorting sorting, int page)
        {
            var dishes = await _dishService.GetDishMenu(category, vegeterian, sorting, page);
            return Ok(dishes);
        }
        [HttpGet("Dish{id:guid}")]
        public async Task <IActionResult> GetDescription(Guid id)
        {
            DishDTO dish = await _dishService.GetDescription(id);
            if (dish == null)
            {
                return BadRequest();
            }
            return Ok(dish);
        }
        [HttpGet("Dish{id:guid}/rating/check")]
        [Authorize]
        public async Task <IActionResult> CheckAbletoRate(Guid id)
        {
            string token= HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var ableToRate = await _dishService.CheckAbleToRate(id, token);
            return Ok(ableToRate);
        }
        [HttpPost("Dish{id:guid}/rating")]
        [Authorize]
        public async Task <IActionResult> SetRatingToDish(Guid id, int rating)
        {
            string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            await _dishService.SetRatingToDish(id, token, (double)rating); 
            return Ok();
        }
    }
}
