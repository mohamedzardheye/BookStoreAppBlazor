using BookStoreApp.Blazor.Server.Services.Base;

namespace BookStoreApp.Blazor.Server.Services
{
    public interface IAuthorService
    {
        Task<Response<List<AuthorReadOnlyDto>>> GetAuthors();
    }

}