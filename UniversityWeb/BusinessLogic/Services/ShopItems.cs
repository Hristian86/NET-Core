using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.interfaces;
using Data.Domain.Data;
using Db.Models;

namespace BusinessLogic.Services
{
    public class ShopItems : IShopItems
    {
        private readonly MovieShopDBSEContext db;

        public ShopItems(MovieShopDBSEContext db)
        {
            this.db = db;
        }

        public async Task BuyMovie(string userId, int movieId)
        {

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

                        await this.db.SaveChangesAsync();

                    }
                }
            }
        }

        public async Task BuyBook(string userId, int bookId)
        {

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

                        await this.db.SaveChangesAsync();

                    }
                }
            }
        }
    }
}