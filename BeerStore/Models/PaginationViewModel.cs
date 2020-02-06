using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerStore.Models
{
    public class PaginationViewModel
    {
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }
        public PaginationViewModel(int count, int pageNumber, int pageSize) 
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
        public bool HasPrevPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }
}
