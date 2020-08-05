using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Models.ContactUs
{
    public class Contactus
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Add a Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Add Email")]
        public string Email { get; set; }
        [Display(Name ="Message")]
        [Required(ErrorMessage = "Add Massage")]
        public string Body { get; set; }
    }
}
