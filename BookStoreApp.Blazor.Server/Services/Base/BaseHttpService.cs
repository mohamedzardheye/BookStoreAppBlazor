using Blazored.LocalStorage;
using System.Net.Http.Headers;


namespace BookStoreApp.Blazor.Server.Services.Base
{
    public class BaseHttpService
    {
        private readonly IClient client;
        private readonly ILocalStorageService localStorage;
        public BaseHttpService(IClient client, ILocalStorageService localStorage)
        {
            this.client = client;
            this.localStorage = localStorage;
        }


        protected Response<Guid> ConvertApiExceptions<Guid>(ApiException apiException)
        {
            if (apiException.StatusCode == 400)
            {
                return new Response<Guid>() { Message = "Validation errors have occured", ValidationErrors = apiException.Response, Success = false};


            }


            if (apiException.StatusCode == 404)
            {
                return new Response<Guid>() { Message = "the requested item could not be found", ValidationErrors = apiException.Response, Success = false };


            }

            if (apiException.StatusCode >= 200 && apiException.StatusCode <= 299) 
            {
                return new Response<Guid>() { Message = "Success Operation",    Success = true };

            }



                return new Response<Guid>() { Message = "An unhandled error occured", ValidationErrors = apiException.Response, Success = false };


        }




        protected async Task GetBearerToken()
        {
            var token = await localStorage.GetItemAsync<string>("accessToken");
            if(token != null)
            {
              client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
