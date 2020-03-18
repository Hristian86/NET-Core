using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAppProject.Controllers
{
    public class ShopedItemsController : Controller
    {
        private readonly IViewMovies _movieDb;

        public ShopedItemsController(IViewMovies movieDb)
        {
            this._movieDb = movieDb;
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
    }
}