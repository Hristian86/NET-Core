using System;
using System.Collections.Generic;
using System.Text;
using MBshop.Data.Data;
using System.Linq;
using MBshop.Service.OutputModels;
using MBshop.Models;
using System.Threading.Tasks;
using MBshop.Service.interfaces;

namespace MBshop.Service.Services
{
    public class RatingSistemService : IRatingSistemService
    {
        private readonly MovieShopDBSEContext db;
        private double final = 0;

        public RatingSistemService(MovieShopDBSEContext db)
        {
            this.db = db;
        }

        public void GetUserRate(string userId)
        {
            //geting current user
            var curUser = this.db.AspNetUsers
                .Where(x => x.Id == userId)
                .FirstOrDefault();

            var movie = this.db.Movies
                .Where(x => x.Id == 1)
                .FirstOrDefault();

            var userRates = this.db.Rating
                .Where(x => x.User == curUser && x.Movies == movie)
                .Select(x => x.RatingMovies)
                .ToList();

            string userName = curUser.UserName;

        }

        /// <summary>
        /// Rate movie
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<string> RateMovie(OutputMovies model, string userId)
        {
            this.final = 0;

            //get current rated movie
            Movies movi = this.db.Movies
                    .Where(x => x.Id == model.Id)
                    .FirstOrDefault();

            if (movi != null)
            {
                //get collection of ratngs in numbers
                var sum = this.db.Rating.Where(x => x.Movies == movi).Select(x => x.RatingMovies).ToList();

                var count = sum.Sum();

                int counts = sum.Count();

                if (model.Raiting < 1 || model.Raiting > 5)
                {
                    return $"This rating is invalid for {movi.Title}";
                }

                double total = (double)count + (double)model.Raiting;

                //display rating
                this.final = total / (counts + 1);

                if (sum.Count() == 0)
                {
                    //updating current rated movie in database
                    movi.Rate = model.Raiting;
                    await UpdateMovie(movi);
                }
                else
                {
                    //updating current rated movie in database
                    movi.Rate = this.final;
                    await UpdateMovie(movi);
                }

                //chek for loged existing user in database
                bool checkUser = db.AspNetUsers.Any(x => x.Id == userId);

                if (userId != null && checkUser)
                {
                    //geting current user
                    var curUser = this.db.AspNetUsers
                        .Where(x => x.Id == userId)
                        .FirstOrDefault();

                    //creating new object for database with added values
                    Rating nwRate = new Rating
                    {
                        RatingMovies = model.Raiting,
                        Movies = movi,
                        UserId = userId,
                        User = curUser
                    };

                    this.db.Rating.Add(nwRate);

                    await this.db.SaveChangesAsync();
                    

                    total = 0;

                    //on successfully rated movie
                    return $"You have rated this {movi.Title} movie successfully";
                }
                else
                {
                    //if user is not loged in
                    return $"User is not found";
                }
            }
            else
            {
                //if movie does not exists in database
                return $"Movie does not exists";
            }
            
        }

        /// <summary>
        /// Rate book
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<string> RateBook(OutputBooks model, string userId)
        {
            this.final = 0;

            //get current rated book
            Books book = this.db.Books
                    .Where(x => x.Id == model.Id)
                    .FirstOrDefault();

            if (book != null)
            {
                //get collection of ratngs in numbers
                var sum = this.db.Rating.Where(x => x.Books == book).Select(x => x.RatingBooks).ToList();

                var count = sum.Sum();

                int counts = sum.Count();

                if (model.Raiting < 1 || model.Raiting > 5)
                {
                    return $"This rating is invalid for {book.Title}";
                }

                double total = (double)count + (double)model.Raiting;

                //display rating
                this.final = total / (counts + 1);

                if (sum.Count() == 0)
                {
                    //updating current rated book in database
                    book.Rate = model.Raiting;
                    await UpdateBook(book);
                }
                else
                {
                    //updating current rated book in database
                    book.Rate = this.final;
                    await UpdateBook(book);
                }

                //chek for loged existing user in database
                bool checkUser = this.db.AspNetUsers.Any(x => x.Id == userId);

                if (userId != null && checkUser)
                {
                    //get current user
                    var curUser = this.db.AspNetUsers
                        .Where(x => x.Id == userId)
                        .FirstOrDefault();

                    //creating new object for database with added values
                    Rating nwRate = new Rating
                    {
                        RatingBooks = model.Raiting,
                        Books = book,
                        UserId = userId,
                        User = curUser
                    };

                    this.db.Rating.Add(nwRate);

                    await this.db.SaveChangesAsync();

                    total = 0;

                    //on successfully rated book
                    return $"You have rated this {book.Title} book successfully";
                }
                else
                {
                    //if user is not loged in
                    return $"User is not found";
                }
            }
            else
            {
                //if book does not exists in database
                return $"Book does not exists";
            }
        }

        private async Task UpdateMovie(Movies movi)
        {
            this.db.Movies.Update(movi);
            await this.db.SaveChangesAsync();
        }

        private async Task UpdateBook(Books book)
        {
            this.db.Books.Update(book);
            await this.db.SaveChangesAsync();
        }
    }
}