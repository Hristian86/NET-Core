using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using DataAccess.UserRental;

namespace UniversityWeb.Controllers
{
    public class UserRentalsController : Controller
    {
        private readonly Enties _context;

        public UserRentalsController(Enties context)
        {
            _context = context;
        }

        // GET: UserRentals
        public async Task<IActionResult> Index()
        {
            return View(await _context.rental.ToListAsync());
        }

        // GET: UserRentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRentals = await _context.rental
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userRentals == null)
            {
                return NotFound();
            }

            return View(userRentals);
        }

        // GET: UserRentals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserRentals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MovieRented,Date,MovieId,IdentityMovieId")] UserRentals userRentals)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userRentals);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userRentals);
        }

        // GET: UserRentals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRentals = await _context.rental.FindAsync(id);
            if (userRentals == null)
            {
                return NotFound();
            }
            return View(userRentals);
        }

        // POST: UserRentals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MovieRented,Date,MovieId,IdentityMovieId")] UserRentals userRentals)
        {
            if (id != userRentals.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userRentals);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserRentalsExists(userRentals.Id))
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
            return View(userRentals);
        }

        // GET: UserRentals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRentals = await _context.rental
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userRentals == null)
            {
                return NotFound();
            }

            return View(userRentals);
        }

        // POST: UserRentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userRentals = await _context.rental.FindAsync(id);
            _context.rental.Remove(userRentals);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserRentalsExists(int id)
        {
            return _context.rental.Any(e => e.Id == id);
        }
    }
}