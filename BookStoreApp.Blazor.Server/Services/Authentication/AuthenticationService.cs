using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.Providers;
using BookStoreApp.Blazor.Server.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;

namespace BookStoreApp.Blazor.Server.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IClient httpClient;
        private readonly ILocalStorageService localStorage;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public AuthenticationService(IClient httpClient , 
                                     ILocalStorageService localStorage,
                                     AuthenticationStateProvider authenticationStateProvider)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
            this.authenticationStateProvider = authenticationStateProvider;
        }

      
        public async Task<bool> AuthenticateAsync(LoginUserDto loginModel)
        {
           Console.WriteLine("Login Service");
            var response = await httpClient.LoginAsync(loginModel);

            Console.WriteLine(response.ToString());


            // store Token in LocalStorage

            await localStorage.SetItemAsync("accessToken", response.Token);
            await localStorage.SetItemAsync("userName", response.Email);

            //Change auth state of app
            Console.WriteLine(response.Token);
            await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedIn();

            return true;


            //   authenticationStateProvider.

            // change the default request Authorization header
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.Token);
            //  return response;

        }


        public async Task Logout()
        {
           await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedOut();

        }
    }
}
