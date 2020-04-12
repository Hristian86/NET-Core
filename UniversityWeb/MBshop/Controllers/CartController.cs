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
        private readonly ICartService cardBasket;

        public CartController(IShopItemsService shopService,
            ICartService cardBasket)
        {
            this.shopService = shopService;
            this.cardBasket = cardBasket;
        }

        public IActionResult Cart()
        {
            return View(this.cardBasket.GetCartBascket());
        }

        [Authorize]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult AddToCartMovie([Bind("Id","price")] OutputMovies model)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            //cart user interface for displayin numberof items in card
            GlobalAlertMessages.MessageForStaatus = this.cardBasket.AddToCartMovie(model.Id,model.price, GetCurrentUser());

            GlobalAlertMessages.CountOfProductsInBasket = this.cardBasket.GetCartBascket().Count();

            return RedirectToAction("MovieCollection", "MovieList");
        }

        [Authorize]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult AddToCartBook([Bind("Id", "price")] OutputBooks model)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            //cart user interface for displayin numberof items in card
            GlobalAlertMessages.MessageForStaatus = this.cardBasket.AddToCartBook(model.Id,model.price, GetCurrentUser());

            GlobalAlertMessages.CountOfProductsInBasket = this.cardBasket.GetCartBascket().Count();

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
            var currentProducts = this.cardBasket.GetCartBascket();

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

            this.cardBasket.DisposeCartProducts();

            GlobalAlertMessages.CountOfProductsInBasket = this.cardBasket.GetCartBascket().Count();

            return RedirectToAction("UserMovieShops", "UserShopedItems");
        }

        [Authorize]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult RemoveMovieProduct(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            this.cardBasket.RemoveMovie((int)id);

            GlobalAlertMessages.MessageForStaatus = "";

            GlobalAlertMessages.CountOfProductsInBasket = this.cardBasket.GetCartBascket().Count();

            return RedirectToAction("Cart","Cart");
        }

        [Authorize]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult RemoveBookProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            this.cardBasket.RemoveBook((int)id);

            GlobalAlertMessages.MessageForStaatus = "";

            GlobalAlertMessages.CountOfProductsInBasket = this.cardBasket.GetCartBascket().Count();

            return RedirectToAction("Cart", "Cart");
        }

        private string GetCurrentUser()
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return user;
        }
    }
}