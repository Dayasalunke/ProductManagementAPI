using System.ComponentModel.DataAnnotations;

namespace ProductManagementApi.Models.Database
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? UserType { get; set; } // seller / buyer
        public string? Name { get; set; }
    }
}
        