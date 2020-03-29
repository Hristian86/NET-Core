using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLogic.interfaces;
using BusinessLogic.OutputModels;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MBshop.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IShopItems shopService;
        private readonly ICartService cardBasket;

        public CartController(IShopItems shopeService,
            ICartService cardBasket)
        {
            this.shopService = shopeService;
            this.cardBasket = cardBasket;
        }


        public IActionResult Cart()
        {

            var productsInCart = cardBasket.GetCartBascket();

            return View(productsInCart);
        }

        
        [HttpPost]
        public IActionResult AddToCartMovie(int? id, double? price)
        {

            if (id == null && price == null)
            {
                return NotFound();
            }

            int _id = (int)id;
            double _price = (double)price;

            cardBasket.AddToCartMovie(_id,_price);


            return RedirectToAction("MovieCollection", "MovieList");
        }

        [HttpPost]
        public IActionResult AddToCartBook(int? id, double? price)
        {
            if (id == null && price == null)
            {
                return NotFound();
            }

            int _id = (int)id;
            double _price = (double)price;

            cardBasket.AddToCartBook(_id,_price);

            return RedirectToAction("BooksCollection", "BookList");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CartChekout(IEnumerable<OutputCart> model)
        {
            var currentProducts = cardBasket.GetCartBascket();

            var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            foreach (var product in currentProducts)
            {
                if (product.Type == "Movie")
                {
                    await shopService
                        .BuyMovie(user,product.Id);
                }
                else if (product.Type == "Book")
                {
                    await shopService
                        .BuyBook(user,product.Id);
                }
            }

            cardBasket.DisposeCartProducts();

            return RedirectToAction("Index", "Home");
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

            return RedirectToAction("Cart", "Cart");
        }
    }
}