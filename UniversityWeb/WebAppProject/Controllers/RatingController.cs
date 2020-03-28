using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLogic.OutputModels;
using BusinessLogic.Services;
using Data.Domain.Data;
using Db.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MBshop.Controllers
{

    public class RatingController : Controller
    {
        private readonly MovieShopDBSEContext db;
        private readonly RatingMovies rateService;

        public RatingController(MovieShopDBSEContext db,
            RatingMovies rateService)
        {
            this.db = db;
            this.rateService = rateService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> RateMovie(OutputMovies model)
        {
            if (ModelState.IsValid)
            {
                var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                double rating = await this.rateService.RateMovie(model,user);

                return this.RedirectToAction("MovieCollection", "MovieList");
            }
            return this.View();
        }

        [HttpGet]
        public async Task<IActionResult> RateBook(OutputBooks model)
        {
            if (ModelState.IsValid)
            {
                var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                double rating = await this.rateService.RateBook(model, user);

                return this.RedirectToAction("BooksCollection", "BookList");
            }
            return this.View();
        }

    }
}