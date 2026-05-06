namespace ProductManagementApi.Models.DTO.LoginceResponceDto
{
    public class LoginResponce
    {
        public int UserId { get; set; }

        public string? UserName { get; set; }

        public bool IsValid { get; set; }
        public int CartCount { get; set; }

    }
}
