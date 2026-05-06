namespace ProductManagementApi.Models.DTO.ECartDto.Request
{
    public class UpdateCartRequest
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
