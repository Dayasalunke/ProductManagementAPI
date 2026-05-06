using ProductManagementApi.Models.DTO.ECartDto;

namespace ProductManagementApi.Repositories.ECart
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartByUserId(int userId);
    }
}
