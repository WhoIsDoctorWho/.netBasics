using System.Collections.Generic;
using System.Linq;
using BeerStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeerStore.Controllers
{
    public class ProductController : Controller
    {
        BeerStoreContext db;        
        public ProductController(BeerStoreContext context)
        {
            db = context;
        }        
        public IActionResult Beers(int ? id, SortState sortOrder = SortState.NameAsc)
        {
            if(id != null)
                return View("Beer", db.Beers.Find(id));
            List<Beer> beers = db.Beers.ToList();
            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["PriceSort"] = sortOrder == SortState.PriceAsc ? SortState.PriceDesc : SortState.PriceAsc;
            var sortedBeers = sortOrder switch
            {
                SortState.NameDesc => beers.OrderByDescending(beer => beer.Name),
                SortState.PriceAsc => beers.OrderBy(beer => beer.Price),
                SortState.PriceDesc => beers.OrderByDescending(beer => beer.Price),
                _ => beers.OrderBy(beer => beer.Name),
            };
            return View(sortedBeers.ToList());
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