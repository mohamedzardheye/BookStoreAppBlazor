using System.Collections.Generic;

namespace BookStoreApp.Shared
{
    public class PaginatedResponseDto<T>
    {
        public int TotalRecords { get; set; } // Total records matching the query
        public List<T> Data { get; set; } = new List<T>();


        // Optionally, you can add pagination details like current page, page size, etc.
        public int CurrentPage { get; set; }
        public int QuantityPerPage { get; set; }
    }
}
