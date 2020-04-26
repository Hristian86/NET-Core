using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MBshop.Service.StaticProperyes;
using MBshop.Service.interfaces;
using MBshop.Service.OutputModels;
using MBshop.Service.Services;
using MBshop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MBshop.Service.WebConstants;

namespace MBshop.Controllers
{

    public class CartController : Controller
    {
        private readonly IShopItemsService shopService;
        private readonly ICartService cartBasket;

        public CartController(IShopItemsService shopService,
            ICartService cartBasket)
        {
            this.shopService = shopService;
            this.cartBasket = cartBasket;
        }

        [AllowAnonymous]
        public IActionResult Cart()
        {

            if (User.Identity.Name == null)
            {
                return this.View();
            }

            var viewItms = this.cartBasket.GetCartBasketUser(GetCurrentUser());

            return this.View(viewItms);

        }

        [Authorize]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddToCartMovie([Bind("Id", "price")] OutputMovies model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Error404Page", "Error404");
            }

            //cart user interface for displayin numberof items in card
            try
            {
                GlobalAlertMessages.StatusMessage = await this.cartBasket.AddToCartMovie(model.Id, model.price, GetCurrentUser());
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException("Somthing goes wrong with adding movie to the cart", e);
            }

            ViewData["CartCount"] = this.cartBasket.GetCartBasketUser(GetCurrentUser()).Count;

            GlobalAlertMessages.CountOfProductsInBasket = this.cartBasket.GetCartBasketUser(GetCurrentUser()).Count;

            return RedirectToAction("MovieCollection", "MovieList");
        }

        [Authorize]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddToCartBook([Bind("Id", "price")] OutputBooks model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Error404Page", "Error404");
            }

            //cart user interface for displayin numberof items in card
            try
            {
                GlobalAlertMessages.StatusMessage = await this.cartBasket.AddToCartBook(model.Id, model.price, GetCurrentUser());
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException("Somthing goes wrong with addin book to the cart", e);
            }

            ViewData["CartCount"] = this.cartBasket.GetCartBasketUser(GetCurrentUser()).Count;

            GlobalAlertMessages.CountOfProductsInBasket = this.cartBasket.GetCartBasketUser(GetCurrentUser()).Count();

            return RedirectToAction("BooksCollection", "BookList");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CartChekout(IEnumerable<ViewProducts> model, List<int> productId, List<string> type)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Error404Page", "Error404");
            }

            //Current product in cart basket
            var currentProducts = this.cartBasket.GetCartBasketUser(GetCurrentUser());

            var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            foreach (var product in currentProducts)
            {
                if (product.Type == "Movie")
                {
                    try
                    {

                        GlobalAlertMessages.StatusMessage = await this.shopService
                            .BuyMovie(user, product.Id);
                    }
                    catch (InvalidOperationException e)
                    {

                        throw new InvalidOperationException("Somthing goes wrong with shoping movie", e);
                    }
                }
                else if (product.Type == "Book")
                {
                    //To Do string message
                    try
                    {
                        GlobalAlertMessages.StatusMessage = await this.shopService
                            .BuyBook(user, product.Id);
                    }
                    catch (InvalidOperationException e)
                    {
                        throw new InvalidOperationException("Somthing goes wrong with shoping a book", e);
                    }
                }
            }

            try
            {
                await this.cartBasket.DisposeCartProducts(GetCurrentUser());
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException("Somthing goes wrong when deleting user cart bascket", e);
            }

            ViewData["CartCount"] = this.cartBasket.GetCartBasketUser(GetCurrentUser()).Count;

            GlobalAlertMessages.CountOfProductsInBasket = this.cartBasket.GetCartBasketUser(GetCurrentUser()).Count();

            return RedirectToAction("UserMovieShops", "UserShopedItems");
        }

        [Authorize]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> RemoveMovieProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                GlobalAlertMessages.StatusMessage = await this.cartBasket.RemoveMovie((int)id, GetCurrentUser());
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException("Somthing goes wrong when removing movie from user - cart", e);
            }

            //GlobalAlertMessages.MessageForStaatus = "";

            ViewData["CartCount"] = this.cartBasket.GetCartBasketUser(GetCurrentUser()).Count;

            GlobalAlertMessages.CountOfProductsInBasket = this.cartBasket.GetCartBasketUser(GetCurrentUser()).Count();

            return RedirectToAction("Cart", "Cart");
        }

        [Authorize]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> RemoveBookProduct(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error404Page", "Error404");
            }

            try
            {
                GlobalAlertMessages.StatusMessage = await this.cartBasket.RemoveBook((int)id, GetCurrentUser());
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException("Somthing goes wrong when removing book from user cart",e);
            }

            //GlobalAlertMessages.MessageForStaatus = "";

            ViewData["CartCount"] = this.cartBasket.GetCartBasketUser(GetCurrentUser()).Count;

            GlobalAlertMessages.CountOfProductsInBasket = this.cartBasket.GetCartBasketUser(GetCurrentUser()).Count();

            return RedirectToAction("Cart", "Cart");
        }

        private string GetCurrentUser()
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return user;
        }
    }
}