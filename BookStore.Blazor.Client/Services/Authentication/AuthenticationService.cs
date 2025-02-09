using Blazored.LocalStorage;
using BookStore.Blazor.Client.Providers;
using BookStore.Blazor.Client.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;

namespace BookStore.Blazor.Client.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IClient httpClient;
        private readonly ILocalStorageService localStorage;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public AuthenticationService(
            IClient httpClient,
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

            // Send the login request to the backend API
            var response = await httpClient.LoginAsync(loginModel);

            Console.WriteLine(response.ToString());

            // Store Token and Email in LocalStorage
            await localStorage.SetItemAsync("accessToken", response.Token);
            await localStorage.SetItemAsync("userName", response.Email);

            // Change the authentication state of the app
            Console.WriteLine(response.Token);
            await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedIn();

            return true;
        }

        public async Task Logout()
        {
            // Logout the user and change authentication state
            await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedOut();
        }
    }
}
