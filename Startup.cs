using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Identity.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Identity.IdentityPolicy;
using System;
using Microsoft.AspNetCore.Authorization;
using Identity.CustomPolicy;
using FoodPlanner.Interface;
using FoodPlanner.Services;
using FoodPlanner.Interfaces;
using FoodPlanner.Email;
using NToastNotify;

namespace Identity
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IPasswordValidator<AppUser>, CustomPasswordPolicy>();
            services.AddTransient<IUserValidator<AppUser>, CustomUsernameEmailPolicy>();
            

            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();
            services.AddScoped<ISchedule, ScheduleService>();
            services.AddScoped<IUserLoginStatus, UserLoginStatusService>();
            services.AddScoped<IContactUs, ContactUsService>();
            services.AddScoped<ISoupFrequency, SoupFrequencyService>();
            services.AddScoped<IOperation, OperationService>();
            services.AddScoped<IEmailSender, EmailSender>();
            /*services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = ".AspNetCore.Identity.Application";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                options.SlidingExpiration = true;
            });*/

            /*services.ConfigureApplicationCookie(opts =>
            {
                opts.LoginPath = "/Authenticate/Login";
                opts.AccessDeniedPath = "/Stop/Index";
            });*/
            services.AddMvc().AddNToastNotifyToastr(new ToastrOptions()
            {
                ProgressBar = false,
                PositionClass = ToastPositions.TopCenter
            });

            services.Configure<IdentityOptions>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                opts.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz";
                opts.Password.RequiredLength = 8;
                opts.Password.RequireNonAlphanumeric = true;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = true;
                opts.Password.RequireDigit = true;

                //opts.SignIn.RequireConfirmedEmail = true;

                opts.Lockout.AllowedForNewUsers = true;
                opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                opts.Lockout.MaxFailedAccessAttempts = 3;
            });

            services.AddAuthorization(opts => {
                opts.AddPolicy("AspManager", policy => {
                    policy.RequireRole("Manager");
                    policy.RequireClaim("Coding-Skill", "ASP.NET Core MVC");
                });
            });

            services.AddTransient<IAuthorizationHandler, AllowUsersHandler>();
            services.AddAuthorization(opts => {
                opts.AddPolicy("AllowTom", policy => {
                    policy.AddRequirements(new AllowUserPolicy("tom"));
                });
            });

            services.AddTransient<IAuthorizationHandler, AllowPrivateHandler>();
            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("PrivateAccess", policy =>
                {
                    policy.AddRequirements(new AllowPrivatePolicy());
                });
            });

            services.AddAuthentication()
                .AddGoogle(opts =>
                {
                    opts.ClientId = "717469225962-3vk00r8tglnbts1cgc4j1afqb358o8nj.apps.googleusercontent.com";
                    opts.ClientSecret = "babQzWPLGwfOQVi0EYR-7Fbb";
                    opts.SignInScheme = IdentityConstants.ExternalScheme;
                });

            services.AddControllersWithViews();

            var emailConfig = Configuration.GetSection("EmailConfiguration");
            services.Configure<EmailConfiguration>(emailConfig);
            var emailConfigurationSection = emailConfig.Get<EmailConfiguration>();
            //.Get<FoodPlanner.Email.EmailConfiguration>();
            // services.AddSingleton(emailConfig);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days.
                app.UseHsts();
            }
            app.UseNToastNotify();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
