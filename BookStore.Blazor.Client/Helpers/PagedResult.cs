using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Blazor.Client.Helpers
{
    public class PagedResult<T>
    {
        public List<T> Data { get; set; }
        public int totalRecords { get; set; }
        public int currentPage { get; set; }
        public int quantityPerPage { get; set; }
    }
}
