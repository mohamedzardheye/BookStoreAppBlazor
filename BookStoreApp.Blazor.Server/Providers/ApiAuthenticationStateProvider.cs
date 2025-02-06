using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.Services.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace BookStoreApp.Blazor.Server.Providers
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider, IAsyncDisposable
    {
        private readonly ILocalStorageService localStorage;
        private readonly Client httpClient;
        private readonly NavigationManager navManager; // Added NavigationManager to redirect user
        private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler;
        private Timer? _timer;

        public ApiAuthenticationStateProvider(
            ILocalStorageService localStorage,
            Client httpClient,
            NavigationManager navManager)
        {
            this.localStorage = localStorage;
            this.httpClient = httpClient;
            this.navManager = navManager;
            jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var savedToken = await localStorage.GetItemAsync<string>("accessToken");
            var notLoggedIn = new ClaimsPrincipal(new ClaimsIdentity());

            if (string.IsNullOrWhiteSpace(savedToken))
            {
                return new AuthenticationState(notLoggedIn);
            }

            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(savedToken);
            var claims = tokenContent.Claims;
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

            // Check if the token is expired
            if (tokenContent.ValidTo < DateTime.UtcNow)
            {
                Console.WriteLine("Token is expired: " + tokenContent.ValidTo);
                await LoggedOut();
                return new AuthenticationState(notLoggedIn); // Return an empty user state after logging out
            }

            return new AuthenticationState(user);
        }

        public async Task LoggedIn()
        {
            var claims = await GetClaims();
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public async Task LoggedOut()
        {
            // Handle the case when localStorage or navManager is null
            if (localStorage == null || navManager == null)
            {
                Console.WriteLine("Error: localStorage or navigation is null!");
                navManager.NavigateTo("/user/login", forceLoad: true);
                return;
            }

            Console.WriteLine("Logging out user...");

            // Remove the access token and other sensitive information
            await localStorage.RemoveItemAsync("accessToken");
            await localStorage.RemoveItemAsync("userName");

            var notLoggedIn = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(notLoggedIn)));

            // Redirect to the login page after logout
            navManager.NavigateTo("/user/login", forceLoad: true);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var savedToken = await localStorage.GetItemAsync<string>("accessToken");
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(savedToken);
            var claims = tokenContent.Claims.ToList();
            claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
            return claims;
        }

        public void StartChecking()
        {
            // Start a timer to periodically check token expiration
            _timer = new Timer(async _ => await CheckTokenAsync(), null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
        }

        private async Task CheckTokenAsync()
        {
            var token = await localStorage.GetItemAsync<string>("accessToken");

            if (string.IsNullOrEmpty(token))
            {
                //   await LoggedOut();

                var currentUrl = navManager.Uri;

                if (!currentUrl.Contains("/user/login"))
                {
                    navManager.NavigateTo("/user/login", forceLoad: true);

                }
                Console.WriteLine("No token found in CheckTokenAsync.");
                return;
            }

            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(token);

            // If the token has expired, log out the user
            if (tokenContent.ValidTo < DateTime.UtcNow)
            {
                Console.WriteLine("Token expired. Logging out...");
                await LoggedOut();
            }
        }

        public async ValueTask DisposeAsync()
        {
            // Dispose of the timer when no longer needed
            if (_timer != null)
            {
                await _timer.DisposeAsync();
            }
        }
    }
}
