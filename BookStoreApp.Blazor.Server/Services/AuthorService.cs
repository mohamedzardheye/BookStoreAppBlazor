﻿using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.Services.Base;

namespace BookStoreApp.Blazor.Server.Services
{
    public class AuthorService : BaseHttpService, IAuthorService
    {
        private readonly IClient client;

        public AuthorService(IClient client, ILocalStorageService localStorage) : base(client, localStorage)
        {
            this.client = client;
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
