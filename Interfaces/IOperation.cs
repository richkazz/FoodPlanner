using FoodPlanner.Models.UserPlScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Interface
{
    public interface IOperation
    {
        Task<bool> CreateFood(UserPlSchedulers model,string users);
        Task<bool> ÙpdateFood(UserPlSchedulers model);
        Task<bool> ÙpdateStartDateTime(UserPlSchedulers model);
        Task<bool> DeleteFood(int id);
        Task<List<UserPlSchedulers>> FetchFoods();
        Task<UserPlSchedulers> FetchFoodById(int id);
        Task<UserPlSchedulers> FetchFoodByUserId(string userId);
        Task<UserPlSchedulers> FetchFoodByTime(string userId);

    }
}
