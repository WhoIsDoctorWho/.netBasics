using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeerStore.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Incorrect email address")]
        public string Email { get; set; }
        [Required]
        public string Fullname { get; set; }        
        public string AvaUrl { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm your password")]
        [Compare("Password", ErrorMessage = "Passwords are different")]
        public string ConfirmPassword { get; set; }
    }
}
