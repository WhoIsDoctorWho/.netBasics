using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BeerStoreSPA.Models;
using Microsoft.AspNetCore.Http;

namespace BeerStoreSPA.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class BeerController : Controller
    {
        ApplicationContext db;
        public BeerController(ApplicationContext context)
        {
            db = context;
            if (!db.Products.Any())
            {
                db.Products.Add(new Beer { Name = "Cool Lager", Description="Laaaageeer", ImageUrl = "/", Price = 20 });
                db.Products.Add(new Beer { Name = "Carling", Description="Caaarling beer", ImageUrl = "/", Price = 20 });
                db.Products.Add(new Beer { Name = "Obolon Svitle", Description="Mmm svitle", ImageUrl = "/", Price = 20 });
                db.SaveChanges();
            }
        }
        [HttpGet]
        public IEnumerable<Beer> Get()
        {
            return db.Products.ToList();
        }

        [HttpGet("{id}")]
        public Beer Get(int id)
        {
            Beer beer = db.Products.FirstOrDefault(x => x.Id == id);
            return beer;
        }
        [HttpPost]
        public IActionResult Post(Beer beer)
        {                        
            if (ModelState.IsValid)
            {
                db.Products.Add(beer);
                db.SaveChanges();
                return Ok(beer);
            }
            return BadRequest(ModelState);
        }
        [HttpPut]
        public IActionResult Put(Beer product)
        {
            if (ModelState.IsValid)
            {
                db.Update(product);
                db.SaveChanges();
                return Ok(product);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Beer product = db.Products.FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                db.Products.Remove(product);
                db.SaveChanges();
            }
            return Ok(product);
        }
    }
    public class Body
    {
        public Beer beer { get; set; }
        public IFormFileCollection fd { get; set; }
    }
}