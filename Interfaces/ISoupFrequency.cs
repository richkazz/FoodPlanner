using FoodPlanner.Models;
using FoodPlanner.Models.UserPlScheduler;
using Identity.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Interfaces
{
   public interface ISoupFrequency
    {
        Task<List<int>> FetchFoodByUserId(string userName);
        Task<List<SoupFrequency>> GetSelectFrequency();
        
        Task<bool> PostCreateSoupList(UserPlSchedulers model);
        Task<bool> ÙpdateSoupList(UserPlSchedulers model);
        Task<bool> ÙpdateSoupFrequency(UserPlSchedulers model);
        Task<List<string>> RandomizeSoup(string userName);
        
        Task<List<string>> OrderOne(string userName);
        Task<List<string>> OrderTwo();
        Task<List<string>> SplitFoodList(string userName);
        Task<List<string>> StartSoupFrequencyProcess(string userName,int item);
    }
}
