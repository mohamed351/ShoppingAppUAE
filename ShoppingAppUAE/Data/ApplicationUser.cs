using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingAppUAE.Data
{
    public class ApplicationUser : IdentityUser
    {
        [ForeignKey(nameof(Counties))]
        public string CountryCode { get; set; }

        public Counties Counties { get; set; }

        public string City { get; set; }

        public string Address { get; set; }
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }





    }

}
