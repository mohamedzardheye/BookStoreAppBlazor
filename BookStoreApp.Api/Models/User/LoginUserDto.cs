using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.Api.Models.User
{
    public class LoginUserDto
    {
        [Required, MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
