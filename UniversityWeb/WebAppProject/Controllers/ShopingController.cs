using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLogic.interfaces;
using BusinessLogic.OutputModels;
using DataDomain.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAppProject.Controllers
{
    public class ShopingController : Controller
    {
        private readonly IViewMovies _movieDb;
        private readonly IShopItems _shoping;

        public ShopingController(IViewMovies movieDb,
            IShopItems shoping)
        {
            this._movieDb = movieDb;
            this._shoping = shoping;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Purchases(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movies = _movieDb.GetListOfMovies()
                .FirstOrDefault(m => m.Id == id);

            if (movies == null)
            {
                return NotFound();
            }

            return View(movies);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AutoValidateAntiforgeryToken]
        public IActionResult Purchase(int id, [Bind("Id,Title,Director,RealeaseDate,Genre,price,Discount,Picture,Actors,Raiting,Description")] OutputMovies movies)
        {


            var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (id != movies.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (MoviesExists(movies.Id))
                {
                    var movi = this._movieDb.GetListOfMovies()
                        .Where(x => x.Id == movies.Id && x.price == movies.price)
                        .FirstOrDefault();

                    this._shoping.BuyMovie(user, movies.Id);
                }
                else
                {
                    return NotFound();
                }
                return RedirectToAction("MovieCollection", "MovieList");
                //return RedirectToAction(nameof(Index));
                
            }
            return View();
        }

        private bool MoviesExists(int id)
        {
            return _movieDb.GetListOfMovies()
                .Any(x => x.Id == id);
        }
    }
}