namespace BookStoreApp.Blazor.Server.Services.Base
{
    public class Response<T>
    {
        public string Message { get; set; }
        public string ValidationErrors { get; set; }

        public bool Success { get; set; }

        public T data { get; set;  }
    }
}
