using Microsoft.EntityFrameworkCore;
using ProductManagementApi.Models.ECartModel;
using ProductManagementApi.Models.ProductModel;
using ProductManagementApi.Models.user;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Users> Users { get; set; }
    public DbSet<Cart> Cart { get; set; }
    public DbSet<CartItem> CartItems { get; set; }

}
