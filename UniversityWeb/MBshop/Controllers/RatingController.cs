using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MBshop.Service.interfaces;
using MBshop.Service.OutputModels;
using MBshop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MBshop.Service.StaticProperyes;

namespace MBshop.Controllers
{

    public class RatingController : Controller
    {
        private readonly IRatingSistemService rateService;

        public RatingController(
            IRatingSistemService rateService)
        {
            this.rateService = rateService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> RateMovie(OutputMovies model)
        {
            if (ModelState.IsValid)
            {
                var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                 StatusForCartCount.MessageForStaatus = await this.rateService.RateMovie(model,user);

                return this.RedirectToAction("MovieCollection", "MovieList");
            }
            return this.View();
        }

        [HttpGet]
        [Authorize]
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