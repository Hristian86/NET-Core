using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MBshopService.interfaces;
using MBshopService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MBshop.Models;
using MBshopService.OutputModels;

namespace MBshop.Controllers
{
    public class MovieListController : Controller
    {
        private readonly IViewMovies movieDb;
        private readonly IShopItems shoping;
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
            this.shoping = shoping;
            this.userItems = userItems;
            this.status = status;
        }

        public IActionResult MovieCollection(int orderBy, string searchItem, string type)
        {
            if (searchItem != null && type == "Movie")
            {
                List<OutputMovies> result = this.movieDb.GetListOfMovies()
                    .Where(x => x.Title.ToLower().Contains(searchItem.ToLower()))
                    .ToList();
                if (result.Count != 0)
                {
                    return this.View(result);
                }
            }
            else if (searchItem != null && type == "Book")
            {
                return this.RedirectToAction("BooksCollection", "BookList", new { searchItem });
            }

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
        public IActionResult PurchaseMovie(int? id, string type)
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


    }
}