using FoodPlanner.Interfaces;
using FoodPlanner.Models.ContactUs;
using Identity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Services
{
    public class ContactUsService : IContactUs
    {
        private readonly AppIdentityDbContext _context;


        public ContactUsService(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<List<Contactus>> FetchFoodByUserId()
        {
            var model = _context.Contactus

                .Select(model => new Contactus
                {
                    Email = model.Email,
                    Body = model.Body,
                    Name = model.Name
                }).ToList();
            return model;
        }
    }
}
