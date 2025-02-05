
namespace BookStoreApp.Blazor.Server.Static
{
    public static class EndPoints
    {
        public static string Prefix = "https://localhost:7039/api";

        public static string AuthorsEndPoint = $"{Prefix}/Authors/";
        public static string NovelsEndPoint = $"{Prefix}/Novels/";

    }
}