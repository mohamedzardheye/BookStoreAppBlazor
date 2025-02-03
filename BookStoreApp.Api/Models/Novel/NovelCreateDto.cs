using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.Api.Models.Novel
{
    public class NovelCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [Range(1000, int.MaxValue)]

        public int Year { get; set; }

        [Required]
        public string Isbn { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 10)]

        public string Summary { get; set; }

    

        [Required]
        [Range(0, int.MaxValue)]

        public decimal Price { get; set; }
    }
}
