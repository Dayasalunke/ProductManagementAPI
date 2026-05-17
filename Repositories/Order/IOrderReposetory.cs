using Microsoft.AspNetCore.Mvc;
using ProductManagementApi.Models.Request;
using ProductManagementApi.Models.Response;

namespace ProductManagementApi.Repositories.Order
{
    public interface IOrderReposetory
    {
        public Task<PlaceOrderResponce> PlaceOrderAsync(PlaceOrderRequest placeOrderRequest, CancellationToken ct);
    }
}
