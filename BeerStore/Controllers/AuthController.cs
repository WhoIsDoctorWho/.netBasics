using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BeerStore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeerStore.Controllers
{
    public class AuthController : Controller
    {
        private BeerStoreContext db;
        public AuthController(BeerStoreContext context)
        {
            db = context;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if(ModelState.IsValid)
            {

                User user = await db.Users.FirstOrDefaultAsync(u => u.Login == model.Email && u.Password == model.Password);                
                if (user != null) // such user exists
                {
                    await Authenticate(model.Email);
                    return RedirectToActionPermanent("Beers", "Product");
                }                                
                ModelState.AddModelError("", "Incorrect login or password");                                 
            }
            return View(model);
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(user => user.Login == model.Email);
                if (user == null)
                {                    
                    db.Users.Add(new User { Login = model.Email, Fullname = model.Fullname,
                        AvaUrl = model.AvaUrl, Password = model.Password });
                    await db.SaveChangesAsync();

                    await Authenticate(model.Email); 

                    return RedirectToAction("Beers", "Product");
                }
                else
                    ModelState.AddModelError("", "Incorrect login and/or password");
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {            
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };            
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", 
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);            
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth");
        }

    }
}