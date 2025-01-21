using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace BookStoreApp.Blazor.Server.Providers
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorage;
        private readonly Client httpClient;


        private readonly JwtSecurityTokenHandler  jwtSecurityTokenHandler;
        public ApiAuthenticationStateProvider(ILocalStorageService localStorage, Client httpClient)
        {
            this.localStorage = localStorage;
            this.httpClient = httpClient;

            jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var savedToken = await localStorage.GetItemAsync<string>("accessToken");
            var notLoggedIn = new ClaimsPrincipal(new ClaimsIdentity());

            if (string.IsNullOrWhiteSpace(savedToken))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }


            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(savedToken);

        
            var claims = tokenContent.Claims;
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

            if (tokenContent.ValidTo < DateTime.UtcNow)
            {
                return new AuthenticationState(user);
                //await localStorage.RemoveItemAsync("accessToken");
                //return new AuthenticationState(notLoggedIn);
            }

            //  httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);


            return new AuthenticationState(user);

        }
        


        public async Task LoggedIn()
        {
            var claims = await GetClaims();
        
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            var authState = Task.FromResult(new AuthenticationState(user));
            NotifyAuthenticationStateChanged(authState);
        }


        public async Task LoggedOut()
        {
           await localStorage.RemoveItemAsync("accessToken");
            var notLoggedIn = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(notLoggedIn));
            NotifyAuthenticationStateChanged(authState);
        }


       private async Task<List<Claim>> GetClaims()
        {
            var savedToken = await localStorage.GetItemAsync<string>("accessToken");
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(savedToken);
            var claims = tokenContent.Claims.ToList();
            claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
            return claims;
        }

    }
}
