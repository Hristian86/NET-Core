using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic.interfaces;
using DataDomain.Data.Models;

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

            var movie = _db.Movies.Where(x => x.Id == movieId).FirstOrDefault();
            if (movie != null)
            {

                var user = _db.AspNetUsers.Where(x => x.Id == userId).FirstOrDefault();

                Shops rental = new Shops
                {
                    UserId = userId,
                    MovieId = movieId,
                    Movie = movie,
                    User = user
                };

                _db.Shops.Add(rental);

                this._db.SaveChanges();

            }
        }
    }
}
