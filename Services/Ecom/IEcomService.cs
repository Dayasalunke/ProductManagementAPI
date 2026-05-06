using ProductManagementApi.Models.DTO.AuthDto;
using ProductManagementApi.Models.DTO.LoginceResponceDto;
using ProductManagementApi.Models.DTO.ProductDto;
using ProductManagementApi.Models.ProductModel;

namespace ProductManagementApi.Services.Ecom
{
    public interface IEcomService
    {
        public Task<IReadOnlyList<ProductResponse>> GetAllProductsAsync(CancellationToken cancellationToken);
        public Task<ProductResponse?> GetProductByIdAsync(int id, CancellationToken cancellationToken);
        public Task<ProductResponse> CreateProductAsync(ProductCreateDto dto, CancellationToken cancellationToken);
        public Task<ProductResponse> UpdateProductAsync(ProductUpdateDto dto, CancellationToken cancellationToken);
        public Task<bool> DeleteProductAsync(int id, CancellationToken cancellationToken);
        public Task<LoginResponce> LoginAsync(LoginRequest request, CancellationToken ct);

        //public Task<LoginResponce> IsUserValidAsync(LoginResponce request, CancellationToken ct);
        public Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken ct);
        public Task<List<Product>> SearchProductAsync(string? search, CancellationToken ct);

    }
}
