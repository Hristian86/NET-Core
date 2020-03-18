using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLogic.interfaces;
using BusinessLogic.Services;
using DataDomain.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppProject.Models;

namespace WebAppProject.Controllers
{
    public class MovieListController : Controller
    {
        private readonly IViewMovies _mods;
        private readonly IShopItems _createRental;

        public MovieListController(IViewMovies mods, IShopItems createRental)
        {
            this._mods = mods;
            this._createRental = createRental;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MovieCollection()
        {
            return this.View(this._mods.GetListOfMovies());
        }

        public IActionResult BuyMovie(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            this._createRental.BuyMovie(user, id);

            return RedirectToAction("Index","Home");
        }

    }
}