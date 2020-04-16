using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MBshop.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using MBshop.Data;
using Microsoft.AspNetCore.Authorization;
using MBshop.Data.Data;

namespace MBshop.Controllers
{

    [Authorize(Roles = "Admin")]
    public class MoviesController : Controller
    {
        private readonly MovieShopDBSEContext db;

        public MoviesController(MovieShopDBSEContext db)
        {
            this.db = db;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            return View(await this.db.Movies.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movies = await db.Movies
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
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Director,RealeaseDate,Genre,price,Discount,Picture,Actors,Raiting,Description,LinkForProductContentWhenPurchase,Rate")] Movies movies)
        {

            if (ModelState.IsValid)
            {
                movies.Rate = 0;
                db.Add(movies);
                await db.SaveChangesAsync();
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

            var movies = await db.Movies.FindAsync(id);

            if (movies == null)
            {
                return NotFound();
            }

            return View(movies);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Director,RealeaseDate,Genre,price,LinkForProductContentWhenPurchase,Discount,Picture,Actors,Raiting,Description,Rate")] Movies movies)
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

                    db.Update(movies);
                    await db.SaveChangesAsync();
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

            var movies = await db.Movies
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movies == null)
            {
                return NotFound();
            }

            return View(movies);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movies = await db.Movies.FindAsync(id);
            
            //cascade delete manualy 
            var shopMovies = db.Shops.Where(x => x.MovieId == movies.Id).ToList();
            var ratingMovies = db.Rating.Where(x => x.MoviesId == movies.Id).ToList();

            db.Rating.RemoveRange(ratingMovies);
            db.RemoveRange(shopMovies);
            db.Movies.Remove(movies);

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoviesExists(int id)
        {
            return db.Movies.Any(e => e.Id == id);
        }
    }
}