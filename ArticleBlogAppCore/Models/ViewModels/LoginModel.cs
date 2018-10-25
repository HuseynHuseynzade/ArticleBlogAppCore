using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleBlogAppCore.Models.ViewModels
{
    public class LoginModel
    {
        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        [DataType(DataType.EmailAddress)]
        [Display(Name ="Please enter email address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Please enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Do you want to be remembered?")]
        
        public bool IsRemembered { get; set; }

       
    }
}
