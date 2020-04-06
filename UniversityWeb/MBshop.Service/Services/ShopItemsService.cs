using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBshop.Service.interfaces;
using MBshop.Data.Data;
using MBshop.Models;

namespace MBshop.Service.Services
{
    public class ShopItemsService : IShopItemsService
    {
        private readonly MovieShopDBSEContext db;

        public ShopItemsService(MovieShopDBSEContext db)
        {
            this.db = db;
        }

        public async Task<string> BuyMovie(string userId, int movieId)
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

                        return $"Purchase successed";
                    }
                }
            }

            string movieTitle = this.db.Movies.Where(x => x.Id == movieId).Select(x => x.Title).FirstOrDefault();

            return $"This product {movieTitle} allready is purchased";
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