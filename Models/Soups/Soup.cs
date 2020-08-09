using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Models.Soups
{
    public class Soup
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Soup name is required")]
        public string Name { get; set; }
    }
}
