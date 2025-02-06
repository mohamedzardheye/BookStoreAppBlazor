namespace BookStoreApp.Api.Models
{
    public class CreateUserRoleDto
    {
        public required string userId { get; set; }
        public required string Role { get; set; }
    }
}