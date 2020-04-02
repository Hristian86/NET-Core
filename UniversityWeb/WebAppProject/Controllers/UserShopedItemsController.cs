using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MBshopService.interfaces;
using MBshopService.OutputModels;
using MBshopService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MBshop.Controllers
{
    public class UserShopedItemsController : Controller
    {
        private readonly IUserShopedProducts products;

        public UserShopedItemsController(IUserShopedProducts products)
        {
            this.products = products;
        }

        [Authorize]
        [AutoValidateAntiforgeryToken]
        public IActionResult UserMovieShops()
        {
            var user = CurrentUser();

            if (user == null)
            {
                return NotFound();
            }

            IEnumerable<OutputMovies> movi = products.PersonalMovies(user);

            return View(movi);
        }

        [Authorize]
        [AutoValidateAntiforgeryToken]
        public IActionResult UserBooksShops()
        {
            var user = CurrentUser();

            if (user == null)
            {
                return NotFound();
            }

            IEnumerable<OutputBooks> books = products.PersonalBooks(user); 

            return View(books);
        }

        private string CurrentUser()
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return user;
        }
    }
}