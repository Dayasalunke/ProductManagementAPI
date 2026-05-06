namespace ProductManagementApi.Models.DTO.ECartDto.Response
{
    public class CartItemResponse
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public string? ImageLink { get; set; }
    }
}
