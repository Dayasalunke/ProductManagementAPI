using ProductManagementApi.Models.Request;
using ProductManagementApi.Models.Response;

namespace ProductManagementApi.Repositories.Ecom
{
    public interface IEcomRepository
    {
        public Task<IReadOnlyList<ProductResponse>> GetAllProductsAsync(CancellationToken cancellationToken);
        public Task<ProductResponse?> GetProductByIdAsync(int ProductId, CancellationToken cancellationToken);
        public Task<ProductResponse> CreateProductAsync(ProductCreateRequest dto, CancellationToken cancellationToken);
        public Task<ProductResponse?> UpdateProductAsync(ProductUpdateRequest dto, CancellationToken cancellationToken);
        public Task<bool> DeleteProductAsync(int id, CancellationToken cancellationToken);
        Task<LoginResponce?> LoginAsync(LoginRequest request, CancellationToken ct);
        public Task<UsersResponse> AddUserAsync(UsersRequest usersRequest, CancellationToken ct);
        public Task<List<ProductResponse>> SearchProductAsync(string? search, CancellationToken ct);


    }
}
