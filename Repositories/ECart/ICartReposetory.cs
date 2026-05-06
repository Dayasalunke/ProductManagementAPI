using ProductManagementApi.Models.DTO.ECartDto.Request;
using ProductManagementApi.Models.DTO.ECartDto.Response;
using ProductManagementApi.Models.ECartModel;

namespace ProductManagementApi.Repositories.ECart
{
    public interface ICartReposetory
    {
        public Task<bool> AddUpdateToCartAsync(AddToCartRequest request, CancellationToken ct);
        Task<List<CartItemResponse>> GetCartItemsAsync(int userId, CancellationToken ct);
        Task<bool> RemoveFromCartAsync(int userId, int productId, CancellationToken ct);
        Task<bool> UpdateQuantityAsync(UpdateCartRequest request, CancellationToken ct);
        Task<bool> ClearCartAsync(int userId, CancellationToken ct);
    }
}
