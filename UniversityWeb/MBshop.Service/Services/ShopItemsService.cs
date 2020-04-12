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

        /// <summary>
        /// Function for purchasing movie and add them in to database
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="movieId"></param>
        /// <returns></returns>
        public async Task<string> BuyMovie(string userId, int movieId)
        {

            var chek = this.db.Shops.Any(x => x.MovieId == movieId && x.UserId == userId);

            if (!chek)
            {

                var movie = this.db.Movies
                .Where(x => x.Id == movieId)
                .FirstOrDefault();

                if (movie != null)
                {
                    var user = this.db.AspNetUsers
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

                        this.db.Shops.Add(purchasedItem);

                        await this.db.SaveChangesAsync();

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

            string movieTitle = this.db.Movies.Where(x => x.Id == movieId).Select(x => x.Title).FirstOrDefault();

            return $"This product {movieTitle} allready is purchased";
        }

        /// <summary>
        /// Function for purchasing book and add them in to database
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public async Task<string> BuyBook(string userId, int bookId)
        {

            var chek = this.db.Shops.Any(x => x.BooksId == bookId && x.UserId == userId);

            if (!chek)
            {

                var book = this.db.Books
                    .Where(x => x.Id == bookId)
                    .FirstOrDefault();

                if (book != null)
                {
                    var user = this.db.AspNetUsers
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

                        this.db.Shops.Add(purchasedItem);

                        await this.db.SaveChangesAsync();

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

            string bookTitle = this.db.Books.Where(x => x.Id == bookId).Select(x => x.Title).FirstOrDefault();

            return $"This product {bookTitle} allready is purchased";
        }
    }
}