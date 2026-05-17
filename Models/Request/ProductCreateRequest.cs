namespace ProductManagementApi.Models.Request
{
    public class ProductCreateRequest
    {
        public string Name { set; get;  } = "";
        public decimal Price { get; set; }
        public string Category { get; set; } = "";
        public string Color { get; set; } = "";
        public string Description { get; set; } = "";
        public string ImageLink { get; set; } = "";
        public int AvailQuantity { get; set; }
    }
}
