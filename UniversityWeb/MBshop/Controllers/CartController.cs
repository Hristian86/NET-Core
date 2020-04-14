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

        public IActionResult Cart()
        {
            var viewItms = this.cartBasket.GetCartBasketUser(GetCurrentUser());

            return this.View(viewItms);
            //return View(this.cardBasket.GetCartBascket());
        }

        [Authorize]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddToCartMovie([Bind("Id","price")] OutputMovies model)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            //cart user interface for displayin numberof items in card
            GlobalAlertMessages.MessageForStaatus = await this.cartBasket.AddToCartMovie(model.Id,model.price, GetCurrentUser());

            GlobalAlertMessages.CountOfProductsInBasket = this.cartBasket.GetCartBasketUser(GetCurrentUser()).Count();

            return RedirectToAction("MovieCollection", "MovieList");
        }

        [Authorize]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddToCartBook([Bind("Id", "price")] OutputBooks model)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            //cart user interface for displayin numberof items in card
            GlobalAlertMessages.MessageForStaatus = await this.cartBasket.AddToCartBook(model.Id,model.price, GetCurrentUser());

            GlobalAlertMessages.CountOfProductsInBasket = this.cartBasket.GetCartBasketUser(GetCurrentUser()).Count();

            return RedirectToAction("BooksCollection", "BookList");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CartChekout(IEnumerable<ViewProducts> model,List<int> productId, List<string> type)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            //Current product in cart basket
            var currentProducts = this.cartBasket.GetCartBasketUser(GetCurrentUser());

            var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            foreach (var product in currentProducts)
            {
                if (product.Type == "Movie")
                {
                    GlobalAlertMessages.MessageForStaatus = await this.shopService
                        .BuyMovie(user,product.Id);
                }
                else if (product.Type == "Book")
                {
                    //To Do string message
                    GlobalAlertMessages.MessageForStaatus = await this.shopService
                        .BuyBook(user,product.Id);
                }
            }

            await this.cartBasket.DisposeCartProducts(GetCurrentUser());

            GlobalAlertMessages.CountOfProductsInBasket = this.cartBasket.GetCartBasketUser(GetCurrentUser()).Count();

            return RedirectToAction("UserMovieShops", "UserShopedItems");
        }

        [Authorize]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> RemoveMovieProduct(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            GlobalAlertMessages.MessageForStaatus = await this.cartBasket.RemoveMovie((int)id,GetCurrentUser());

            GlobalAlertMessages.MessageForStaatus = "";

            GlobalAlertMessages.CountOfProductsInBasket = this.cartBasket.GetCartBasketUser(GetCurrentUser()).Count();

            return RedirectToAction("Cart","Cart");
        }

        [Authorize]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> RemoveBookProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await this.cartBasket.RemoveBook((int)id,GetCurrentUser());

            GlobalAlertMessages.MessageForStaatus = "";

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