namespace ProductManagementApi.Models.DTO.ECartDto.Request
{
    public class AddToCartRequest
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
