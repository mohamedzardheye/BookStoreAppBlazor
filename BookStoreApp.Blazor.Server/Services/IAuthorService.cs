using BookStoreApp.Blazor.Server.Services.Base;
using BookStoreApp.Shared;
using BookStoreApp.Shared.Models;

namespace BookStoreApp.Blazor.Server.Services
{
    public interface IAuthorService
    {
        Task<Response<List<AuthorReadOnlyDto>>> GetAuthors();

        Task<Response<AuthorReadOnlyDto>> GetAuthor(int id);
        //Task<Response<List<PaginatedResponseDto<AuthorReadOnlyDto>>>> GetSearchAuthors(AuthorFilterDto authorFilterDto);

        Task<Response<List<PaginatedResponseDto<AuthorReadOnlyDto>>>> GetSearchAuthors(AuthorFilterDto authorFilterDto);



        Task<Response<int>> CreateAuthor(AuthorCreateDto author);
        Task<Response<int>> UpdateAuthor(int Id,AuthorCreateDto author);

        Task<Response<int>> DeleteAuthor(int Id);


        Task<Response<AuthorCreateDto>> getAuthorForUpdate(int id);
    }

}