using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Interface
{
    public interface ISchedule
    {
        Task<List<string>> RandomizeLightFood();
        Task<List<string>> RandomizeGrainDish();
        Task<List<string>> RandomizeSwallow();
        Task<List<string>> OrderOne();
        Task<List<string>> OrderTwo();
        Task<List<string>> SplitFoodList(string user);
        Task<List<string>> ComperTime(string userName);
    }
}
