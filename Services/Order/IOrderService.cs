using Microsoft.AspNetCore.Mvc;
using ProductManagementApi.Models.Request;
using ProductManagementApi.Models.Response;

namespace ProductManagementApi.Services.Order
{
    public interface IOrderService
    {
        public Task<PlaceOrderResponce> PlaceOrderAsync(PlaceOrderRequest placeOrderRequest, CancellationToken ct);

    }
}
