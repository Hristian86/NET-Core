﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBshop.Service.interfaces;
using MBshop.Service.OutputModels;
using MBshop.Data.Data;
using MBshop.Models;
using MBshop.Service.WebConstants;

namespace MBshop.Service.Services
{
    public class CartService : ICartService
    {
        private static List<ViewProducts> carts = new List<ViewProducts>();

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
        public List<ViewProducts> GetCartBascket()
        {
            var carty = carts.ToList();
            return carty;
        }

        /// <summary>
        /// Add book to cart
        /// </summary>
        /// <param name="id"></param>
        /// <param name="price"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string AddToCartBook(int id, double price,string userId)
        {
            //current book to be addet
            var book = this.db.Books
                .Where(x => x.Id == id && x.price == price)
                .FirstOrDefault();

            if (book == null)
            {
                return $"Book not found";
            }

            //chek for already added in cart : static list
            bool chek = carts
                .Any(x => x.Id == id && (x.Type == WebConstansVariables.Book && x.price == price));

            //chek for user personal product
            bool chekForUserPurchase = this.userItems.PersonalBooks(userId).Any(x => x.Id == id);

            if (chekForUserPurchase)
            {
                return $"This book {book.Title} already is purchased";
            }
            else if (book != null && !chek)
            {
                //maping book to cart model
                ViewProducts cart = new ViewProducts
                {
                    Id = id,
                    price = price,
                    Picture = book.Picture,
                    Title = book.Title,
                    Type = WebConstansVariables.Book
                };

                if (cart != null && cart.Title != null)
                {
                    carts.Add(cart);
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
        /// <param name="id"></param>
        /// <param name="price"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string AddToCartMovie(int id, double price, string userId)
        {
            //current movie to be addet
            var movie = this.db.Movies
                .Where(x => x.Id == id && x.price == price)
                .FirstOrDefault();

            if (movie == null)
            {
                return $"Movie not found";
            }

            //chek for already added to cart
            var chek = carts
                .Any(x => x.Id == id && (x.Type == WebConstansVariables.Movie && x.price == price));

            //chek for user personal product
            bool chekForUserPurchase = this.userItems.PersonalMovies(userId).Any(x => x.Id == id);

            if (chekForUserPurchase)
            {
                return $"This movie {movie.Title} already is purchased";
            }

            if (movie != null && !chek)
            {
                //maping movie to cart model
                ViewProducts cart = new ViewProducts
                {
                    Id = id,
                    price = price,
                    Picture = movie.Picture,
                    Title = movie.Title,
                    Type = WebConstansVariables.Movie
                };

                if (cart != null && cart.Title != null)
                {
                    carts.Add(cart);
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
        public void DisposeCartProducts()
        {
            carts = new List<ViewProducts>();
        }

        /// <summary>
        /// Remove product : Type = Movie
        /// </summary>
        /// <param name="id"></param>
        public void RemoveMovie(int id)
        {
            var cart = carts.Where(x => x.Id == id && x.Type == WebConstansVariables.Movie).FirstOrDefault();

            carts.Remove(cart);
        }

        /// <summary>
        /// Remove product : Type = Book
        /// </summary>
        /// <param name="id"></param>
        public void RemoveBook(int id)
        {
            var cart = carts.Where(x => x.Id == id && x.Type == WebConstansVariables.Book).FirstOrDefault();

            carts.Remove(cart);
        }
    }
}
