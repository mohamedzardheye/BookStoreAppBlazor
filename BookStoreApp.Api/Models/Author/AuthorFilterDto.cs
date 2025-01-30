namespace BookStoreApp.Api.Models
{
    public class AuthorFilterDto : PaginationDTO
    {

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
