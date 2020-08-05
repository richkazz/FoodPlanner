using Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Interfaces
{
    public interface IUserLoginStatus
    {
      public Task<(long, long)> GetStatus();
        public Task<bool> UpdatStaus(string id,bool isLogin);
    }
}
