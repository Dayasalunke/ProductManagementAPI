
using System.ComponentModel.DataAnnotations;

namespace ProductManagementApi.Models.ECartModel
{
    public class Cart
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
