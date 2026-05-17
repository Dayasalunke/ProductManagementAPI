using ProductManagementApi.Models.Request;
using ProductManagementApi.Models.Response;
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
        public async Task<ProductResponse> CreateProductAsync(ProductCreateRequest productCreateRequest, CancellationToken cancellationToken)
        {
            return await _ecomRepository.CreateProductAsync(productCreateRequest, cancellationToken);
        }
        public async Task<ProductResponse> UpdateProductAsync(ProductUpdateRequest productUpdateRequest, CancellationToken cancellationToken)
        {
            return await _ecomRepository.UpdateProductAsync(productUpdateRequest, cancellationToken);
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
            var usersRequest = new UsersRequest
            {
                Email = request.Email,
                Password = request.Password,
                UserType = request.UserType,
                Name = request.Name
            };

            var created = await _ecomRepository.AddUserAsync(usersRequest, ct);

            return new AuthResponse
            {
                Id = created.UserId,
                Email = created.Email,
                UserType = created.UserType
            };
        }

        public async Task<List<ProductResponse>> SearchProductAsync(string? search, CancellationToken ct)
        {
           return await _ecomRepository.SearchProductAsync(search, ct);
        }
    }
}
