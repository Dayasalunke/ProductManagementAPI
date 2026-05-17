using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagementApi.Models.Database;
using ProductManagementApi.Models.Request;
using ProductManagementApi.Models.Response;

namespace ProductManagementApi.Repositories.Order.Impl
{
    public class OrderReposetory : IOrderReposetory
    {
        private readonly AppDbContext _context;
        public OrderReposetory(AppDbContext context)
        {
            _context = context;
        }
        public async Task<PlaceOrderResponce> PlaceOrderAsync(PlaceOrderRequest placeOrderRequest, CancellationToken ct)
        {
            return null;

        }

    }
}
