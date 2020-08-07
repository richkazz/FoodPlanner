using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FoodPlanner.Models.GrainDishes;
using FoodPlanner.Models.FoodSchedulerTimeStarts;
using FoodPlanner.Models.GrainDishNutrients;
using FoodPlanner.Models.LightFood;
using FoodPlanner.Models.MainIngredients;
using FoodPlanner.Models.Soups;
using FoodPlanner.Models.Swallows;
using FoodPlanner.Models.UserFoodSelectionCategory;
using FoodPlanner.Models.SoupCategory;
using FoodPlanner.Models.ContactUs;
using FoodPlanner.Models.FAQ;
using FoodPlanner.Models;

namespace Identity.Models
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options) { }
        public DbSet<FoodPlanner.Models.GrainDishes.GrainDish> GrainDish { get; set; }
        public DbSet<FoodPlanner.Models.FoodSchedulerTimeStarts.FoodSchedulerTimeStarts> FoodSchedulerTimeStarts { get; set; }
        public DbSet<FoodPlanner.Models.GrainDishNutrients.GrainDishNutrient> GrainDishNutrient { get; set; }
        public DbSet<FoodPlanner.Models.LightFood.LightFoodNutrient> LightFoodNutrient { get; set; }
        public DbSet<FoodPlanner.Models.LightFood.LightFood> LightFood { get; set; }
        public DbSet<FoodPlanner.Models.MainIngredients.MainIngredient> MainIngredient { get; set; }
        public DbSet<FoodPlanner.Models.Soups.Soup> Soup { get; set; }
        public DbSet<FoodPlanner.Models.Swallows.SwallowNutrient> SwallowNutrient { get; set; }
        public DbSet<FoodPlanner.Models.Swallows.Swallow> Swallow { get; set; }
        public DbSet<FoodPlanner.Models.UserFoodSelectionCategory.UserGrainDishSelection> UserGrainDishSelection { get; set; }
        public DbSet<FoodPlanner.Models.UserFoodSelectionCategory.UserLightFoodSelection> UserLightFoodSelection { get; set; }
        public DbSet<FoodPlanner.Models.UserFoodSelectionCategory.UserSoupSelection> UserSoupSelection { get; set; }
        public DbSet<FoodPlanner.Models.UserFoodSelectionCategory.UserSwallowSelection> UserSwallowSelection { get; set; }
        public DbSet<FoodPlanner.Models.SoupCategory.SwallowSoup> SwallowSoup { get; set; }
        public DbSet<FoodPlanner.Models.UserPlScheduler.UserPlSchedulers> UserPlScheduler { get; set; }
        public DbSet<FoodPlanner.Models.ContactUs.Contactus> Contactus { get; set; }
        public DbSet<FoodPlanner.Models.FAQ.FAQs> FAQs { get; set; }
        public DbSet<FoodPlanner.Models.SoupFrequency> SoupFrequency { get; set; }
    }
}
