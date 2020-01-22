using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BeerStore.Models;

namespace BeerStore.Controllers
{
    public class HomeController : Controller
    {
        BeerContext db;
        public HomeController(BeerContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View(db.Beers.ToList());
        }
        [HttpGet]
        public IActionResult Buy(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");
            ViewBag.BeerId = id;
            return View();
        }
        [HttpPost]
        public string GenerateOrder(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return "Thanks" + order.User;
        }
    }
}
