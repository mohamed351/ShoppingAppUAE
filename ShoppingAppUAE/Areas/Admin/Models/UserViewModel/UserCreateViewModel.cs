using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Areas.Admin.Models.UserViewModel
{
    public class UserCreateViewModel
    {

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]

        public string Password { get; set; }
        [Required]
        public string City { get; set; }
        [Required]

        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }
        [Required]

        public string CountryCode { get; set; }
        [Required]
        public string RoleName { get; set; }

       
    }

    public class UserEditViewModel :UserCreateViewModel
    {

        public string Id { get; set; }

    }
}
