using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Interface
{
    public interface ISchedule
    {
        Task<List<string>> RandomizeLightFood(string userName);
        Task<List<string>> RandomizeGrainDish(string userName);
        Task<List<string>> RandomizeSwallow(string userName);
        Task<List<string>> OrderOne(string userName);
        Task<List<string>> OrderTwo(string userName);
        Task<List<string>> SplitFoodList(string user);
        Task<List<string>> ComperTime(string userName);
    }
}
