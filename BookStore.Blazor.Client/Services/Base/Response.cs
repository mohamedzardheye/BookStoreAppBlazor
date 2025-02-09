
namespace BookStore.Blazor.Client.Services.Base
{
    public class Response<T>
    {
        public string Message { get; set; }
        public string ValidationErrors { get; set; }

        public bool Success { get; set; }

        public T data { get; set;  }

        public static implicit operator Response<T>(Response<List<AuthorReadOnlyDtoPaginatedResponseDto>> v)
        {
            throw new NotImplementedException();
        }
    }
}
