using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeerStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeerStore.Controllers
{
    public class ProductController : Controller
    {
        BeerStoreContext db;        
        public ProductController(BeerStoreContext context)
        {
            db = context;
        }        
        public async Task<IActionResult> Beers(int ? id, string toSearch, int page = 1, SortState sortOrder = SortState.NameAsc)
        {
            if(id != null)
                return View("Beer", db.Beers.Find(id));
            int pageSize = 4;            
            IQueryable<Beer> beers = db.Beers;
            if (!string.IsNullOrEmpty(toSearch))
                beers = beers.Where(beer => beer.Name.Contains(toSearch));            
            //ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            //ViewData["PriceSort"] = sortOrder == SortState.PriceAsc ? SortState.PriceDesc : SortState.PriceAsc;
            beers = sortOrder switch
            {
                SortState.NameDesc => beers.OrderByDescending(beer => beer.Name),
                SortState.PriceAsc => beers.OrderBy(beer => beer.Price),
                SortState.PriceDesc => beers.OrderByDescending(beer => beer.Price),
                _ => beers.OrderBy(beer => beer.Name),
            };
            var count = await beers.CountAsync();
            var pageContent = await beers.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            ProductViewModel model = new ProductViewModel
            {
                Pagination = new PaginationViewModel(count, page, pageSize),
                Filter = new FilterViewModel(toSearch),
                Sort = new SortViewModel(sortOrder),
                Beers = pageContent
            };
            return View(model);
            //return View(sortedBeers.ToList());
        }
        public IActionResult __Beers(int id)
        {
            return View("Beer", db.Beers.Find(id));
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View("New");
        }
        [HttpPost]
        public IActionResult Create(Beer beer)
        {
            db.Beers.Add(beer);
            db.SaveChanges();
            return RedirectToActionPermanent("Beers");
        }
    }
}