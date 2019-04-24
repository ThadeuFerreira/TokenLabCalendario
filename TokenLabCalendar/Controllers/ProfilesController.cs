using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TokenLabCalendar.Models;

namespace TokenLabCalendar.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly CalendarioContext _context;

        public ProfilesController(CalendarioContext context)
        {
            _context = context;
        }

        // GET: Profiles
        public async Task<IActionResult> Index()
        {
            var calendarioContext = _context.Profiles.Include(p => p.ApplicationUser);
            return View(await calendarioContext.ToListAsync());
        }

        // GET: Profiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profile = await _context.Profiles
                .Include(p => p.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profile == null)
            {
                return NotFound();
            }

            var events = _context.Events.Where(c => c.Profile.Id == profile.Id).ToList();

            profile.Events = events;
            return View(profile);
        }

        // GET: Profiles/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserForeignKey"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Profiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ProfileDescription,ApplicationUserForeignKey")] Profile profile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(profile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserForeignKey"] = new SelectList(_context.Users, "Id", "Id", profile.ApplicationUserForeignKey);
            return View(profile);
        }

        // GET: Profiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profile = await _context.Profiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserForeignKey"] = new SelectList(_context.Users, "Id", "Id", profile.ApplicationUserForeignKey);
            return View(profile);
        }

        // POST: Profiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ProfileDescription,ApplicationUserForeignKey")] Profile profile)
        {
            if (id != profile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfileExists(profile.Id))
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
            ViewData["ApplicationUserForeignKey"] = new SelectList(_context.Users, "Id", "Id", profile.ApplicationUserForeignKey);
            return View(profile);
        }

        // GET: Profiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profile = await _context.Profiles
                .Include(p => p.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }

        // POST: Profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profile = await _context.Profiles.FindAsync(id);
            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfileExists(int id)
        {
            return _context.Profiles.Any(e => e.Id == id);
        }
    }
}
