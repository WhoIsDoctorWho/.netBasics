using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeerStore.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public IActionResult Beers(int ? id, DisplayParams displayParams)
        {
            if(id != null)
                return View("Beer", db.Beers.Find(id));
            ProductViewModel model = new ProductViewModel(db.Beers, displayParams);
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