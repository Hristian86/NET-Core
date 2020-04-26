using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MBshop.Service.interfaces;
using MBshop.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MBshop.Models;
using MBshop.Service.OutputModels;

namespace MBshop.Controllers
{
    public class MovieListController : Controller
    {
        private readonly IViewMoviesService movieDb;
        private readonly IUserShopedProductsService userItems;
        private readonly Status status;

        public MovieListController(IViewMoviesService movieDb,
            IUserShopedProductsService userItems,
            Status status
            )
        {
            this.movieDb = movieDb;
            this.userItems = userItems;
            this.status = status;
        }

        [AllowAnonymous]
        public IActionResult MovieCollection(int orderBy, string searchItem, string type)
        {
            
            var list = this.movieDb.SortMovies(orderBy);

            if (User.Identity.Name != null)
            {

                //Get user id from cookies
                var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                //current user personal movies
                var userItm = this.userItems.PersonalMovies(user);

                if (userItm.Count != 0)
                {
                    //chek for possessed items in collections
                    this.status.StatusChekMovies(list, userItm);
                }

            }
            
            return this.View(list);
        }

        [HttpGet]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public IActionResult MovieDetail(int? id, string type)
        {
            if (id == null)
            {
                return RedirectToAction("Error404Page", "Error404");
            }

            var movie = this.movieDb.GetListOfMovies()
                .FirstOrDefault(m => m.Id == id);

            string user = "";

            if (User.Identity.Name != null && movie != null)
            {
                //Get user id from cookies
                user = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                bool userMovie = this.userItems.PersonalMovies(user).Any(x => x.Id == movie.Id);

                if (userMovie)
                {
                    movie.Status = true;
                }
            }
            //status check for movies in purchase method

            if (movie == null)
            {
                return RedirectToAction("Error404Page", "Error404");
            }

            return View(movie);
        }
    }
}