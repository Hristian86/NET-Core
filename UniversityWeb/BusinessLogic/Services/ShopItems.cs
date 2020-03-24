using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic.interfaces;
using Data.Domain.Data;
using Db.Models;

namespace BusinessLogic.Services
{
    public class ShopItems : IShopItems
    {
        private readonly MovieShopDBSEContext _db;

        public ShopItems(MovieShopDBSEContext db)
        {
            this._db = db;
        }

        public void BuyMovie(string userId, int movieId)
        {

            var userMs = _db.Shops
                .Where(x => x.UserId == userId)
                .Select(x => x.Movie)
                .ToList();

            var chek = userMs.Any(x => x.Id == movieId);

            if (!chek)
            {

                var movie = _db.Movies
                .Where(x => x.Id == movieId)
                .FirstOrDefault();

                if (movie != null)
                {
                    var user = _db.AspNetUsers
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

                        _db.Shops.Add(purchasedItem);

                        this._db.SaveChanges();

                    }
                }
            }
        }
    }
}