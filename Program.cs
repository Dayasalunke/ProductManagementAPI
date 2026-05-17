using Microsoft.EntityFrameworkCore;
using ProductManagementApi.Repositories;
using ProductManagementApi.Repositories.ECart;
using ProductManagementApi.Repositories.Ecom;
using ProductManagementApi.Repositories.Ecom.Impl;
using ProductManagementApi.Repositories.Order;
using ProductManagementApi.Repositories.Order.Impl;
using ProductManagementApi.Services.ECart;
using ProductManagementApi.Services.ECart.Impl;
using ProductManagementApi.Services.Ecom;
using ProductManagementApi.Services.Ecom.Impl;
using ProductManagementApi.Services.Order;
using ProductManagementApi.Services.Order.Impl;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔹 Dependency Injection
builder.Services.AddScoped<IEcomService, EcomService>();
builder.Services.AddScoped<IEcomRepository, EcomRepository>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICartReposetory, CartReposetory>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderReposetory, OrderReposetory>();
// 🔹 Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// 🔹 CORS (Angular)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Angular URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// 🔥 Middleware Order (VERY IMPORTANT)

app.UseHttpsRedirection();

app.UseCors("AllowAngular"); // ✅ MUST be before MapControllers

app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();