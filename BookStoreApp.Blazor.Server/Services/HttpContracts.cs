using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.Services.Contracts;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace BookStoreApp.Blazor.Server.Services
{
    public class HttpContracts<T> : IDisposable, IHttpContacts<T> where T : class
    {
        private readonly HttpClient client;
        private readonly HttpInerceptorService interceptor;
        private readonly ILocalStorageService localStorage;

        public HttpContracts(HttpClient client, HttpInerceptorService interceptor, ILocalStorageService localStorage)
        {
            this.client = client;
            this.interceptor = interceptor;
            this.localStorage = localStorage;
        }
        protected async Task GetBearerToken()
        {
            var token = await localStorage.GetItemAsync<string>("accessToken");
            if (token != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
        }
        public async Task Post(string url, T obj)
        {
            await GetBearerToken();
            interceptor.MonitorEvent();
            //interceptor.RegisterEvent();
            await client.PostAsJsonAsync(url, obj);
        }
        public async Task<T> PostAsync(string url, T obj)
        {
            await GetBearerToken();
            interceptor.MonitorEvent();
            //interceptor.RegisterEvent();
            var response = await client.PostAsJsonAsync<T>(url, obj);
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task Delete(string url, int id)
        {
            await GetBearerToken();
            interceptor.MonitorEvent();
            //interceptor.RegisterEvent();
            await client.DeleteAsync($"{url}{id}");
        }

        public async Task Delete(string url, string id)
        {
            await GetBearerToken();
            interceptor.MonitorEvent();
            //interceptor.RegisterEvent();
            await client.DeleteAsync($"{url}{id}");
        }

        public void Dispose()
        {
            interceptor.DisposeEvent();
        }

        public async Task<T> Get(string url, int id)
        {
            await GetBearerToken();
            interceptor.MonitorEvent();
            //interceptor.RegisterEvent();

            return await client.GetFromJsonAsync<T>($"{url}{id}");
        }

        public async Task<List<T>> GetAll(string url)
        {
            await GetBearerToken();
            interceptor.MonitorEvent();
            // interceptor.RegisterEvent();
            return await client.GetFromJsonAsync<List<T>>($"{url}");
        }
        public async Task<T> GetPagined(string url)
        {
            await GetBearerToken();
            interceptor.MonitorEvent();
            //interceptor.RegisterEvent();
            return await client.GetFromJsonAsync<T>($"{url}");
        }

        public async Task Update(string url, T obj, int id)
        {
            await GetBearerToken();
            interceptor.MonitorEvent();
            //interceptor.RegisterEvent();
            await client.PutAsJsonAsync($"{url}{id}", obj);
        }

        public async Task UpdateMongoDb(string url, T obj, string id)
        {
            await GetBearerToken();
            interceptor.MonitorEvent();
            //interceptor.RegisterEvent();
            await client.PutAsJsonAsync($"{url}{id}", obj);
        }

        public async Task<T> Get(string url, string id)
        {
            await GetBearerToken();
            interceptor.MonitorEvent();
            //interceptor.RegisterEvent();
            return await client.GetFromJsonAsync<T>($"{url}{id}");
        }

        //public async Task<T> GetAll(string url, QueryParameters query)
        //{
        //    await GetBearerToken();
        //    interceptor.MonitorEvent();
        //    return await client.GetFromJsonAsync<T>($"{url}?startIndex={query.StartIndex}&pageSize={query.PageSize}");
        //}
    }
}
