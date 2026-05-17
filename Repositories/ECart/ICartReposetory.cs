using ProductManagementApi.Models.Request;
using ProductManagementApi.Models.Response;

namespace ProductManagementApi.Repositories.ECart
{
    public interface ICartReposetory
    {
        public Task<bool> AddUpdateToCartAsync(AddToCartRequest addToCartRequest, CancellationToken ct);
        Task<List<CartItemResponse>> GetCartItemsAsync(int userId, CancellationToken ct);
        Task<bool> RemoveFromCartAsync(int userId, int productId, CancellationToken ct);
        Task<bool> UpdateQuantityAsync(UpdateCartRequest updateCartRequest, CancellationToken ct);
        Task<bool> ClearCartAsync(int userId, CancellationToken ct);
    }
}
