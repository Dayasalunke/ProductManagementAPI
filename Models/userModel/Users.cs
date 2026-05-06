using System.ComponentModel.DataAnnotations;

namespace ProductManagementApi.Models.user
{
    public class Users
    {
        public string? Name {  get; set; }

        [Key]
        public int UserId { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? UserType { get; set; } // seller / buyer
    }
}
