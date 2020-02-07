using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerStore.Models
{
    public class FilterViewModel
    {
        public string ToSearch { get; private set; }
        public FilterViewModel(string toSearch)
        {
            ToSearch = toSearch;
        }

    }
}
