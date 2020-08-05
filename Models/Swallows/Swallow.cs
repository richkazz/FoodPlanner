using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Models.Swallows
{
    public class Swallow
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "*Enter a name for this swallow")]
        public string Name { get; set; }
    }
}
