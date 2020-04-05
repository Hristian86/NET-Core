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
    [Authorize]
    public class CartController : Controller
    {
        private readonly IShopItems shopService;
        private readonly ICartService cardBasket;

        public CartController(IShopItems shopService,
            ICartService cardBasket)
        {
            this.shopService = shopService;
            this.cardBasket = cardBasket;
        }


        public IActionResult Cart()
        {
            return View(cardBasket.GetCartBascket());
        }

        
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult AddToCartMovie(int? id, double? price)
        {
            
            if ((id == null && price == null) || id == null || price == null)
            {
                return NotFound();
            }

            int _id = (int)id;
            double _price = (double)price;

            //cart user interface for displayin numberof items in card
            StatusForCartCount.MessageForStaatus = cardBasket.AddToCartMovie(_id,_price, GetCurrentUser());

            StatusForCartCount.CountOfProductsInBasket = cardBasket.GetCartBascket().Count();

            return RedirectToAction("MovieCollection", "MovieList");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult AddToCartBook(int? id, double? price)
        {
            if ((id == null && price == null) || id == null || price == null)
            {
                return NotFound();
            }

            int _id = (int)id;
            double _price = (double)price;

            //cart user interface for displayin numberof items in card
            StatusForCartCount.MessageForStaatus = cardBasket.AddToCartBook(_id,_price, GetCurrentUser());

            StatusForCartCount.CountOfProductsInBasket = cardBasket.GetCartBascket().Count();

            return RedirectToAction("BooksCollection", "BookList");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CartChekout(IEnumerable<ViewProducts> model)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            //Current product in cart basket
            var currentProducts = cardBasket.GetCartBascket();

            var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            foreach (var product in currentProducts)
            {
                if (product.Type == "Movie")
                {
                    StatusForCartCount.MessageForStaatus = await shopService
                        .BuyMovie(user,product.Id);
                }
                else if (product.Type == "Book")
                {
                    await shopService
                        .BuyBook(user,product.Id);
                }
            }

            cardBasket.DisposeCartProducts();

            StatusForCartCount.CountOfProductsInBasket = cardBasket.GetCartBascket().Count();

            return RedirectToAction("UserMovieShops", "UserShopedItems");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult RemoveMovieProduct(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            cardBasket.RemoveMovie((int)id);

            StatusForCartCount.MessageForStaatus = "";

            StatusForCartCount.CountOfProductsInBasket = cardBasket.GetCartBascket().Count();

            return RedirectToAction("Cart","Cart");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult RemoveBookProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            cardBasket.RemoveBook((int)id);

            StatusForCartCount.MessageForStaatus = "";

            StatusForCartCount.CountOfProductsInBasket = cardBasket.GetCartBascket().Count();

            return RedirectToAction("Cart", "Cart");
        }

        private string GetCurrentUser()
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return user;
        }
    }
}