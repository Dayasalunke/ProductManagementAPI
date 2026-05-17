using ProductManagementApi.Models.Request;
using ProductManagementApi.Models.Response;

namespace ProductManagementApi.Services.Ecom
{
    public interface IEcomService
    {
        public Task<IReadOnlyList<ProductResponse>> GetAllProductsAsync(CancellationToken cancellationToken);
        public Task<ProductResponse?> GetProductByIdAsync(int id, CancellationToken cancellationToken);
        public Task<ProductResponse> CreateProductAsync(ProductCreateRequest productCreateRequest, CancellationToken cancellationToken);
        public Task<ProductResponse> UpdateProductAsync(ProductUpdateRequest productUpdateRequest, CancellationToken cancellationToken);
        public Task<bool> DeleteProductAsync(int id, CancellationToken cancellationToken);
        public Task<LoginResponce> LoginAsync(LoginRequest loginRequest, CancellationToken ct);

        //public Task<LoginResponce> IsUserValidAsync(LoginResponce request, CancellationToken ct);
        public Task<AuthResponse> RegisterAsync(RegisterRequest registerRequest, CancellationToken ct);
        public Task<List<ProductResponse>> SearchProductAsync(string? search, CancellationToken ct);

    }
}
