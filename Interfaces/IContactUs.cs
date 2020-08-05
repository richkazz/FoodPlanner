using FoodPlanner.Models.ContactUs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Interfaces
{
   public interface IContactUs
    {
        Task<List<Contactus>> FetchFoodByUserId();
    }
}
