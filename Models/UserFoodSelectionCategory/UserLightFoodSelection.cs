using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Models.UserFoodSelectionCategory
{
    public class UserLightFoodSelection
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Select an option")]
        [Display(Name = "Light Food")]
        public int UserLightFoodId { get; set; }
    }
}
