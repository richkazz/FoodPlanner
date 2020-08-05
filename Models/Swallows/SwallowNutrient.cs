using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Models.Swallows
{
    public class SwallowNutrient
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Select one Food")]
        [Display(Name = "Swallow")]
        public int SwallowName { get; set; }

        [Required(ErrorMessage = "Select one Ingredient")]
        [Display(Name = " Swallow Main Ingredient")]
        public int MainIngredientsId { get; set; }
    }
}
