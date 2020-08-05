using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Models.MainIngredients
{
    public class MainIngredient
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "*Enter the main ingredient")]
        public string Name { get; set; }

        [Display(Name = "Class of Food")]
        public string ClassOfFood { get; set; }
    }
}
