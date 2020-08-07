using FoodPlanner.Interface;
using FoodPlanner.Interfaces;
using FoodPlanner.Models;
using FoodPlanner.Models.UserPlScheduler;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Services
{
    public class SoupFrequencyService : ISoupFrequency
    {
        private readonly Identity.Models.AppIdentityDbContext _context;
        private UserManager<AppUser> _userManager;
        private IOperation _scheduleoperation;

        public SoupFrequencyService(AppIdentityDbContext context, UserManager<AppUser> userMgr, IOperation scheduleoperation)
        {
            _context = context;
            _userManager = userMgr;
            _scheduleoperation = scheduleoperation;
        }
        public async Task<List<string>> StartSoupFrequencyProcess(string userName,int item)
        {
            var postSoupFrequency = new UserPlSchedulers
            {
                SoupFrequency = item,
                UserId = userName

            };
            var createFoodList = await ÙpdateSoupFrequency(postSoupFrequency);

            var order = await OrderOne(userName);
            string combinedString = string.Join("|", order);

            var postSoupList = new UserPlSchedulers
            {
                SoupList = combinedString,
                UserId = userName

            };
            var FoodList = await ÙpdateSoupList(postSoupList);
            List<string> passValue = new List<string>();

            return passValue;
        }
        public async Task<List<SoupFrequency>> GetSelectFrequency()
        {
            var model = _context.SoupFrequency

               .Select(model => new SoupFrequency
               {
                   SoupCount = model.SoupCount,

                   Id = model.Id
               }).ToList();

            return model;
        }

        public async Task<bool> CreateSoupList(UserPlSchedulers model)
        {
            bool msg = false;





            var checkExist = await _context.UserPlScheduler.FirstOrDefaultAsync(x => x.UserId == model.UserId);
            if (checkExist == null)
            {
                var result = new UserPlSchedulers
                {
                    SoupList = model.SoupList,
                    SoupFrequency = model.SoupFrequency,

                };
                await _context.UserPlScheduler.AddAsync(result);
                if (await _context.SaveChangesAsync() > 0)
                {
                    msg = true;
                }
            }

            return msg;
        }

        public async Task<bool> ÙpdateSoupFrequency(UserPlSchedulers model)
        {
            var checkExist = await _context.UserPlScheduler.FirstOrDefaultAsync(x => x.UserId == model.UserId);
            if (checkExist != null)
            {
                checkExist.SoupFrequency = model.SoupFrequency;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool>  ÙpdateSoupList(UserPlSchedulers model)
        {
            var checkExist = await _context.UserPlScheduler.FirstOrDefaultAsync(x => x.UserId == model.UserId);
            if (checkExist != null)
            {
                checkExist.SoupList = model.SoupList;
                 await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<string>> randomResult(UserPlSchedulers model, List<string> chosengraindish, List<string> resultgraindish)
        {
            var soupcount = _context.UserPlScheduler.Where(x => x.UserId == model.UserId).Select(x => x.SoupFrequency).ToList();

            //to group the item in the list chosen into key and value
            Dictionary<string, int> keyValues = chosengraindish.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
            //to loop through the value gotten from keyvalue
            foreach (KeyValuePair<string, int> entry in keyValues)
            {
                //to stop the item in the list from going more than 7
                if (resultgraindish.Count < soupcount[0])
                {
                    //to check if the value of the item in the list is not grater than 2
                    if (entry.Value < 3)
                    {
                        //to add the item if it is not greater than 2
                        resultgraindish.Add(entry.Key);
                    }

                }

            }

            return resultgraindish;
        }


        public async Task<List<string>> OrderOne(string userName)
        {
            

            var lightFoodList = await RandomizeSoup(userName);

            List<string> combinedlist = new List<string>();
            int len = lightFoodList.Count();
            for (int i = 0; i < len; i++)
            {
                combinedlist.Add(lightFoodList[i]);
            }

            //combinedlist.(RandomizeGrainDish,RandomizeLightFood,RandomizeSwallow)
            return combinedlist;
        }

        public Task<List<string>> OrderTwo()
        {
            throw new NotImplementedException();
        }

        public async Task<List<string>> RandomizeSoup(string userName)
        {
            List<string> splitfoodlist = new List<string>();
            List<int> unsplitfoodlist = new List<int>();
            List<AppUser> userlist = new List<AppUser>();

            //var mod=  model.Select(x => x.UserId);

            List<string> resultgraindish = new List<string>();
            //create a list of name resultgraindishlist
            List<string> resultgraindishList = new List<string>();
            var soupcount = _context.UserPlScheduler.Where(x => x.UserId == userName).Select(x => x.SoupFrequency).ToList();


            try
            {

                //to check if the item in resultgraindishList is less than 7
                while (resultgraindishList.Count < soupcount[0])
                {
                    //and if resultgraindishList.count is less than 7 it perfom the method randomItem
                    var chosengraindish = await randomItems(userName);

                    resultgraindishList = await randomResult(chosengraindish, resultgraindish,userName);
                }

                return resultgraindishList;

            }
            catch (Exception ex)
            {
                return resultgraindish;
            }
        }
        public async Task<List<string>> randomItems(string userName)
        {
            
            List<string> list = new List<string>();
            List<string> chosengraindish = new List<string>();

            var soupcount = _context.UserPlScheduler.Where(x => x.UserId == userName).Select(x => x.SoupFrequency).ToList();

            //this is to get the list of string from the model
            var gettingfoodfromgraindish = await _context.UserSoupSelection.ToListAsync();


            //if statement to randomise the list of string gotten from the model
            if (gettingfoodfromgraindish.Count != soupcount[0])
            {
                //to loop through the list gottten from the model and randomise it
                foreach (var item in gettingfoodfromgraindish)
                {
                    var random = new Random();
                    //add a nem item to the list 
                    list.Add(_context.Soup.FirstOrDefaultAsync(x => x.Id == item.UserSoupId).Result.Name);
                    //randomise the index
                    int index = random.Next(list.Count);
                    //add the item from the list index to chosen
                    chosengraindish.Add(list[index]);
                }


            }
            else
            {
                throw new ArgumentException($"Number of food in the list should be more than 4 {nameof(chosengraindish)}");
            }
            return chosengraindish;

        }
        public async Task<List<string>> randomResult(List<string> chosengraindish, List<string> resultgraindish ,string userName)
        {
            var soupcount = _context.UserPlScheduler.Where(x => x.UserId == userName).Select(x => x.SoupFrequency).ToList();

            //to group the item in the list chosen into key and value
            Dictionary<string, int> keyValues = chosengraindish.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
            //to loop through the value gotten from keyvalue
            foreach (KeyValuePair<string, int> entry in keyValues)
            {
                //to stop the item in the list from going more than 7
                if (resultgraindish.Count < soupcount[0])
                {
                    //to check if the value of the item in the list is not grater than 2
                    if (entry.Value == 1)
                    {
                        //to add the item if it is not greater than 2
                        resultgraindish.Add(entry.Key);
                    }

                }

            }

            return resultgraindish;
        }


        public async Task<List<string>> SplitFoodList(string userName)
        {
            List<string> splitfoodlist = new List<string>();
            List<string> unsplitfoodlist = new List<string>();
            List<AppUser> userlist = new List<AppUser>();



            if (userName != null)
            {
                var user = await _userManager.FindByIdAsync(userName);
                if (user != null)
                {
                    userlist.Add(user);
                }


            }
            if (userlist.Count == 0)
            {
                var user = await _userManager.FindByNameAsync(userName);
                userlist.Add(user);
            }

            var fetchWeeklySchedule = await _scheduleoperation.FetchFoodByUserId(userlist[0].Id);


            //var Check = await _context.UserPlSchedulers.ToListAsync();


            //foreach (var item in Check)
            //{
            //    splitfoodlist.Add(item.FoodList);
            //}

            var splittedlist = fetchWeeklySchedule.FoodList.Split(new char[] { '#' });
            for (int i = 0; i < splittedlist.Length; i++)
            {
                unsplitfoodlist.Add(splittedlist[i]);
            }

            return unsplitfoodlist;
        }

        public Task<bool> GetCreateSoupList(UserPlSchedulers model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PostCreateSoupList(UserPlSchedulers model)
        {
            throw new NotImplementedException();
        }
        public async Task<List<int>> FetchFoodByUserId(string userName)
        {

            List<string> splitfoodlist = new List<string>();
            List<int> unsplitfoodlist = new List<int>();
            List<AppUser> userlist = new List<AppUser>();



            if (userName != null)
            {
                var user = await _userManager.FindByIdAsync(userName);
                if (user != null)
                {
                    userlist.Add(user);
                }


            }
            if (userlist.Count == 0)
            {
                var user = await _userManager.FindByNameAsync(userName);
                userlist.Add(user);
            }

            var fetchWeeklySchedule = await _scheduleoperation.FetchFoodByUserId(userlist[0].Id);

            var splittedlist = fetchWeeklySchedule.SoupFrequency;

            unsplitfoodlist.Add(splittedlist);
            return unsplitfoodlist;

        }

        
    }
    }
