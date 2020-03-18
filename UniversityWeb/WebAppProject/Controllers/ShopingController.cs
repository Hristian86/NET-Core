using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.interfaces;
using DataDomain.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAppProject.Controllers
{
    public class ShopingController : Controller
    {
        private readonly IViewMovies _movieDb;

        public ShopingController(IViewMovies movieDb)
        {
            this._movieDb = movieDb;
        }

        public IActionResult Index()
        {
            return View();
        }


    }
}