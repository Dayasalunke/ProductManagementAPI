using Microsoft.EntityFrameworkCore;
using ProductManagementApi.Models.Database;
using ProductManagementApi.Models.Request;
using ProductManagementApi.Models.Response;

namespace ProductManagementApi.Repositories.Ecom.Impl
{
    public class EcomRepository : IEcomRepository
    {

        private readonly AppDbContext _context;
        public EcomRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<ProductResponse>> GetAllProductsAsync(CancellationToken cancellationToken)
        {

            var data = await _context.Products
                       .AsNoTracking()
                       .Select(p => new ProductResponse
                       {
                           ProductId = p.ProductId,
                           ProductCode = p.ProductCode,
                           Name = string.IsNullOrWhiteSpace(p.Name) ? "ProductName" : p.Name,
                           Price = p.Price,
                           Category = p.Category,
                           Color = p.Color,
                           Description = p.Description,
                           ImageLink = p.ImageLink,
                           AvailQuantity = p.AvailQuantity
                       })
                       .ToListAsync(cancellationToken);
            return data;
        }

        public async Task<ProductResponse?> GetProductByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Products
                  .AsNoTracking()
                  .Select(p => new ProductResponse
                  {
                      ProductId = p.ProductId,
                      ProductCode = "P" + p.ProductId,
                      Name = p.Name,
                      Price = p.Price,
                      Category = p.Category,
                      Color = p.Color,
                      Description = p.Description,
                      ImageLink = p.ImageLink,
                      AvailQuantity = p.AvailQuantity
                  })
                  .FirstOrDefaultAsync(p => p.ProductId == id, cancellationToken);
        }
        public async Task<ProductResponse> CreateProductAsync(ProductCreateRequest productCreateRequest, CancellationToken cancellationToken)
        {
            Products products = new Products
            {
                Name = productCreateRequest.Name,
                ProductCode = GenerateProductCode(productCreateRequest.Name, productCreateRequest.Category),
                Price = productCreateRequest.Price,
                Category = productCreateRequest.Category,
                Color = productCreateRequest.Color,
                Description = productCreateRequest.Description,
                ImageLink = productCreateRequest.ImageLink,
                AvailQuantity = productCreateRequest.AvailQuantity
            };

            //ORRRRRRRR OLD IMPLIMENTATION
            //Products products = new Products();
            //products.Name = productCreateRequest.Name;
            //products.ProductCode = GenerateProductCode(productCreateRequest.Name, productCreateRequest.Category);
            //products.Price = productCreateRequest.Price;
            //products.Category = productCreateRequest.Category;
            //products.Color = productCreateRequest.Color;
            //products.Description = productCreateRequest.Description;
            //products.ImageLink = productCreateRequest.ImageLink;
            //products.AvailQuantity = productCreateRequest.AvailQuantity;
            //ORRRRRRRR IMPLIMENTATION

            _context.Products.Add(products);
            await _context.SaveChangesAsync();

            ProductResponse productResponse = new();

            productResponse.ProductId = products.ProductId;
            productResponse.ProductCode = products.ProductCode;
            productResponse.Name = products.Name;
            productResponse.Price = products.Price;
            productResponse.Category = products.Category;
            productResponse.Color = products.Color;
            productResponse.Description = products.Description;
            productResponse.ImageLink = products.ImageLink;
            productResponse.AvailQuantity = products.AvailQuantity;
           
            return productResponse;
        }
        private string GenerateProductCode(string name, string category)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(category))
                throw new ArgumentException("Name and Category are required");

            string cleanName = new string(name.Where(char.IsLetterOrDigit).ToArray()).ToUpper();
            string cleanCategory = new string(category.Where(char.IsLetterOrDigit).ToArray()).ToUpper();

            string namePart = cleanName.Length >= 2 ? cleanName[..2] : cleanName.PadRight(2, 'X');
            string categoryPart = cleanCategory.Length >= 2 ? cleanCategory[..2] : cleanCategory.PadRight(2, 'X');

            // 6-char unique part (safe + compact)
            string uniquePart = Guid.NewGuid()
                .ToString("N")[..6]
                .ToUpper();

            return $"{namePart}{categoryPart}{uniquePart}";
        }
        public async Task<ProductResponse?> UpdateProductAsync(ProductUpdateRequest productUpdateRequest, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == productUpdateRequest.productId, cancellationToken);
            if (product == null)
                return null;

            //update fields
            product.Name = productUpdateRequest.Name!;
            product.Price = productUpdateRequest.Price;
            product.Category = productUpdateRequest.Category;
            product.Color = productUpdateRequest.Color;
            product.Description = productUpdateRequest.Description;
            product.ImageLink = productUpdateRequest.ImageLink;
            product.AvailQuantity = productUpdateRequest.AvailQuantity;

            await _context.SaveChangesAsync(cancellationToken);

            ProductResponse productResponse = new()
            {
                ProductId = product.ProductId,
                ProductCode = product.ProductCode!,
                Name = product.Name,
                Price = product.Price,
                Category = product.Category,
                Color = product.Color,
                Description = product.Description,
                ImageLink = product.ImageLink,
                AvailQuantity = product.AvailQuantity
            };

            return productResponse;
        }
        public async Task<bool> DeleteProductAsync(int id, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == id, cancellationToken);

            if (product == null)
                return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);
            return true;

        }
        public async Task<LoginResponce?> LoginAsync(LoginRequest request, CancellationToken ct)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u =>
                    u.Email == request.Email &&
                    u.Password == request.Password &&
                    u.UserType == request.UserType, ct);

            //INVALID user
            if (user == null)
            {
                return new LoginResponce
                {
                    UserId = 0,
                    UserName = null,
                    IsValid = false,
                };
            }
            int cartItemCount = await GetCartItemCountAsync(user.UserId, ct);
            //VALID LOGIN
            return new LoginResponce
            {
                UserId = user.UserId,
                UserName = user.Name,
                IsValid = true,
                CartCount = cartItemCount
            };
        }
        public async Task<UsersResponse> AddUserAsync(UsersRequest usersRequest, CancellationToken ct)
        {
            Users users = new Users
            {
                Name = usersRequest.Name,
                Email = usersRequest.Email,
                Password = usersRequest.Password,
                UserType = usersRequest.UserType
            };
            _context.Users.Add(users);
            await _context.SaveChangesAsync(ct);
            UsersResponse usersResponse = new UsersResponse
            {
                UserId = users.UserId,
                Name = users.Name,
                Email = users.Email,
                Password = users.Password,
                UserType = users.UserType
            };
            return usersResponse;
        }
        public async Task<List<ProductResponse>> SearchProductAsync(string? search, CancellationToken ct)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();

                query = query.Where(p =>
                EF.Functions.Like(p.Name, $"%{search}%") ||
                EF.Functions.Like(p.Category, $"%{search}%") ||
                EF.Functions.Like(p.Description, $"%{search}%")
                );
            }
            var products = await query
                .OrderByDescending(p => p.ProductId)
                .ToListAsync(ct);

            var productResponses = products.Select(p => new ProductResponse
            {
                ProductId = p.ProductId,
                ProductCode = p.ProductCode!,
                Name = p.Name,
                Price = p.Price,
                Category = p.Category,
                Color = p.Color,
                Description = p.Description,
                ImageLink = p.ImageLink,
                AvailQuantity = p.AvailQuantity
            }).ToList();

            return productResponses;
        }

        // ================= PRIVATE METHOD =================
        // Get total cart item count for a user
        private async Task<int> GetCartItemCountAsync(int userId, CancellationToken ct)
        {
            // Step 1: Find cart for the user
            var cart = await _context.Cart
                .FirstOrDefaultAsync(c => c.UserId == userId, ct);

            // Step 2: If no cart → count = 0
            if (cart == null)
                return 0;

            // Step 3: Count items in CartItems table
            var count = await _context.CartItems
                .Where(ci => ci.CartId == cart.CartId)
                .CountAsync(ct);

            return count;
        }
    }
}





