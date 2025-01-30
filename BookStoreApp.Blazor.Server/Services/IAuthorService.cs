using BookStoreApp.Blazor.Server.Services.Base;
using BookStoreApp.Shared.Models;

namespace BookStoreApp.Blazor.Server.Services
{
    public interface IAuthorService
    {
        Task<Response<List<AuthorReadOnlyDto>>> GetAuthors();


        Task<Response<List<AuthorReadOnlyDto>>> GetSearchAuthors(AuthorFilterDto authorFilterDto);


        Task<Response<int>> CreateAuthor(AuthorCreateDto author);
    }

}