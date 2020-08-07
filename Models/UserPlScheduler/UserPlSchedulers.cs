using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Models.UserPlScheduler
{
    public class UserPlSchedulers
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FoodList { get; set; }
        public DateTime StartTime { get; set; }
        public int SoupFrequency { get; set; }
        public string SoupList { get; set; }
        public bool showSF { get; set; }
    }
}
