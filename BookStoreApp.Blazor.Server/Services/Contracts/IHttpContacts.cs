namespace BookStoreApp.Blazor.Server.Services.Contracts
{
    public interface IHttpContacts<T> where T : class
    {
        Task<T> Get(string url, int id);
        Task<T> Get(string url, string id);
        Task<List<T>> GetAll(string url);
        Task<T> GetPagined(string url);
        Task Post(string url, T obj);
        Task<T> PostAsync(string url, T obj);
        Task Update(string url, T obj, int id);

        Task UpdateMongoDb(string url, T obj, string id);
        Task Delete(string url, int id);
        Task Delete(string url, string id);


    }
}
