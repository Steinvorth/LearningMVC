using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "This field is needed!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is needed!")]
        public string Address { get; set; }

        [Required(ErrorMessage = "This field is needed!")]
        public string City { get; set; }

        [Required(ErrorMessage = "This field is needed!")]
        public string Country { get; set; }
    }
}
