using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.Services.Base;
using BookStoreApp.Shared.Models;

namespace BookStoreApp.Blazor.Server.Services
{
    public class AuthorService : BaseHttpService, IAuthorService
    {
        private readonly IClient client;

        public AuthorService(IClient client, ILocalStorageService localStorage) : base(client, localStorage)
        {
            this.client = client;
        }


        public async Task<Response<List<AuthorReadOnlyDto>>> GetSearchAuthors(AuthorFilterDto authorFilterDto)
        {
            Response<List<AuthorReadOnlyDto>> response = new Response<List<AuthorReadOnlyDto>>();

            try
            {
                await GetBearerToken();
                var data = await client.SearchAsync(authorFilterDto.FirstName,authorFilterDto.LastName,authorFilterDto.Page,authorFilterDto.QuantityPerPage);
                Console.WriteLine(data.ToString());
                response = new Response<List<AuthorReadOnlyDto>>
                {
                    data = data.ToList(),
                    Success = true
                };


            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<List<AuthorReadOnlyDto>>(ex);
            }
            return response;
        }

        public async Task<Response<int>> CreateAuthor(AuthorCreateDto author)
        {
            Response<int> response = new Response<int> {Success = true };
            try
            {
                await GetBearerToken();
                 await client.AuthorsPOSTAsync(author);
              
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }

            return response;
        }

        public async Task<Response<List<AuthorReadOnlyDto>>> GetAuthors()
        {
            Response<List<AuthorReadOnlyDto>> response = new Response<List<AuthorReadOnlyDto>>();

            try
            {
                await GetBearerToken();
                var data = await client.AuthorsAllAsync();
                response = new Response<List<AuthorReadOnlyDto>>
                {
                    data = data.ToList(),
                    Success = true
                };


            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<List<AuthorReadOnlyDto>>(ex);
            }
            return response;

        }

       
    }
}
