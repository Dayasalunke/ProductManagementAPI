using ProductManagementApi.Repositories;
using Microsoft.EntityFrameworkCore;
using ProductManagementApi.Services.Ecom;
using ProductManagementApi.Repositories.Ecom;
using ProductManagementApi.Services.Ecom.Impl;
using ProductManagementApi.Repositories.Ecom.Impl;
using ProductManagementApi.Services.ECart;
using ProductManagementApi.Repositories.ECart;
using ProductManagementApi.Services.ECart.Impl;

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