using FoodPlanner.Interface;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Services
{
    public class ScheduleService : ISchedule
    {
        private readonly Identity.Models.AppIdentityDbContext _context;
        private UserManager<AppUser> _userManager;
        private IOperation _scheduleoperation;

        public ScheduleService(AppIdentityDbContext context, UserManager<AppUser> userMgr, IOperation scheduleoperation)
        {
            _context = context;
            _userManager = userMgr;
            _scheduleoperation = scheduleoperation;
        }


        // GET: GrainDishes
        public async Task<List<string>> RandomizeGrainDish(string userName)
        {
            //create a list of name resultgraindish
            List<string> resultgraindish = new List<string>();
            //create a list of name resultgraindishlist
            List<string> resultgraindishList = new List<string>();


            try
            {
                //to check if the item in resultgraindishList is less than 7
                while (resultgraindishList.Count < 7)
                {
                    //and if resultgraindishList.count is less than 7 it perfom the method randomItem
                    var chosengraindish = await randomItems(userName);

                    resultgraindishList = await randomResult(chosengraindish, resultgraindish);
                }

                return resultgraindishList;

            }
            catch (Exception ex)
            {
                return resultgraindish;
            }


        }

        //This is a method to get the list from the model
        #region Helper
        public async Task<List<string>> randomItems(string userName)
        {
            List<string> list = new List<string>();
            List<string> chosengraindish = new List<string>();


            //this is to get the list of string from the model
            var gettingfoodfromgraindish = await _context.UserGrainDishSelection.Where(x => x.UserId == userName).ToListAsync();


            //if statement to randomise the list of string gotten from the model
            if (gettingfoodfromgraindish.Count>3)
            {
                //to loop through the list gottten from the model and randomise it
                foreach (var item in gettingfoodfromgraindish)
                {
                    var random = new Random();
                    //add a nem item to the list 
                    list.Add(_context.GrainDish.FirstOrDefaultAsync(x => x.Id == item.UserGrainDishId).Result.Name);
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

        // This is a method to assign a key value to the list gotten from chosen and 
        //check weather the key is reperted more than two-times
        public async Task<List<string>> randomResult(List<string> chosengraindish, List<string> resultgraindish)
        {
            //to group the item in the list chosen into key and value
            Dictionary<string, int> keyValues = chosengraindish.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
            //to loop through the value gotten from keyvalue
            foreach (KeyValuePair<string, int> entry in keyValues)
            {
                //to stop the item in the list from going more than 7
                if (resultgraindish.Count < 7)
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


        #endregion

        // GET: GrainDishes
        public async Task<List<string>> RandomizeLightFood(string userName)
        {
            //create a list of name resultgraindish
            List<string> resultlightfood = new List<string>();
            //create a list of name resultgraindishlist
            List<string> resultlightfoodList = new List<string>();

            try
            {
                //to check if the item in resultgraindishList is less than 7
                while (resultlightfoodList.Count < 7)
                {
                    //and if resultgraindishList.count is less than 7 it perfom the method randomItem
                    var chosenlightfood = await randomItemsLightFood(userName);

                    resultlightfoodList = await randomResultLightFood(chosenlightfood, resultlightfood);
                }

                return resultlightfoodList;

            }
            catch (Exception ex)
            {
                return resultlightfood;
            }



        }

        //This is a method to get the list from the model
        #region Helper
        public async Task<List<string>> randomItemsLightFood(string userName)
        {
            List<string> list = new List<string>();
            List<string> chosenlghtfood = new List<string>();


            //this is to get the list of string from the model
            var gettingfoodfromlightfood = await _context.UserLightFoodSelection.Where(x => x.UserId == userName).ToListAsync();


            //if statement to randomise the list of string gotten from the model
            if (gettingfoodfromlightfood.Count > 3)
            {
                  foreach (var item in gettingfoodfromlightfood)
                    {
                        var random = new Random();
                        //add a nem item to the list 
                        list.Add(_context.LightFood.FirstOrDefaultAsync(x => x.Id == item.UserLightFoodId).Result.Name);
                        //list.Add(item.UserLightFoodId);
                        //randomise the index
                        int index = random.Next(list.Count);
                        //add the item from the list index to chosen
                        //chosenlghtfood.Add(list[index]);
                        chosenlghtfood.Add(list[index]);
                    }
                
                //to loop through the list gottten from the model and randomise it
                


            }
            else
            {
                throw new ArgumentException($"Number of food in the list should be more than 4 {nameof(chosenlghtfood)}");
            }
            return chosenlghtfood;


        }

        // This is a method to assign a key value to the list gotten from chosen and 
        //check weather the key is reperted more than two-times
        public async Task<List<string>> randomResultLightFood(List<string> chosenlghtfood, List<string> resultlightfood)
        {
            //to group the item in the list chosen into key and value
            Dictionary<string, int> keyValues = chosenlghtfood.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
            //to loop through the value gotten from keyvalue
            foreach (KeyValuePair<string, int> entry in keyValues)
            {
                //to stop the item in the list from going more than 7
                if (resultlightfood.Count < 7)
                {
                    //to check if the value of the item in the list is not grater than 2
                    if (entry.Value < 3)
                    {
                        //to add the item if it is not greater than 2
                        resultlightfood.Add(entry.Key);
                    }

                }

            }

            return resultlightfood;
        }


        #endregion

        public async Task<List<string>> RandomizeSwallow(string userName)
        {
            //create a list of name resultswallow
            List<string> resultswallow = new List<string>();
            //create a list of name resultgraindishlist
            List<string> resultswallowList = new List<string>();


            try
            {
                //to check if the item in resultswallowList is less than 7
                while (resultswallowList.Count < 7)
                {
                    //and if resultswallowList.count is less than 7 it perfom the method randomItem
                    var chosenswallow = await randomItemswallow(userName);

                    resultswallowList = await randomResultswallow(chosenswallow, resultswallow);
                }

                return resultswallowList;

            }
            catch (Exception ex)
            {
                return resultswallow;
            }


        }

        //This is a method to get the list from the model
        #region Helper
        public async Task<List<string>> randomItemswallow(string userName)
        {
            List<string> list = new List<string>();
            List<string> chosenswallow = new List<string>();


            //this is to get the list of string from the model
            var gettingfoodfromswallow = await _context.UserSwallowSelection.Where(x => x.UserId == userName).ToListAsync();


            //if statement to randomise the list of string gotten from the model
            if (gettingfoodfromswallow.Count>3)
            {
                //to loop through the list gottten from the model and randomise it
                foreach (var item in gettingfoodfromswallow)
                {
                    var random = new Random();
                    //add a nem item to the list 
                    list.Add(_context.Swallow.FirstOrDefaultAsync(x => x.Id == item.UserSwallowId).Result.Name);

                    //randomise the index
                    int index = random.Next(list.Count);
                    //add the item from the list index to chosen
                    chosenswallow.Add(list[index]);
                }


            }
            else
            {
                throw new ArgumentException($"Number of food in the list should be more than 4 {nameof(chosenswallow)}");
            }
            return chosenswallow;

        }

        // This is a method to assign a key value to the list gotten from chosen and 
        //check weather the key is reperted more than two-times
        public async Task<List<string>> randomResultswallow(List<string> chosenswallow, List<string> resultswallow)
        {
            //to group the item in the list chosen into key and value
            Dictionary<string, int> keyValues = chosenswallow.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
            //to loop through the value gotten from keyvalue
            foreach (KeyValuePair<string, int> entry in keyValues)
            {
                //to stop the item in the list from going more than 7
                if (resultswallow.Count < 7)
                {
                    //to check if the value of the item in the list is not grater than 2
                    if (entry.Value < 3)
                    {
                        //to add the item if it is not greater than 2
                        resultswallow.Add(entry.Key);
                    }

                }

            }

            return resultswallow;
        }


        #endregion

        public async Task<List<string>> OrderOne(string userName)
        {
            var lightFoodList = await RandomizeLightFood(userName);
            var grainDishesList = await RandomizeLightFood(userName);
            var swallowList = await RandomizeSwallow(userName);
            List<string> combinedlist = new List<string>();
            int len = lightFoodList.Count();
            for (int i = 0; i < len; i++)
            {
                combinedlist.Add(lightFoodList[i] + "|" + grainDishesList[i] + "|" + swallowList[i]);
            }

            //combinedlist.(RandomizeGrainDish,RandomizeLightFood,RandomizeSwallow)
            return combinedlist;

        }

        public async Task<List<string>> OrderTwo(string userName)
        {
            var lightFoodList = await RandomizeLightFood(userName);
            var grainDishesList = await RandomizeLightFood(userName);
            var swallowList = await RandomizeSwallow(userName);
            List<string> ordertwocombinedlist = new List<string>();
            int len = lightFoodList.Count();
            for (int i = 0; i < len; i++)
            {
                ordertwocombinedlist.Add(lightFoodList[i] + "|" + grainDishesList[i] + "|" + swallowList[i]);
            }

            //combinedlist.(RandomizeGrainDish,RandomizeLightFood,RandomizeSwallow)
            return ordertwocombinedlist;
        }

        public async Task<List<string>> SplitFoodList(string user)
        {
            List<string> splitfoodlist = new List<string>();
            List<string> unsplitfoodlist = new List<string>();
            List<AppUser> userlist = new List<AppUser>();
            

            
           

            var fetchWeeklySchedule = await _scheduleoperation.FetchFoodByUserId(user);


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


        public async Task<List<string>> ComperTime(string userName)
        {
            List<string> returnresult = new List<string>();
            List<DateTime> contain = new List<DateTime>();
            List<DateTime> containend = new List<DateTime>();

            var time = await _context.FoodSchedulerTimeStarts.ToListAsync();

            var user = await _userManager.FindByNameAsync(userName);
            var fetchWeeklyScheduletime = await _scheduleoperation.FetchFoodByTime(user.Id);

            DateTime now = DateTime.Now;
            var end = now.Date;
            contain.Add(fetchWeeklyScheduletime.StartTime.Date.AddDays(6));

            containend.Add(end);
            returnresult.Add(contain[0] + "|" + containend[0]);
            return returnresult;
        }



        #region comment
        //public async Task<List<string>> TimeStoreForFood()
        //{
        //    var weekorder0ne = await OrderOne();
        //    var weekordertwo = await OrderTwo();
        //    DateTime now = DateTime.Now;
        //    var end = now.AddDays(7);
        //    var start = now;

        //    if (weekorder0ne != null & weekordertwo != null)
        //    {

        //    }
        //    else
        //    {
        //        if (start == end)
        //        {

        //        }
        //    }


        //    }

        #endregion

    }
}
