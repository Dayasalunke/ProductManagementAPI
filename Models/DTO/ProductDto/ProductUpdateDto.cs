namespace ProductManagementApi.Models.DTO.ProductDto
{
    public class ProductUpdateDto
    {
        public int productId { get; set; }
        public string? Name { get; set; } = ""; 
        public decimal Price { get; set; }
        public string Category { get; set; } = "";
        public string Color { get; set; } = "";
        public string Description { get; set; } = "";
        public string ImageLink { get; set; } = "";
        public int AvailQuantity { get; set; }
    }
}
