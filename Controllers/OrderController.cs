using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagementApi.Models.Request;
using ProductManagementApi.Services.Order;

namespace ProductManagementApi.Controllers
{
    [Route("apii/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;
        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [HttpPost("place-ordder")]
        public async Task<IActionResult> PlaceOrderAsync([FromBody] PlaceOrderRequest placeOrderRequest, CancellationToken ct)
        {
            var result = await _service.PlaceOrderAsync(placeOrderRequest, ct);
            return Ok(result);
        }
    }
}
