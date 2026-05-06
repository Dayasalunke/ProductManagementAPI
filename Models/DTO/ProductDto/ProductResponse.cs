namespace ProductManagementApi.Models.DTO.ProductDto
{
    public class ProductResponse
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; } = ""; // Custom (P1, P2...)
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public string Category { get; set; } = "";
        public string Color { get; set; } = "";
        public string Description { get; set; } = "";
        public string ImageLink { get; set; } = "";
        public int AvailQuantity { get; set; }

    }
}
