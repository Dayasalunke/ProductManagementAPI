using System.ComponentModel.DataAnnotations;

namespace ProductManagementApi.Models.Database
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public string? Status { get; set; }
        public string? FullName { get; set; }
        public string? MobileNumber { get; set; }
        public string? Address { get; set; }

        // Navigation Property
        public ICollection<OrderItems>? OrderItems { get; set; }
    }
}
