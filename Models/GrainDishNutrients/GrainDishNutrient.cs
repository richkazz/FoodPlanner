using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Models.GrainDishNutrients
{
    public class GrainDishNutrient
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Select one Food")]
        [Display(Name = "Grain Dish")]
        public int GrainName { get; set; }

        [Required(ErrorMessage = "Select an option")]
        [Display(Name = "Requires Soup")]
        public bool SoupRequired { get; set; }

        [Required(ErrorMessage = "Select one Ingredient")]
        [Display(Name = " Grain Dish Main Ingredient")]
        public int KaroMainIngredientsId { get; set; }
    }
}
