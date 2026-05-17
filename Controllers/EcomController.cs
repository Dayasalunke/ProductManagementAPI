using Microsoft.AspNetCore.Mvc;
using ProductManagementApi.Models.Request;
using ProductManagementApi.Services.Ecom;

namespace ProductManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EcomController : ControllerBase
    {
        public readonly IEcomService _ecomService;
        private readonly ILogger<EcomController> _logger;

        public EcomController(IEcomService ecomService, ILogger<EcomController> logger)
        {
            _ecomService = ecomService;
            _logger = logger;
        }

        //Get All Products
        [HttpGet("GetAllProducts")]
        public async Task<ActionResult> GetAllProducts(CancellationToken cancellationToken = default)
        {
            try
            {
                var products = await _ecomService.GetAllProductsAsync(cancellationToken);
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching products.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }

        //Get Product By Id
        [HttpGet("GetProductById")]
        public async Task<IActionResult> GetProductById(int ProductId, CancellationToken cancellationToken)
        {
            var product = await _ecomService.GetProductByIdAsync(ProductId, cancellationToken);
            if (product == null)
            {
                return NotFound($"Product with id {ProductId} Not FOund");
            }
            return Ok(product);
        }

        //Add a product 
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProductAsync([FromBody] ProductCreateRequest productCreateRequest, CancellationToken cancellationToken)
        {
            var result = await _ecomService.CreateProductAsync(productCreateRequest, cancellationToken);
            return Ok(result);
        }

        //Update Product
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProductAsync([FromBody] ProductUpdateRequest productUpdateRequest, CancellationToken cancellationToken)
        {
            var result = await _ecomService.UpdateProductAsync(productUpdateRequest, cancellationToken);
            if (result == null)
            {
                return NotFound($"product with id {productUpdateRequest.productId} not Found");
            }
            return Ok(result);
        }

        //Delete Product Byt ID 
        [HttpDelete("DeleteProductById")]
        public async Task<IActionResult> DeleteProductById(int productId, CancellationToken cancellationToken)
        {
            var delProductById = await _ecomService.DeleteProductAsync(productId, cancellationToken);
            if (delProductById == null)
                return NotFound($"Product With id {productId} is not found");
            return Ok(delProductById);

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest, CancellationToken ct)
        {
            var result = await _ecomService.RegisterAsync(registerRequest, ct);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest loginRequest, CancellationToken ct)
        {

            var result = await _ecomService.LoginAsync(loginRequest, ct);

            return Ok(result); //return full object
        }

        [HttpGet("Search")]
        public async Task<IActionResult> SearchProductAsync([FromQuery] string? search, CancellationToken ct)
        {
            var result = await _ecomService.SearchProductAsync(search, ct);
            return Ok(result);
        }

    }

}
