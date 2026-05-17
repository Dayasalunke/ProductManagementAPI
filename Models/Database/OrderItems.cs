using System.ComponentModel.DataAnnotations;

namespace ProductManagementApi.Models.Database
{
    public class OrderItems
    {
        [Key]
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }

        //Navigation property   
        public Orders? Orders { get; set; }
    }
}
