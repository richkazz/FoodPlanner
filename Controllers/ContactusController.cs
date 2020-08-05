using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodPlanner.Models.ContactUs;
using Identity.Models;
using FoodPlanner.Interfaces;

namespace FoodPlanner.Controllers
{
    public class ContactusController : Controller
    {

        private readonly AppIdentityDbContext _context;
        private IContactUs _contactManager;

        public ContactusController(AppIdentityDbContext context, IContactUs contactManager)
        {
            _context = context;
            _contactManager = contactManager;
        }

        // GET: Contactus
        public async Task<IActionResult> Index()
        {
            List<string> con = new List<string>();
            
            var swallows = await _contactManager.FetchFoodByUserId();
            List<string> roles = swallows.Select(x => x.Name+"|"+x.Email+"|"+x.Body+"|"+x.Id).ToList();
            ViewBag.Monday0 = roles;
            //for (int i=0; i< roles.Count; i++)
            //{
            //    var cont = (roles[i].Split(new char[] { '|' }));
            //    con.Add(cont[0]);
            //    con.Add(cont[1]);
            //    con.Add(cont[2]);
            //    ViewBag.Monday0 = con[0]; ViewBag.Monday1 = con[1]; ViewBag.Monday2 = con[2];

            //}

            return View();
        }

        // GET: Contactus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactus = await _context.Contactus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactus == null)
            {
                return NotFound();
            }

            return View(contactus);
        }

        // GET: Contactus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contactus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Body")] Contactus contactus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactus);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return Redirect( "/");
            }
            return View(contactus);
        }

        // GET: Contactus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactus = await _context.Contactus.FindAsync(id);
            if (contactus == null)
            {
                return NotFound();
            }
            return View(contactus);
        }

        // POST: Contactus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Body")] Contactus contactus)
        {
            if (id != contactus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactusExists(contactus.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(contactus);
        }

        // GET: Contactus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactus = await _context.Contactus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactus == null)
            {
                return NotFound();
            }

            return View(contactus);
        }

        // POST: Contactus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactus = await _context.Contactus.FindAsync(id);
            _context.Contactus.Remove(contactus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactusExists(int id)
        {
            return _context.Contactus.Any(e => e.Id == id);
        }
    }
}
