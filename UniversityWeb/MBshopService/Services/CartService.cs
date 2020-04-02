using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBshopService.interfaces;
using MBshopService.OutputModels;
using Data.Domain.Data;
using Db.Models;
using MBshopService.WebConstants;

namespace MBshopService.Services
{
    public class CartService : ICartService
    {
        private static List<OutputCart> carts = new List<OutputCart>();
        private readonly MovieShopDBSEContext db;

        public CartService(MovieShopDBSEContext db)
        {
            this.db = db;
        }

        public List<OutputCart> GetCartBascket()
        {
            var carty = carts.ToList();
            return carty;
        }

        public string AddToCartBook(int id, double price)
        {
            //current book to be addet
            var book = this.db.Books
                .Where(x => x.Id == id && x.price == price)
                .FirstOrDefault();

            //chek for already added
            var chek = carts
                .Any(x => x.Id == id && (x.Type == WebConstansVariables.Book && x.price == price));

            if (book != null && !chek)
            {
                //maping book to cart model
                OutputCart cart = new OutputCart
                {
                    Id = id,
                    price = price,
                    Picture = book.Picture,
                    Title = book.Title,
                    Type = WebConstansVariables.Book
                };

                if (cart != null)
                {
                    carts.Add(cart);
                    return $"Book {book.Title} added to cart";
                }
                else
                {
                    return $"Book not found";
                }
            }
            else
            {
                return $"Book {book.Title} already is added";
            }
        }

        public string AddToCartMovie(int id, double price)
        {
            //current movie to be addet
            var movie = this.db.Movies
                .Where(x => x.Id == id && x.price == price)
                .FirstOrDefault();

            //chek for already added
            var chek = carts
                .Any(x => x.Id == id && (x.Type == WebConstansVariables.Movie && x.price == price));

            if (movie != null && !chek)
            {
                //maping movie to cart model
                OutputCart cart = new OutputCart
                {
                    Id = id,
                    price = price,
                    Picture = movie.Picture,
                    Title = movie.Title,
                    Type = WebConstansVariables.Movie
                };

                if (cart != null)
                {
                    carts.Add(cart);
                    return $"Movie {movie.Title} added to cart";
                }
                else
                {
                    return $"Movie not found";
                }
            }
            else
            {
                return $"Movie {movie.Title} already is added";
            }
        }

        public void DisposeCartProducts()
        {
            carts = new List<OutputCart>();
        }

        public void RemoveMovie(int id)
        {
            var cart = carts.Where(x => x.Id == id && x.Type == WebConstansVariables.Movie).FirstOrDefault();

            carts.Remove(cart);
        }

        public void RemoveBook(int id)
        {
            var cart = carts.Where(x => x.Id == id && x.Type == WebConstansVariables.Book).FirstOrDefault();

            carts.Remove(cart);
        }
    }
}
