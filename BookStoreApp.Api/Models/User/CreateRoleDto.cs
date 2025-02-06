using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.Api.Models
{
    public class CreateRoleDto
    {

        [Required]
        [StringLength(256, MinimumLength = 3, ErrorMessage = "Role name must be between 3 and 256 characters.")]
        public string Name { get; set; }

        public string NormalizedName => Name.ToUpper();

    }
}