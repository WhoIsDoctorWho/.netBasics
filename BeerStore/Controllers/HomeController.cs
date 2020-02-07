using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BeerStore.Models;
using System.Collections.Generic;

namespace BeerStore.Controllers
{
    public class HomeController : Controller
    {
        BeerStoreContext db;
        public HomeController(BeerStoreContext context)
        {
            db = context;
        }
        public IActionResult Index(SortState sortOrder = SortState.NameAsc)
        {
            return RedirectToAction("Beers", "Product");

        }
        [HttpPost]
        public IActionResult Buy(int[] BeerIds)
        {
            if (BeerIds == null || BeerIds.Length == 0)
                return RedirectToAction("Index");
            ViewBag.BeerIds = BeerIds;
            return View();
        }
        [HttpPost]
        public string GenerateOrder(Order order, List<int> BeerIds)
        {
            foreach(var id in BeerIds)
            {
                order.BeerIds.Add(new ShopListItem() { ItemId = id });
            }
            db.Orders.Add(order);
            db.SaveChanges();
            return "Thanks" + order.User;
        }
    }
}
