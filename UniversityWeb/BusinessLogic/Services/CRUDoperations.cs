using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic.interfaces;
using DataDomain.Data.Models;

namespace BusinessLogic.Services
{
    public class CRUDoperations : ICRUDoperations
    {
        private readonly MovieRentalDBSEContext _db;

        public CRUDoperations(MovieRentalDBSEContext db)
        {
            this._db = db;
        }

        public void CreateMovieRental(string userId, int movieId)
        {

            var movie = _db.Movies.Where(x => x.Id == movieId).FirstOrDefault();
            if (movie != null)
            {

                var user = _db.AspNetUsers.Where(x => x.Id == userId).FirstOrDefault();

                Rentals rental = new Rentals
                {
                    UserId = userId,
                    MovieId = movieId,
                    Movie = movie,
                    User = user
                };

                _db.Rentals.Add(rental);

                this._db.SaveChanges();

            }
        }
    }
}
