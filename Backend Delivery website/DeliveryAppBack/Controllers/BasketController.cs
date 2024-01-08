using DeliveryAppBack.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace DeliveryAppBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }
        [HttpGet("Basket")]
        [Authorize]
        public async Task<IActionResult> GetUserBasket()
        {
            string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var basket = await _basketService.GetUserCart(token);
            return Ok(basket);
        }
        [HttpPost("dish/{dishId:guid}")]
        [Authorize]
        public async Task<IActionResult> AddDishToCart(Guid dishId)
        {
            string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            await _basketService.AddDish(token, dishId);
            return Ok();
        }
        [HttpDelete("dish/{dishId:guid}")]
        [Authorize]
        public async Task<IActionResult> DeleteDishFromCart(Guid dishId, bool increase)
        {
            string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            await _basketService.DeleteDish(token, dishId, increase);
            return Ok();
        }
    }
}
