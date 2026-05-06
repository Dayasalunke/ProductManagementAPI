using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductManagementApi.Models.DTO.ECartDto.Request;
using ProductManagementApi.Models.DTO.ECartDto.Response;
using ProductManagementApi.Models.ECartModel;
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

        public Task<bool> AddUpdateToCartAsync(AddToCartRequest request, CancellationToken ct)
            => _cartRepository.AddUpdateToCartAsync(request, ct);

        public Task<List<CartItemResponse>> GetCartItemsAsync(int userId, CancellationToken ct)
            => _cartRepository.GetCartItemsAsync(userId, ct);

        public Task<bool> RemoveFromCartAsync(int userId, int productId, CancellationToken ct)
            => _cartRepository.RemoveFromCartAsync(userId, productId, ct);

        public Task<bool> UpdateQuantityAsync(UpdateCartRequest request, CancellationToken ct)
            => _cartRepository.UpdateQuantityAsync(request, ct);

        public Task<bool> ClearCartAsync(int userId, CancellationToken ct)
            => _cartRepository.ClearCartAsync(userId, ct);

    }
}
