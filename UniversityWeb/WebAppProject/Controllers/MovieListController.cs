using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLogic.interfaces;
using BusinessLogic.Services;
using DataDomain.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppProject.Models;

namespace WebAppProject.Controllers
{
    public class MovieListController : Controller
    {
        private readonly IViewMovies _mods;
        private readonly IShopItems _shoping;

        public MovieListController(IViewMovies mods, IShopItems shoping)
        {
            this._mods = mods;
            this._shoping = shoping;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [AutoValidateAntiforgeryToken]
        public IActionResult Shop()
        {
            return RedirectToAction("Purchases", "Shoping");
        }

        public IActionResult MovieCollection()
        {
            return this.View(this._mods.GetListOfMovies());
        }

        [Authorize(Roles = "User")]
        public IActionResult BuyMovie(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            this._shoping.BuyMovie(user, id);

            return RedirectToAction("Index","Home");
        }

        [Authorize]
        [AutoValidateAntiforgeryToken]
        public IActionResult Purchase(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movies = _mods.GetListOfMovies()
                .FirstOrDefault(m => m.Id == id);

            if (movies == null)
            {
                return NotFound();
            }

            return View(movies);
        }
    }
}