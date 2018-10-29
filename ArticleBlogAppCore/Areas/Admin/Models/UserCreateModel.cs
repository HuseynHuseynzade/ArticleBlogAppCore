using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleBlogAppCore.Areas.Admin.Models
{
    public class UserRole
    {
        public string Id { get; set; }
        public string RoleName { get; set; }

    }
    public class UserCreateModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        public string RoleName { get; set; }

        public string RoleId { get; set; }

        public IEnumerable<UserRole> Roles { get; set; }
    }
}
