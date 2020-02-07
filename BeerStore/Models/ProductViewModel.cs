using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BeerStore.Models
{
    public class ProductViewModel
    {
        public ProductViewModel(DbSet<Beer> beers, DisplayParams displayParams)
        {
            Beers = beers;
            Filter(displayParams.ToSearch);
            Sort(displayParams.SortOrder);
            Pagination(displayParams.Page);                                             
        }

        public IEnumerable<Beer> Beers { get; set; }
        public PaginationViewModel PaginationModel { get; set; }
        public FilterViewModel FilterModel { get; set; }
        public SortViewModel SortModel { get; set; }
        private void Filter(string toSearch) 
        {
            if (!string.IsNullOrEmpty(toSearch))
                Beers = Beers.Where(beer => beer.Name.Contains(toSearch));
            FilterModel = new FilterViewModel(toSearch);
        }
        private void Sort(SortState sortOrder)
        {
            Beers = sortOrder switch
            {
                SortState.NameDesc => Beers.OrderByDescending(beer => beer.Name),
                SortState.PriceAsc => Beers.OrderBy(beer => beer.Price),
                SortState.PriceDesc => Beers.OrderByDescending(beer => beer.Price),
                _ => Beers.OrderBy(beer => beer.Name),
            };
            SortModel = new SortViewModel(sortOrder);
        }
        private void Pagination(int page)
        {
            const int pageSize = 4;
            int count = Beers.Count();
            Beers = Beers.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PaginationModel = new PaginationViewModel(count, page, pageSize);                
        }

    }
}
