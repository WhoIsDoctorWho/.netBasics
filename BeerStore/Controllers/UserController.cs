using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BeerStore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeerStore.Controllers
{
    public class UserController : Controller
    {
        BeerStoreContext db;
        IWebHostEnvironment env;
        public UserController(BeerStoreContext context, IWebHostEnvironment appEnvironment) 
        {
            db = context;
            env = appEnvironment;
        }
        public IActionResult Users(int ? id)
        {
            var x = db.Users.Find(id);
            return id == null ? View(db.Users.ToList()) : View("User", db.Users.Find(id));
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(User user, IFormFile avatar)
        {
            string localPath = "/img/user/";            
            if(avatar != null)
            {
                localPath += avatar.FileName;
                string globalPath = env.WebRootPath + localPath;
                using(FileStream fs = new FileStream(globalPath, FileMode.Create))
                {
                    await avatar.CopyToAsync(fs);
                }
            } else            
                localPath += "user.png";
            user.AvaUrl = localPath;
            db.Users.Add(user);
            db.SaveChanges();
            return RedirectToAction("Users");
        }
    }
}