using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleBlogAppCore.Models.ViewModels
{
    public class RegisterModel:LoginModel
    {
        
        [Required]
        [DataType(DataType.Text)]
        [Display(Name ="Please enter user name")]
        public string UserName { get; set; }

        [Required]
        [Compare("Password",ErrorMessage ="Passwords are not the same!!")]
        [DataType(DataType.Password)]
        [Display(Name = "Please confirm Password")]
        public string ConfirmPassword { get; set; }

        public static implicit operator AppUser(RegisterModel registerModel)
        {
            return new AppUser()
            {
                 
                 Email = registerModel.Email,
                 UserName = registerModel.UserName
            };
        }
    }
}
