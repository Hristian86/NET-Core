using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLogic.interfaces;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MBshop.Models;
using BusinessLogic.OutputModels;

namespace MBshop.Controllers
{
    public class MovieListController : Controller
    {
        private readonly IViewMovies movieDb;
        private readonly IShopItems _shoping;
        private readonly IUserShopedProducts userItems;
        private readonly Status status;
        private List<OutputMovies> list = new List<OutputMovies>();

        public MovieListController(IViewMovies movieDb,
            IShopItems shoping,
            IUserShopedProducts userItems,
            Status status
            )
        {
            this.movieDb = movieDb;
            this._shoping = shoping;
            this.userItems = userItems;
            this.status = status;
        }

        public IActionResult MovieCollection(int orderBy)
        {

            this.list = this.movieDb.SortMovies(orderBy);

            if (User.Identity.Name != null)
            {

                //Get user id from cookies
                var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                //current user personal movies
                var userItm = userItems.PersonalMovies(user);

                if (userItm.Count != 0)
                {
                    //chek for possessed items in collections
                    status.StatusChekMovies(this.list, userItm);
                }

            }

            return this.View(this.list);
        }

        [HttpGet]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public IActionResult PurchaseMovie(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = movieDb.GetListOfMovies()
                .FirstOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> PurchaseMovie(int id, [Bind("Id,Title,Director,RealeaseDate,Genre,price,Discount,Picture,Actors,Raiting,Description")] OutputMovies movies)
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
                    var movi = this.movieDb.GetListOfMovies()
                        .Where(x => x.Id == movies.Id && x.price == movies.price)
                        .FirstOrDefault();

                    await this._shoping.BuyMovie(user, movies.Id);
                }
                else
                {
                    return NotFound();
                }

                return RedirectToAction("MovieCollection", "MovieList");

            }
            return View(movies);
        }

        private bool MoviesExists(int id)
        {
            return movieDb.GetListOfMovies()
                .Any(x => x.Id == id);
        }

    }
}