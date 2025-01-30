using AutoMapper;
using Blazored.LocalStorage;

using BookStoreApp.Blazor.Server.Services.Base;
using BookStoreApp.Shared;
using BookStoreApp.Shared.Models;



namespace BookStoreApp.Blazor.Server.Services
{
    public class AuthorService : BaseHttpService, IAuthorService
    {
        private readonly IClient client;
        private readonly ILocalStorageService localStorage;
        private readonly IMapper mapper;

        public AuthorService(IClient client, 
            ILocalStorageService localStorage,
            IMapper mapper
            
            ) : base(client, localStorage)
        {
            this.client = client;
            this.localStorage = localStorage;
            this.mapper = mapper;
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


        public async Task<Response<List<PaginatedResponseDto<AuthorReadOnlyDto>>>> GetSearchAuthors(AuthorFilterDto authorFilterDto)
        {

            Response<List<PaginatedResponseDto<AuthorReadOnlyDto>>> response = new Response<List<PaginatedResponseDto<AuthorReadOnlyDto>>>();
            try
            {
                await GetBearerToken();
                var data = await client.SearchAsync(authorFilterDto.FirstName, authorFilterDto.LastName, authorFilterDto.Page, authorFilterDto.QuantityPerPage);


               

                //response = new Response<List<PaginatedResponseDto<AuthorReadOnlyDto>>>
                //{
                //    data = new PaginatedResponseDto<AuthorReadOnlyDto>
                //    {
                //        Data = data.Data,   // Ensure `Data` exists in your API response
                //        TotalRecords = data.TotalRecords // Ensure `TotalCount` exists in your API response
                //    },
                //    Success = true
                //};
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<List<PaginatedResponseDto<AuthorReadOnlyDto>>>(ex);
            }
            return response;
        }


        //public async Task<Response<PaginatedResponseDto<AuthorReadOnlyDto>>> GetSearchAuthors(AuthorFilterDto authorFilterDto)
        //{
        //    Response<PaginatedResponseDto<AuthorReadOnlyDto>> response = new Response<PaginatedResponseDto<AuthorReadOnlyDto>>();
        //    try
        //    {
        //        await GetBearerToken();
        //        var data = await client.SearchAsync(authorFilterDto.FirstName, authorFilterDto.LastName, authorFilterDto.Page, authorFilterDto.QuantityPerPage);

        //        response = new Response<PaginatedResponseDto<AuthorReadOnlyDto>>
        //        {
        //            data = new PaginatedResponseDto<AuthorReadOnlyDto>
        //            {
        //                Data = data.Data.ToList(),   // Ensure `Data` exists in your API response
        //                TotalRecords = data.TotalRecords // Ensure `TotalCount` exists in your API response
        //            },
        //            Success = true
        //        };
        //    }
        //    catch (ApiException ex)
        //    {
        //        return ConvertApiExceptions<PaginatedResponseDto<AuthorReadOnlyDto>>(ex);
        //    }
        //    return response;
        //}

        //public async Task<Response<List<AuthorReadOnlyDto>>> GetSearchAuthors(AuthorFilterDto authorFilterDto)
        //{
        //    Response<List<AuthorReadOnlyDto>> response = new Response<List<AuthorReadOnlyDto>>();

        //    try
        //    {
        //        await GetBearerToken();
        //        var data = await client.SearchAsync(authorFilterDto.FirstName, authorFilterDto.LastName, authorFilterDto.Page, authorFilterDto.QuantityPerPage);
        //        Console.WriteLine(data.ToString());
        //        response = new Response<List<AuthorReadOnlyDto>>
        //        {
        //            data = data,
        //            Success = true
        //        };


        //    }
        //    catch (ApiException ex)
        //    {
        //        return ConvertApiExceptions<List<AuthorReadOnlyDto>>(ex);
        //    }
        //    return response;
        //}

        public async Task<Response<int>> CreateAuthor(AuthorCreateDto author)
        {
            Response<int> response = new Response<int> { Success = true };
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

     
        public async Task<Response<AuthorReadOnlyDto>> GetAuthor(int id)
        {
            try
            {
                await GetBearerToken();
                var data = await client.AuthorsGETAsync(id);
                return new Response<AuthorReadOnlyDto>
                {
                    data = data,
                    Success = true
                };

            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<AuthorReadOnlyDto>(ex);
            }

        }

        public async Task<Response<int>> UpdateAuthor(int Id, AuthorCreateDto author)
        {
            Response<int> response = new Response<int> { Success = true };
            try
            {
                await GetBearerToken();
                await client.AuthorsPUTAsync(Id, author);
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }

            return response;
        }

     

        public async Task<Response<AuthorCreateDto>> getAuthorForUpdate(int id)
        {
            Response<AuthorCreateDto> response;
            try
            {
                await GetBearerToken();
                var data = await client.AuthorsGETAsync(id);
                response = new Response<AuthorCreateDto>
                {

                    data = mapper.Map<AuthorCreateDto>(data),



                    Success = true
                };
            }
            catch (ApiException ex) { 
                response = ConvertApiExceptions<AuthorCreateDto>(ex);

            }

            return response;
        }

        public async Task<Response<int>> DeleteAuthor(int Id)
        {
            Response<int> response = new Response<int> { Success = true };
            try
            {
                await GetBearerToken();
                await client.AuthorsDELETEAsync(Id);

            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }

            return response;
        }

       
    }
}
