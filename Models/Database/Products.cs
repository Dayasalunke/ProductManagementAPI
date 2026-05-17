using System.ComponentModel.DataAnnotations;

namespace ProductManagementApi.Models.Database
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }     // Primary Key
        public string? ProductCode { get; set; } = "";
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public string Category { get; set; } = "";
        public string Color { get; set; } = "";
        public string Description { get; set; } = "";
        public string ImageLink { get; set; } = "";
        public int AvailQuantity { get; set; }
    }
}
