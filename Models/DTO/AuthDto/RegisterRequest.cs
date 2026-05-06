using System.ComponentModel.DataAnnotations;

namespace ProductManagementApi.Models.DTO.AuthDto
{
    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? UserType { get; set; }
    }

    public class LoginRequest
    {

        [Required]
        public string? Email { get; set; }

        [Required]
        public string?  Password { get; set; }
        public string? UserType { get; set; }
    }

    public class AuthResponse
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? UserType { get; set; }
    }
}
