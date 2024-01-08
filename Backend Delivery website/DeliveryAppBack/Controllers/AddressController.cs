using DeliveryAppBack.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryAppBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }
        [HttpGet("search")]
        public async Task<IActionResult> GetAddressList(long parentObjectId, string? query)
        {
            var objects= await _addressService.GetAddress(parentObjectId, query);
            return Ok(objects);
        }
        [HttpGet("chain/{objectGuid:guid}")]
        public async Task<IActionResult> GetChainParent(Guid objectGuid)
        {
            var chain = await _addressService.GetChain(objectGuid);
            return Ok(chain);
        }
    }
}
