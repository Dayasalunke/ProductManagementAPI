using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagementApi.Models.Request;
using ProductManagementApi.Services.ECart;

namespace ProductManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ECartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public ECartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        //Add to cart
        [HttpPost("AddUpdateToCart")]
        public async Task<IActionResult> AddUpdateToCartAsync([FromBody] AddToCartRequest addToCartRequest, CancellationToken ct)
        {
            var result = await _cartService.AddUpdateToCartAsync(addToCartRequest, ct);

            if (!result)
                return BadRequest("Product or User not found");

            return Ok(new { message = "Item added to cart" });
        }

        /// <summary>
        /// Get user card details by userID.
        /// </summary>
        /// <param name="userId"> Enter User Id</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// 
        [HttpGet("GetCart")]
        public async Task<IActionResult> GetCartItemsAsync(int userId, CancellationToken ct)
        {
            if (userId <= 0)
            {
                return BadRequest("Invalid UserID");
            }

            var data = await _cartService.GetCartItemsAsync(userId, ct);

            if (data == null || data.Count == 0)
            {
                return BadRequest("UserID is Not Found");
            }

            return Ok(data);
        }

        //Remove item
        [HttpDelete("RemoveCartItem")]
        public async Task<IActionResult> RemoveFromCartAsync(int userId, int productId, CancellationToken ct)
        {
            var result = await _cartService.RemoveFromCartAsync(userId, productId, ct);

            if (!result)
                return NotFound("Item not found");

            return Ok(new { message = "Item removed" });
        }

        //Update quantity
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateQuantityAsync([FromBody] UpdateCartRequest updateCartRequest, CancellationToken ct)
        {
            var result = await _cartService.UpdateQuantityAsync(updateCartRequest, ct);

            if (!result)
                return NotFound("Item not found");

            return Ok(new { message = "Quantity updated" });
        }

        //Clear cart
        [HttpDelete("CleareCart")]
        public async Task<IActionResult> ClearCartAsync(int userId, CancellationToken ct)
        {
            var result = await _cartService.ClearCartAsync(userId, ct);

            if (!result)
                return NotFound("Cart not found");

            return Ok(new { message = "Cart cleared" });
        }
    }
}