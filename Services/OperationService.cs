using FoodPlanner.Interface;
using FoodPlanner.Models.UserPlScheduler;
using Identity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Services
{
    public class OperationService : IOperation
    {
        private readonly AppIdentityDbContext _context;


        public OperationService(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateFood(UserPlSchedulers model)
        {
            bool msg = false;

            var checkExist = await _context.UserPlScheduler.FirstOrDefaultAsync(x => x.UserId == model.UserId);
            if (checkExist == null)
            {
                var result = new UserPlSchedulers
                {
                    FoodList = model.FoodList,
                    UserId = model.UserId,
                    StartTime = model.StartTime
                };
                await _context.UserPlScheduler.AddAsync(result);
                if (await _context.SaveChangesAsync() > 0)
                {
                    msg = true;
                }
            }

            return msg;
        }

        public async Task<bool> DeleteFood(int id)
        {
            var model = await _context.UserPlScheduler.FirstOrDefaultAsync(x => x.Id == id);
            if (model != null)
            {
                _context.UserPlScheduler.Remove(model);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<UserPlSchedulers> FetchFoodById(int id)
        {
            var model = new UserPlSchedulers();
            model = await _context.UserPlScheduler.FirstOrDefaultAsync(x => x.Id == id);
            if (model != null)
            {
                var result = new UserPlSchedulers
                {
                    FoodList = model.FoodList,
                    UserId = model.UserId,
                    Id = model.Id
                };
                return result;
            }
            return model;
        }

        public async Task<UserPlSchedulers> FetchFoodByUserId(string userId)
        {
            var model = new UserPlSchedulers();
            model = await _context.UserPlScheduler.FirstOrDefaultAsync(x => x.UserId == userId);
            if (model != null)
            {
                var result = new UserPlSchedulers
                {
                    FoodList = model.FoodList,
                    UserId = model.UserId,
                    Id = model.Id
                };
                return result;
            }
            return model;
        }
        public async Task<UserPlSchedulers> FetchFoodByTime(string userId)
        {
            var model = new UserPlSchedulers();
            model = await _context.UserPlScheduler.FirstOrDefaultAsync(x => x.UserId == userId);
            if (model != null)
            {
                var result = new UserPlSchedulers
                {
                    FoodList = model.FoodList,
                    UserId = model.UserId,
                    Id = model.Id,
                    StartTime = model.StartTime
                };
                return result;
            }
            return model;
        }


        public async Task<List<UserPlSchedulers>> FetchFoods()
        {
            var model = _context.UserPlScheduler

                .Select(model => new UserPlSchedulers
                {
                    FoodList = model.FoodList,
                    UserId = model.UserId,
                    Id = model.Id,
                    SoupList=model.SoupList,
                    SoupFrequency=model.SoupFrequency
                }).ToList();
            return model;
        }

        public async Task<bool> ÙpdateFood(UserPlSchedulers model)
        {
            var checkExist = await _context.UserPlScheduler.FirstOrDefaultAsync(x => x.UserId == model.UserId);
            if (checkExist != null)
            {

                checkExist.FoodList = model.FoodList;
                checkExist.StartTime = model.StartTime;


                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
