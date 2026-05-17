using Microsoft.AspNetCore.Mvc;
using ProductManagementApi.Models.Request;
using ProductManagementApi.Models.Response;
using ProductManagementApi.Repositories.Order;

namespace ProductManagementApi.Services.Order.Impl
{
    public class OrderService : IOrderService
    {
        private readonly IOrderReposetory _repository;
        public OrderService(IOrderReposetory reposetory)
        {
            _repository = reposetory;
        }
        public async Task<PlaceOrderResponce> PlaceOrderAsync(PlaceOrderRequest PlaceOrderRequest, CancellationToken ct)
        {
            var result = await _repository.PlaceOrderAsync(PlaceOrderRequest, ct);
            return result;
        }
    }
}
