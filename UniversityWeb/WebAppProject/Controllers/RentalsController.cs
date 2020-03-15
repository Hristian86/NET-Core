using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataDomain.Data.Models;
using Microsoft.AspNetCore.Identity;
using DataDomain;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace WebAppProject.Controllers
{
    public class RentalsController : Controller
    {
        private readonly MovieRentalDBSEContext _context;

        public RentalsController(MovieRentalDBSEContext context)
        {
            _context = context;
        }


        // GET: Rentals
        public async Task<IActionResult> Index()
        {
            var users = User.Identity.Name;
            //CurrentUserTest.UserName;

            if (users != null)
            {
            var currentUser = _context.AspNetUsers.Where(x => x.UserName == users).FirstOrDefault();
                var id = currentUser.Id;
            }

            var movieRentalDBSContext = _context.Rentals.Include(r => r.Movie).Include(r => r.User);
            return View(await movieRentalDBSContext.ToListAsync());
        }

        // GET: Rentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentals = await _context.Rentals
                .Include(r => r.Movie)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rentals == null)
            {
                return NotFound();
            }

            return View(rentals);
        }

        // GET: Rentals/Create
        public IActionResult Create()
        {
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return View();
        }

        // POST: Rentals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,MovieId")] Rentals rentals)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rentals);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", rentals.MovieId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", rentals.UserId);
            return View(rentals);
        }

        // GET: Rentals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentals = await _context.Rentals.FindAsync(id);
            if (rentals == null)
            {
                return NotFound();
            }
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", rentals.MovieId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", rentals.UserId);
            return View(rentals);
        }

        // POST: Rentals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,MovieId")] Rentals rentals)
        {
            if (id != rentals.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rentals);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentalsExists(rentals.Id))
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
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", rentals.MovieId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", rentals.UserId);
            return View(rentals);
        }

        // GET: Rentals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentals = await _context.Rentals
                .Include(r => r.Movie)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rentals == null)
            {
                return NotFound();
            }

            return View(rentals);
        }

        // POST: Rentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rentals = await _context.Rentals.FindAsync(id);
            _context.Rentals.Remove(rentals);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentalsExists(int id)
        {
            return _context.Rentals.Any(e => e.Id == id);
        }
    }
}
