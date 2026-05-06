using ProductManagementApi.Models.DTO.AuthDto;
using ProductManagementApi.Models.DTO.AuthDto;
using ProductManagementApi.Models.DTO.LoginceResponceDto;
using ProductManagementApi.Models.DTO.ProductDto;
using ProductManagementApi.Models.ProductModel;
using ProductManagementApi.Models.user;


namespace ProductManagementApi.Repositories.Ecom
{
    public interface IEcomRepository
    {
        public Task<IReadOnlyList<ProductResponse>> GetAllProductsAsync(CancellationToken cancellationToken);
        public Task<ProductResponse?> GetProductByIdAsync(int id, CancellationToken cancellationToken);
        public Task<ProductResponse> CreateProductAsync(ProductCreateDto dto, CancellationToken cancellationToken);
        public Task<ProductResponse?> UpdateProductAsync(ProductUpdateDto dto, CancellationToken cancellationToken);
        public Task<bool> DeleteProductAsync(int id, CancellationToken cancellationToken);
        Task<LoginResponce?> LoginAsync(LoginRequest request, CancellationToken ct);
        public Task<Users> AddUserAsync(Users user, CancellationToken ct);
        public Task<List<Product>> SearchProductAsync(string? search, CancellationToken ct);


    }
}
