using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MBshop.Service.interfaces;
using MBshop.Service.OutputModels;
using MBshop.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MBshop.Controllers
{
    public class UserShopedItemsController : Controller
    {
        private readonly IUserShopedProductsService products;
        private IEnumerable<OutputMovies> movi;
        private IEnumerable<OutputBooks> books;

        public UserShopedItemsController(IUserShopedProductsService products)
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
                return RedirectToAction("Error404Page", "Error404");
            }

            try
            {
                this.movi = this.products.PersonalMovies(user);
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException("Problem with mapping models",e);
            }

            return this.View(this.movi);
        }

        [Authorize]
        [AutoValidateAntiforgeryToken]
        public IActionResult UserBooksShops()
        {
            var user = CurrentUser();

            if (user == null)
            {
                return RedirectToAction("Error404Page", "Error404");
            }

            try
            {
                this.books = this.products.PersonalBooks(user);
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException("Problem with mapping books",e);
            }

            return this.View(this.books);
        }

        private string CurrentUser()
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return user;
        }
    }
}