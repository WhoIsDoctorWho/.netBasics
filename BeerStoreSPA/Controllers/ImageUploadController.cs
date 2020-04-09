using Microsoft.AspNetCore.Mvc;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace BeerStoreSPA.Controllers
{
    [ApiController]
    [Route("api/upload")]
    public class ImageUploadController : Controller
    {        
        [HttpPost]
        public IActionResult Post()
        {            
            IFormFile file = Request.Form.Files[0];            
            Account account = new Account( //@todo to environment variables
                "dvqrdocnv",
                "672512687121367",
                "1IBpUj_4xjMPewvu60Xha2UkGak");
            Cloudinary cloudinary = new Cloudinary(account);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription("img", file.OpenReadStream()),
                Folder = "Beerstore",             
                Transformation = new Transformation().Width(220).Height(520).Crop("pad")                
            };                    
            ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);                              
            return Ok(new { img = uploadResult.SecureUri });
        }
    }
}