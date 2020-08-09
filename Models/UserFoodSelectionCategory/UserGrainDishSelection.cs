using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Models.UserFoodSelectionCategory
{
    public class UserGrainDishSelection
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [Required(ErrorMessage = "Select an option")]
        [Display(Name = "GrainDish")]
        public int UserGrainDishId { get; set; }
    }
}
