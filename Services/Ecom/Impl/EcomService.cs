using ProductManagementApi.Models.DTO.AuthDto;
using ProductManagementApi.Models.DTO.LoginceResponceDto;
using ProductManagementApi.Models.DTO.ProductDto;
using ProductManagementApi.Models.ProductModel;
using ProductManagementApi.Models.user;
using ProductManagementApi.Repositories.Ecom;

namespace ProductManagementApi.Services.Ecom.Impl
{
    public class EcomService : IEcomService
    {
        //inherits IEcomService But Var created a IEcomRepository 
        //To call a IEcomRepository methods so create var IEcomRepo in this service
        public readonly IEcomRepository _ecomRepository;
        public EcomService(IEcomRepository ecomRepository)
        {
            _ecomRepository = ecomRepository;
        }

        public async Task<IReadOnlyList<ProductResponse>> GetAllProductsAsync(CancellationToken cancellationToken)
        {
            return await _ecomRepository.GetAllProductsAsync(cancellationToken);
        }
        public async Task<ProductResponse?> GetProductByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _ecomRepository.GetProductByIdAsync(id, cancellationToken);
        }
        public async Task<ProductResponse> CreateProductAsync(ProductCreateDto dto, CancellationToken cancellationToken)
        {
            return await _ecomRepository.CreateProductAsync(dto, cancellationToken);
        }
        public async Task<ProductResponse?> UpdateProductAsync(ProductUpdateDto dto, CancellationToken cancellationToken)
        {
            return await _ecomRepository.UpdateProductAsync(dto, cancellationToken);
        }
        public async  Task<bool> DeleteProductAsync(int id, CancellationToken cancellationToken)
        {
            return await _ecomRepository.DeleteProductAsync(id, cancellationToken);

        }

        public async Task<LoginResponce> LoginAsync(LoginRequest request, CancellationToken ct)
        {
            return await _ecomRepository.LoginAsync(request,ct);
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken ct)
        {
            var user = new Users
            {
                Email = request.Email,
                Password = request.Password,
                UserType = request.UserType,
                Name = request.Name
            };

            var created = await _ecomRepository.AddUserAsync(user, ct);

            return new AuthResponse
            {
                Id = created.UserId,
                Email = created.Email,
                UserType = created.UserType
            };
        }

        public async Task<List<Product>> SearchProductAsync(string? search, CancellationToken ct)
        {
           return await _ecomRepository.SearchProductAsync(search, ct);
        }
    }
}
