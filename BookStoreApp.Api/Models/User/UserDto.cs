using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.Api.Models.User
{
    public class UserDto
    {
        [Required, MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
