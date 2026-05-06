namespace ProductManagementApi.Repositories.ECart.IMpl
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Cart?> GetCartByUserId(int userId)
        {
           return await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }


    }
}
