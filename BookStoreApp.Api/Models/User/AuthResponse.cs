namespace BookStoreApp.Api.Models.User
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
    }
}
