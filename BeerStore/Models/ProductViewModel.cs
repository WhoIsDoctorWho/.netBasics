using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerStore.Models
{
    public class ProductViewModel
    {
        public IEnumerable<Beer> Beers { get; set; }
        public PaginationViewModel Pagination { get; set; }
        public FilterViewModel Filter { get; set; }
        public SortViewModel Sort { get; set; }

    }
}
