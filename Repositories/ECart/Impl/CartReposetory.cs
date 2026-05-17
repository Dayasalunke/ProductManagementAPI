using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ProductManagementApi.Models.Database;
using ProductManagementApi.Models.Enum;
using ProductManagementApi.Models.Request;
using ProductManagementApi.Models.Response;

namespace ProductManagementApi.Repositories.ECart
{
    public class CartReposetory : ICartReposetory
    {
        private readonly AppDbContext _context;
        public CartReposetory(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddUpdateToCartAsync(AddToCartRequest addToCartRequest, CancellationToken ct)
        {
            //Check user FIRST
            var userExists = await _context.Users
                .AnyAsync(u => u.UserId == addToCartRequest.UserId && u.UserType!.ToLower() == UserRole.buyer.ToString().ToLower(), ct);

            if (!userExists)
                return false;

            //Get or create cart
            var cart = await _context.Cart
                .FirstOrDefaultAsync(c => c.UserId == addToCartRequest.UserId, ct);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = addToCartRequest.UserId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _context.Cart.AddAsync(cart, ct);
                await _context.SaveChangesAsync(ct);
            }

            //Get product
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == addToCartRequest.ProductId, ct);

            if (product == null)
                return false;

            //Check existing item
            var item = await _context.CartItems
                .FirstOrDefaultAsync(ci =>
                    ci.CartId == cart.CartId &&
                    ci.ProductId == addToCartRequest.ProductId, ct);

            if (item != null)
            {
                item.Quantity = addToCartRequest.Quantity;
            }
            else
            {
                var cartItems = new CartItems
                {
                    CartId = cart.CartId,
                    ProductId = addToCartRequest.ProductId,
                    Quantity = addToCartRequest.Quantity
                };

                await _context.CartItems.AddAsync(cartItems, ct);
            }

            cart.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<List<CartItemResponse>> GetCartItemsAsync(int userId, CancellationToken ct)
        {
            var cart = await _context.Cart
                .FirstOrDefaultAsync(c => c.UserId == userId, ct);

            if (cart == null)
            {
                return [];
            }

            var lstCarItems = await (
                                    from ci in _context.CartItems
                                    join p in _context.Products
                                    on ci.ProductId equals p.ProductId
                                    where ci.CartId == cart.CartId
                                    select new CartItemResponse
                                    {
                                        CartItemId = ci.CartItemId,
                                        ProductId = ci.ProductId,
                                        Quantity = ci.Quantity,
                                        Price = p.Price,
                                        TotalPrice = p.Price * ci.Quantity,
                                        ImageLink = p.ImageLink
                                    }
                                    ).ToListAsync(ct);

            return lstCarItems;
        }

        public async Task<bool> RemoveFromCartAsync(int userId, int productId, CancellationToken ct)
        {
            var cart = await _context.Cart
                .FirstOrDefaultAsync(c => c.UserId == userId, ct);

            if (cart == null)
                return false;

            var item = await _context.CartItems
                .FirstOrDefaultAsync(ci =>
                    ci.CartId == cart.CartId &&
                    ci.ProductId == productId, ct);

            if (item == null)
                return false;

            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync(ct);

            return true;
        }

        public async Task<bool> UpdateQuantityAsync(UpdateCartRequest request, CancellationToken ct)
        {
            var cart = await _context.Cart
                .FirstOrDefaultAsync(c => c.UserId == request.UserId, ct);

            if (cart == null)
                return false;

            var item = await _context.CartItems
                .FirstOrDefaultAsync(ci =>
                    ci.CartId == cart.CartId &&
                    ci.ProductId == request.ProductId, ct);

            if (item == null)
                return false;

            item.Quantity = request.Quantity;           

            await _context.SaveChangesAsync(ct);
            return true;
        }

        #region ClearCartAsync
        /// <summary>
        /// Clear cart and cartItems based on userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<bool> ClearCartAsync(int userId, CancellationToken ct)
        {
            // 🔍 Step 1: Find the cart for the given user
            // We assume one cart per user
            var cart = await _context.Cart
                .FirstOrDefaultAsync(c => c.UserId == userId, ct);

            // ❌ If no cart exists, nothing to delete
            if (cart == null)
                return false;

            // 🔍 Step 2: Get all cart items linked to this cart
            // These are child records (dependent on Cart)
            var items = _context.CartItems
                .Where(ci => ci.CartId == cart.CartId);

            // 🗑️ Step 3: Remove all cart items first
            // IMPORTANT: We delete child records before parent
            // to avoid foreign key constraint errors
            _context.CartItems.RemoveRange(items);

            // Save changes first
            await _context.SaveChangesAsync(ct);


            // 🗑️ Step 4: Remove the cart itself (parent record)
            // Now safe because child records are already marked for deletion
            _context.Cart.Remove(cart);

            // 💾 Step 5: Save all changes to the database
            // This will execute DELETE statements for both CartItems and Cart
            await _context.SaveChangesAsync(ct);

            // ✅ Step 6: Return success
            return true;
        }
       
        #endregion ClearCartAsync

    }
}