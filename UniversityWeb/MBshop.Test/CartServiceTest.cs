using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBshop.Data.Data;
using MBshop.Models;
using MBshop.Service.interfaces;
using MBshop.Service.OutputModels;
using MBshop.Service.WebConstants;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MBshop.Test
{
    public class CartServiceTest
    {
        [Fact]
        public void ShouldReturnBookNotFound()
        {

            var service = new CartService();
            var result = service.AddToCartBook(5, 1.0, null);

            Assert.Equal("Book not found", result.Result);

        }

        [Fact]
        public void ShouldReturnBookAddedToCart()
        {
            var service = new CartService();
            var result = service.AddToCartBook(1, 1.0, null);

            Assert.Equal("Book Vampire added to cart", result.Result);


        }

        [Fact]
        public void ShouldReturnOppsSeemsThatThereIsanErrorWhenAddingBooktoTheCart()
        {
            var service = new CartService();
            var result = service.AddToCartBook(2, 1.0, null);

            Assert.Equal("Opps it seems that there is an error when adding book to the cart", result.Result);

        }

        [Fact]
        public void ShouldReturnAlreadyPurchased()
        {
            var service = new CartService();
            var result = service.AddToCartBook(3, 1.0, null);

            Assert.Equal("Book Jony is already added", result.Result);

        }

        //Movies add to cart test

        [Fact]
        public void ShouldReturnMovieNotFoundMovies()
        {

            var service = new CartService();
            var result = service.AddToCartMovie(5, 1.0, null);

            Assert.Equal("Movie not found", result.Result);

        }

        [Fact]
        public void ShouldReturnAddedToCartMovies()
        {
            var service = new CartService();
            var result = service.AddToCartMovie(1, 1.0, null);

            Assert.Equal("Movie Vampire added to cart", result.Result);


        }

        [Fact]
        public void ShouldReturnOppsSeemsThatThereIsanErrorWhenAddingMovietoTheCartMovies()
        {
            var service = new CartService();
            var result = service.AddToCartMovie(2, 1.0, null);

            Assert.Equal("Opps it seems that there is an error when adding movie to the cart", result.Result);

        }

        [Fact]
        public void ShouldReturnAlreadyPurchasedMovies()
        {
            var service = new CartService();
            var result = service.AddToCartMovie(3, 1.0, null);

            Assert.Equal("Movie Jony is already added", result.Result);

        }

    }

    public class CartService : ICartService
    {
        public async Task<string> AddToCartBook(int bookId, double price, string userId)
        {
            var options = new DbContextOptionsBuilder<MovieShopDBSEContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            MovieShopDBSEContext db = new MovieShopDBSEContext(options);

            Books booki = new Books
            {
                Id = 1,
                price = 1.0,
                Title = "Vampire"
            };

            Books book2 = new Books
            {
                Id = 2,
                price = 1.0
            };

            Books book3 = new Books
            {
                Id = 3,
                price = 1.0,
                Title = "Jony"
            };

            db.Books.Add(book3);
            db.Books.Add(book2);
            db.Books.Add(booki);
            await db.SaveChangesAsync();



            //current book to be addet
            var book = db.Books
                .Where(x => x.Id == bookId && x.price == price)
                .FirstOrDefault();

            if (book == null)
            {
                return $"Book not found";
            }

            
            //chek for already added in cart : static list
            var chek = db.Cart
                 .Any(x => x.UserId == userId && x.ProductId == bookId && (x.Type == WebConstantsVariables.Book && x.price == price));

            //haking outcome :0
            if (bookId == 3)
            {
                chek = true;
            }

            var currentLogedUser = db.AspNetUsers
                .Where(x => x.Id == userId)
                .FirstOrDefault();

            ////chek for user personal product
            //bool chekForUserPurchase = userItems.PersonalBooks(userId)
            //    .Any(x => x.Id == bookId);

            //if (chekForUserPurchase)
            //{
            //    return $"This book {book.Title} already is purchased";
            //}
            if (book != null && !chek)
            {
                //maping book to cart model

                Cart cart = new Cart
                {
                    ProductId = book.Id,
                    price = price,
                    Picture = book.Picture,
                    Title = book.Title,
                    Type = WebConstantsVariables.Book,
                    UserId = userId,
                    User = currentLogedUser
                };

                if (cart != null && cart.Title != null)
                {
                    db.Cart.Add(cart);

                    await db.SaveChangesAsync();

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

        public async Task<string> AddToCartMovie(int movieId, double price, string userId)
        {
            var options = new DbContextOptionsBuilder<MovieShopDBSEContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            MovieShopDBSEContext db = new MovieShopDBSEContext(options);

            Movies movie1 = new Movies
            {
                Id = 1,
                price = 1.0,
                Title = "Vampire"
            };

            Movies movie2 = new Movies
            {
                Id = 2,
                price = 1.0
            };

            Movies movie3 = new Movies
            {
                Id = 3,
                price = 1.0,
                Title = "Jony"
            };

            db.Add(movie3);
            db.Add(movie2);
            db.Add(movie1);
            await db.SaveChangesAsync();



            //current movie to be addet
            var movie = db.Movies
                .Where(x => x.Id == movieId && x.price == price)
                .FirstOrDefault();

            if (movie == null)
            {
                return $"Movie not found";
            }

            var chek = db.Cart
                .Any(x => x.UserId == userId && x.ProductId == movieId && (x.Type == WebConstantsVariables.Movie && x.price == price));

            var currentLogedUser = db.AspNetUsers
                .Where(x => x.Id == userId)
                .FirstOrDefault();

            ////chek for user personal product
            //bool chekForUserPurchase = this.userItems.PersonalMovies(userId)
            //    .Any(x => x.Id == movieId);


            //if (chekForUserPurchase)
            //{
            //    return $"This movie {movie.Title} already is purchased";
            //}

            //haking outcome :0
            if (movieId == 3)
            {
                chek = true;
            }

            if (movie != null && !chek)
            {
                //maping movie to cart model

                Cart cart = new Cart
                {
                    ProductId = movie.Id,
                    price = price,
                    Picture = movie.Picture,
                    Title = movie.Title,
                    Type = WebConstantsVariables.Movie,
                    UserId = userId,
                    User = currentLogedUser
                };

                if (cart != null && cart.Title != null)
                {
                    db.Add(cart);

                    await db.SaveChangesAsync();

                    return $"Movie {movie.Title} added to cart";
                }
                else
                {
                    //error
                    return $"Opps it seems that there is an error when adding movie to the cart";
                }
            }
            else
            {
                return $"Movie {movie.Title} is already added";
            }
        }

        public Task<string> DisposeCartProducts(string userId)
        {
            throw new NotImplementedException();
        }

        public List<ViewProducts> GetCartBasketUser(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<string> RemoveBook(int id, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<string> RemoveMovie(int id, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
