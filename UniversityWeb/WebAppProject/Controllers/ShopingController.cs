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
        public IActionResult Purchase(int id, [Bind("Id,Title,Director,RealeaseDate,Genre,price,Discount,Picture,Actors,Raiting,Description")] Movieses movies)
        {


            var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;





            if (id != movies.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    //this._shoping.BuyMovie(user, movies.Id);


                }

                catch (InvalidOperationException ex)
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

        private bool MoviesExists(int id)
        {
            return _movieDb.GetListOfMovies().Any(x => x.Id == id);
        }
    }
}