using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBshop.Service.interfaces;
using MBshop.Service.OutputModels;
using MBshop.Data.Data;
using MBshop.Models;
using MBshop.Service.WebConstants;
using System.Threading.Tasks;

namespace MBshop.Service.Services
{
    public class CartService : ICartService
    {

        private readonly MovieShopDBSEContext db;
        private readonly IUserShopedProductsService userItems;

        public CartService(MovieShopDBSEContext db,
            IUserShopedProductsService userItems)
        {
            this.db = db;
            this.userItems = userItems;
        }

        /// <summary>
        /// Retrive all products in the basket
        /// </summary>
        /// <returns></returns>
        public List<ViewProducts> GetCartBasketUser(string userId)
        {
            var carties = this.db.Cart
                .Where(x => x.UserId == userId)
                .Select(item => new ViewProducts
                {
                    
                    Id = (int)item.MovieId,
                    price = item.price,
                    Picture = item.Picture,
                    Title = item.Title,
                    Type = item.Type,

                }).ToList();

            return carties;
        }

        /// <summary>
        /// Add book to cart
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="price"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<string> AddToCartBook(int bookId, double price, string userId)
        {
            //current book to be addet
            var book = this.db.Books
                .Where(x => x.Id == bookId && x.price == price)
                .FirstOrDefault();

            if (book == null)
            {
                return $"Book not found";
            }

            //chek for already added in cart : static list
            var chek = this.db.Cart
                 .Any(x => x.UserId == userId && x.BookId == bookId && (x.Type == WebConstansVariables.Book && x.price == price));

            var currentLogedUser = this.db.AspNetUsers.Where(x => x.Id == userId).FirstOrDefault();

            //chek for user personal product
            bool chekForUserPurchase = this.userItems.PersonalBooks(userId).Any(x => x.Id == bookId);

            if (chekForUserPurchase)
            {
                return $"This book {book.Title} already is purchased";
            }
            else if (book != null && !chek)
            {
                //maping book to cart model
                
                Cart cart = new Cart
                {
                    MovieId = book.Id,
                    price = price,
                    Picture = book.Picture,
                    Title = book.Title,
                    Type = WebConstansVariables.Book,
                    UserId = userId,
                    User = currentLogedUser
                };

                if (cart != null && cart.Title != null)
                {
                    this.db.Cart.Add(cart);

                    await this.db.SaveChangesAsync();

                    return $"Book {book.Title} added to cart";
                }
                else
                {
                    //error
                    return $"Opps it seems that there is an error when adding book to the cart";
                }
            }
            else
            {
                return $"Book {book.Title} is already added";
            }
        }

        /// <summary>
        /// Add movie to cart
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="price"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<string> AddToCartMovie(int movieId, double price, string userId)
        {
            //current movie to be addet
            var movie = this.db.Movies
                .Where(x => x.Id == movieId && x.price == price)
                .FirstOrDefault();

            if (movie == null)
            {
                return $"Movie not found";
            }

            var chek = this.db.Cart
                .Any(x => x.UserId == userId && x.MovieId == movieId && (x.Type == WebConstansVariables.Movie && x.price == price));

            var currentLogedUser = this.db.AspNetUsers.Where(x => x.Id == userId).FirstOrDefault();

            //chek for user personal product
            bool chekForUserPurchase = this.userItems.PersonalMovies(userId).Any(x => x.Id == movieId);


            if (chekForUserPurchase)
            {
                return $"This movie {movie.Title} already is purchased";
            }

            if (movie != null && !chek)
            {
                //maping movie to cart model

                Cart cart = new Cart
                {
                    MovieId = movie.Id,
                    price = price,
                    Picture = movie.Picture,
                    Title = movie.Title,
                    Type = WebConstansVariables.Movie,
                    UserId = userId,
                    User = currentLogedUser
                };

                if (cart != null && cart.Title != null)
                {
                    this.db.Add(cart);

                    await this.db.SaveChangesAsync();

                    return $"Movie {movie.Title} added to cart";
                }
                else
                {
                    //error
                    return $"Opps it seems that there is an error when adding book to the cart";
                }
            }
            else
            {
                return $"Movie {movie.Title} is already added";
            }
        }

        /// <summary>
        /// Remove items from cart when chekout is pressed
        /// </summary>
        public async Task<string> DisposeCartProducts(string userId)
        {
            var products = this.db.Cart.Where(x => x.UserId == userId).ToList();

            this.db.Cart.RemoveRange(products);

            await this.db.SaveChangesAsync();

            return $"";
        }

        /// <summary>
        /// Remove product : Type = Movie
        /// </summary>
        /// <param name="id"></param>
        public async Task<string> RemoveMovie(int id, string userId)
        {
            var cart = this.db.Cart.Where(x => x.UserId == userId && (x.MovieId == id && x.Type == WebConstansVariables.Movie)).FirstOrDefault();

            this.db.Cart.Remove(cart);

            await this.db.SaveChangesAsync();

            return "";
        }

        /// <summary>
        /// Remove product : Type = Book
        /// </summary>
        /// <param name="id"></param>
        public async Task<string> RemoveBook(int id, string userId)
        {
            var cart = this.db.Cart.Where(x => x.UserId == userId && (x.MovieId == id && x.Type == WebConstansVariables.Book)).FirstOrDefault();

            this.db.Cart.Remove(cart);

            await this.db.SaveChangesAsync();

            return "";
        }
    }
}
