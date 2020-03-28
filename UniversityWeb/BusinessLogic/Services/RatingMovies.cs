using System;
using System.Collections.Generic;
using System.Text;
using Data.Domain.Data;
using System.Linq;
using BusinessLogic.OutputModels;
using Db.Models;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class RatingMovies
    {
        private readonly MovieShopDBSEContext db;

        public RatingMovies(MovieShopDBSEContext db)
        {
            this.db = db;
        }

        public static int OrderBy { get; set; }


        public async Task<double> RateMovie(OutputMovies model, string user)
        {
            double final = 0;

            Movies movi = this.db.Movies
                    .Where(x => x.Id == model.Id)
                    .FirstOrDefault();

            if (movi != null)
            {

                var sum = this.db.rating.Where(x => x.Movies == movi).Select(x => x.RatingMovies).ToList();

                var count = sum.Sum();

                double total = (double)count;

                //display rating 
                final = total / sum.Count();

                if (sum.Count() == 0)
                {
                    movi.Rate = 0.0;
                }
                else
                {
                    movi.Rate = final;

                    this.db.Movies.Update(movi);
                }

                

                if (user != null)
                {

                    var curUser = this.db.AspNetUsers
                        .Where(x => x.Id == user)
                        .FirstOrDefault();

                    Rating nwRate = new Rating
                    {
                        RatingMovies = model.Raiting,
                        Movies = movi,
                        UserId = user,
                        User = curUser
                    };

                    this.db.rating.Add(nwRate);

                    await this.db.SaveChangesAsync();

                    total = 0;
                }
            }
            return final;
        }


        public async Task<double> RateBook(OutputBooks model, string user)
        {
            double final = 0;

            Books book = this.db.Books
                    .Where(x => x.Id == model.Id)
                    .FirstOrDefault();

            if (book != null)
            {

                var sum = this.db.rating.Where(x => x.Books == book).Select(x => x.RatingBooks).ToList();

                var count = sum.Sum();

                double total = (double)count;

                //display rating 
                final = total / sum.Count();

                if (sum.Count() == 0)
                {
                    book.Rate = 0.0;
                }
                else
                {
                    book.Rate = final;

                    this.db.Books.Update(book);
                }


                if (user != null)
                {

                    var curUser = this.db.AspNetUsers
                        .Where(x => x.Id == user)
                        .FirstOrDefault();

                    Rating nwRate = new Rating
                    {
                        RatingBooks = model.Raiting,
                        Books = book,
                        UserId = user,
                        User = curUser
                    };

                    this.db.rating.Add(nwRate);

                    await this.db.SaveChangesAsync();

                    total = 0;
                }
            }
            return final;
        }
    }
}
