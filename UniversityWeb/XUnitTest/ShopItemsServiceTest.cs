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
        public void ShouldReturnUserDoesNotExistsMessage()
        {
            var service = new ShopItemsService();

            //User does not exists
            string resultUserNotFound = service.BuyMovie("asd", 1);
            Assert.Equal("User does not exists", resultUserNotFound);
        }

        [Fact]
        public void ShouldReturnMovieDoesNotExistsMessage()
        {
            var service = new ShopItemsService();

            string resultMovieNotFound = service.BuyMovie(this.userId, 2);
            Assert.Equal("Movie does not exists", resultMovieNotFound.ToString());
        }

        [Fact]
        public void ShouldReturnTheProductIsAlreadyPurchasedMessage()
        {
            var service = new ShopItemsService();

            string testName = "Jonh";
            string resultMovieAlreadyPurchased = service.BuyMovie(testName, 3);
            Assert.Equal("This product  allready is purchased", resultMovieAlreadyPurchased.ToString());
        }

        [Fact]
        public void ShouldBeAddProductsToDatabase()
        {

            var service = new ShopItemsService();

            string result = service.BuyMovie(this.userId, 1);
            Assert.Equal("Purchase successed", result.ToString());
        }

        public class ShopItemsService
        {
            public Task BuyBook(string userId, int bookId)
            {
                throw new NotImplementedException();
            }

            public string BuyMovie(string userId, int movieId)
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
                db.SaveChanges();

                Shops shop = new Shops
                {
                    MovieId = 3,
                    UserId = "Jonh"
                };
                db.Shops.Add(shop);
                db.SaveChanges();

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

                            db.SaveChanges();

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
