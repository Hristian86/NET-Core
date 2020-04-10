using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBshop.Data.Data;
using MBshop.Models;
using MBshop.Service.interfaces;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MBshop.Test
{
    public class ShopItemsServiceTest
    {
        private string userId = "eec398cd-c542-4c6c-b90c-9689b6ab8ad7";

        [Fact]
        public void ShouldReturnUserDoesNotExistsInMoviesMessage()
        {
            var service = new ShopItemsService();

            //User does not exists
            string resultUserNotFound = service.BuyMovie("asd", 1).Result;

            Assert.Equal("User does not exists", resultUserNotFound);
        }

        [Fact]
        public void ShouldReturnMovieDoesNotExistsMessage()
        {
            var service = new ShopItemsService();

            string resultMovieNotFound = service.BuyMovie(this.userId, 2).Result;
            Assert.Equal("Movie does not exists", resultMovieNotFound);
        }

        [Fact]
        public void ShouldReturnMovieProductIsAlreadyPurchasedMessage()
        {
            var service = new ShopItemsService();

            string testName = "Jonh";
            string resultMovieAlreadyPurchased = service.BuyMovie(testName, 3).Result;
            Assert.Equal("This product  allready is purchased", resultMovieAlreadyPurchased);
        }

        [Fact]
        public void ShouldBeAddedMovieProductsToDatabase()
        {

            var service = new ShopItemsService();

            string result = service.BuyMovie(this.userId, 1).Result;
            Assert.Equal("Purchase successed", result);
        }

        //books
        [Fact]
        public void ShouldReturnUserDoesNotExistsInBooksMessage()
        {
            var service = new ShopItemsService();

            //User does not exists
            string resultUserNotFound = service.BuyBook("asd", 1).Result;

            Assert.Equal("User does not exists", resultUserNotFound);
        }

        [Fact]
        public void ShouldReturnBookDoesNotExistsMessage()
        {
            var service = new ShopItemsService();

            string resultMovieNotFound = service.BuyBook(this.userId, 2).Result;
            Assert.Equal("Book does not exists", resultMovieNotFound);
        }

        [Fact]
        public void ShouldReturnBookProductIsAlreadyPurchasedMessage()
        {
            var service = new ShopItemsService();

            string testName = "Jonh";
            string resultMovieAlreadyPurchased = service.BuyBook(testName, 3).Result;
            Assert.Equal("This product  allready is purchased", resultMovieAlreadyPurchased);
        }

        [Fact]
        public void ShouldBeAddedBookProductsToDatabase()
        {

            var service = new ShopItemsService();

            string result = service.BuyBook(this.userId, 1).Result;
            Assert.Equal("Purchase successed", result);
        }

        public class ShopItemsService : IShopItemsService
        {
            public async Task<string> BuyBook(string userId, int bookId)
            {
                var options = new DbContextOptionsBuilder<MovieShopDBSEContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

                var db = new MovieShopDBSEContext(options);

                Books booki = new Books
                {
                    Id = 1
                };

                db.Books.Add(booki);
                var cuser = new AspNetUsers
                {
                    Id = "eec398cd-c542-4c6c-b90c-9689b6ab8ad7"
                };
                db.AspNetUsers.Add(cuser);
                await db.SaveChangesAsync();

                Shops shop = new Shops
                {
                    BooksId = 3,
                    UserId = "Jonh"
                };
                db.Shops.Add(shop);
                await db.SaveChangesAsync();

                var chek = db.Shops.Any(x => x.BooksId == bookId && x.UserId == userId);

                if (!chek)
                {

                    var book = db.Books
                        .Where(x => x.Id == bookId)
                        .FirstOrDefault();

                    if (book != null)
                    {
                        var user = db.AspNetUsers
                            .Where(x => x.Id == userId)
                            .FirstOrDefault();

                        if (user != null)
                        {

                            Shops purchasedItem = new Shops
                            {
                                UserId = userId,
                                BooksId = bookId,
                                Books = book,
                                User = user
                            };

                            db.Shops.Add(purchasedItem);

                            await db.SaveChangesAsync();

                            return $"Purchase successed";
                        }
                        else
                        {
                            return $"User does not exists";
                        }
                    }
                    else
                    {
                        return $"Book does not exists";
                    }
                }

                string bookTitle = db.Books.Where(x => x.Id == bookId).Select(x => x.Title).FirstOrDefault();

                return $"This product {bookTitle} allready is purchased";
            }

            public async Task<string> BuyMovie(string userId, int movieId)
            {
                var options = new DbContextOptionsBuilder<MovieShopDBSEContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

                var db = new MovieShopDBSEContext(options);

                var movi = new Movies
                {
                    Id = 1
                };

                db.Movies.Add(movi);
                var cuser = new AspNetUsers
                {
                    Id = "eec398cd-c542-4c6c-b90c-9689b6ab8ad7"
                };
                db.AspNetUsers.Add(cuser);
                await db.SaveChangesAsync();

                Shops shop = new Shops
                {
                    MovieId = 3,
                    UserId = "Jonh"
                };
                db.Shops.Add(shop);
                await db.SaveChangesAsync();


                var chek = db.Shops.Any(x => x.MovieId == movieId && x.UserId == userId);

                if (!chek)
                {

                    var movie = db.Movies
                    .Where(x => x.Id == movieId)
                    .FirstOrDefault();

                    if (movie != null)
                    {
                        var user = db.AspNetUsers
                            .Where(x => x.Id == userId)
                            .FirstOrDefault();

                        if (user != null)
                        {

                            Shops purchasedItem = new Shops
                            {
                                UserId = userId,
                                MovieId = movieId,
                                Movie = movie,
                                User = user
                            };

                            db.Shops.Add(purchasedItem);

                            await db.SaveChangesAsync();

                            return $"Purchase successed";
                        }
                        else
                        {
                            return $"User does not exists";
                        }
                    }
                    else
                    {
                        return $"Movie does not exists";
                    }
                }

                string movieTitle = db.Movies.Where(x => x.Id == movieId).Select(x => x.Title).FirstOrDefault();

                return $"This product {movieTitle} allready is purchased";
            }
        }
    }
}