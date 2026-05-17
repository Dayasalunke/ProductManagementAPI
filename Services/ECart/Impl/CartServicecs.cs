using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductManagementApi.Models.Request;
using ProductManagementApi.Models.Response;
using ProductManagementApi.Repositories.ECart;
using System.Security.Cryptography.X509Certificates;
namespace ProductManagementApi.Services.ECart.Impl
{
    public class CartService : ICartService
    {
        private readonly ICartReposetory _cartRepository;

        public CartService(ICartReposetory cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public Task<bool> AddUpdateToCartAsync(AddToCartRequest addToCartRequest, CancellationToken ct)
            => _cartRepository.AddUpdateToCartAsync(addToCartRequest, ct);

        public Task<List<CartItemResponse>> GetCartItemsAsync(int userId, CancellationToken ct)
            => _cartRepository.GetCartItemsAsync(userId, ct);

        public Task<bool> RemoveFromCartAsync(int userId, int productId, CancellationToken ct)
            => _cartRepository.RemoveFromCartAsync(userId, productId, ct);

        public Task<bool> UpdateQuantityAsync(UpdateCartRequest updateCartRequest, CancellationToken ct)
            => _cartRepository.UpdateQuantityAsync(updateCartRequest, ct);

        public Task<bool> ClearCartAsync(int userId, CancellationToken ct)
            => _cartRepository.ClearCartAsync(userId, ct);

    }
}
