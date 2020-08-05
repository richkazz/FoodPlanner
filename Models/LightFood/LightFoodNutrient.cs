using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Models.LightFood
{
    public class LightFoodNutrient
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Select one Food")]
        [Display(Name = "Light Food")]
        public int LightFoodName { get; set; }

        [Required(ErrorMessage = "Select one Ingredient")]
        [Display(Name = " Light Food Main Ingredient")]
        public int LightFoodMainIngredient { get; set; }
    }
}
