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

        [Authorize]
        [AutoValidateAntiforgeryToken]
        public IActionResult Rate()
        {
            if (User.Identity.Name != null)
            {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                rateService.GetUserRate(userId);
            }

            return this.View();
        }

        [HttpPost]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> RateMovie([Bind("Id", "Raiting")] OutputMovies model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                GlobalAlertMessages.StatusMessage = await this.rateService.RateMovie(model, userId);

                return this.RedirectToAction("UserMovieShops", "UserShopedItems");
            }
            else
            {
                return RedirectToAction("Error404Page", "Error404");
            }

        }

        [HttpPost]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> RateBook([Bind("Id", "Raiting")] OutputBooks model)
        {
            if (ModelState.IsValid)
            {
                var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                GlobalAlertMessages.StatusMessage = await this.rateService.RateBook(model, user);

                return this.RedirectToAction("UserBooksShops", "UserShopedItems");
            }
            else
            {
                return RedirectToAction("Error404Page", "Error404");
            }
        }
    }
}