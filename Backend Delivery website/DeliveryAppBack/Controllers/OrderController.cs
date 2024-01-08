using DeliveryAppBack.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryAppBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost("CreateOrder")]
        [Authorize]
        public async Task<IActionResult> CreateUserOrder()
        {
            string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            await _orderService.CreateOrder(token);
            return Ok();
        }
        [HttpPost("{id:guid}/status")]
        [Authorize]
        public async Task<IActionResult> ConfirmUserOrder(Guid id)
        {
            string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            await _orderService.ConfirmOrder(token, id);
            return Ok();
        }
        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetInfoAboutOrder(Guid id)
        {
            string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var order=await _orderService.GetInfoOrder(token, id);
            return Ok(order);
        }
        [HttpGet("GetOrders")]
        [Authorize]
        public async Task<IActionResult> GetListOfOrders()
        {
            string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var orders=await _orderService.GetListOfOrders(token);
            return Ok(orders);
        }
    }
}
