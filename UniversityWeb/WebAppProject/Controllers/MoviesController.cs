using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataDomain.Data.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using DataDomain;
using Microsoft.AspNetCore.Authorization;

namespace WebAppProject.Controllers
{

    [Authorize(Roles = "Admin")]
    public class MoviesController : Controller
    {
        private readonly MovieRentalDBSEContext _context;

        public MoviesController(MovieRentalDBSEContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                CurrentUserTest.Errors = "Not alowed to enter untill loged in";
                return RedirectToAction("Index", "Home");
            }

            return View(await _context.Movies.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!User.Identity.IsAuthenticated)
            {
                ViewData.ModelState.AddModelError("Not alowed", "You need to be loged in");
                return RedirectToAction("Index", "Home");
            }

            var movies = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movies == null)
            {
                return NotFound();
            }

            return View(movies);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Director,RealeaseDate,Genre,price,Discount,Picture")] Movies movies)
        {

            if (ModelState.IsValid)
            {
                //var curPrice = movies.price;
                //var discount = movies.Discount;
                //var total = curPrice - discount;

                //if (total >= 0)
                //{
                //    movies.price = total;
                //}
                
                _context.Add(movies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movies);

        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (User.Identity.IsAuthenticated)
            {

                var movies = await _context.Movies.FindAsync(id);

                if (movies == null)
                {
                    return NotFound();
                }
                return View(movies);
            }

            return RedirectToAction("Index", "Home");
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Director,RealeaseDate,Genre,price,Discount,Picture")] Movies movies)
        {
            if (id != movies.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //var sum = 0.00;
                    //var curPricew = movies.price;
                    //var disc = movies.Discount;
                    //sum = curPricew - disc;

                    //if (sum >= 0)
                    //{
                    //    movies.price = sum;
                    //}

                    _context.Update(movies);
                    await _context.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!MoviesExists(movies.Id))
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
            return View(movies);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (User.Identity.IsAuthenticated)
            {

                var movies = await _context.Movies
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (movies == null)
                {
                    return NotFound();
                }

                return View(movies);
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movies = await _context.Movies.FindAsync(id);
            _context.Movies.Remove(movies);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoviesExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}