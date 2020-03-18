using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.interfaces;
using BusinessLogic.Services;
using DataDomain.Data.Models;
using Microsoft.AspNetCore.Mvc;
using WebAppProject.Models;

namespace WebAppProject.Controllers
{
    public class MovieListController : Controller
    {
        private readonly IViewMovies _mods;

        public MovieListController(IViewMovies mods)
        {
            this._mods = mods;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MovieCollection()
        {
            return this.View(this._mods.GetListOfMovies());
        }
    }
}