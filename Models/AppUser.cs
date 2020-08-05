using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Identity.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsUserLoggedIn { get; set; }

        /*public Country Country { get; set; }

        public int Age { get; set; }

        [Required]
        public string Salary { get; set; }*/
    }
}
