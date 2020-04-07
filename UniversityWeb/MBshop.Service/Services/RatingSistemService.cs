﻿using System;
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

        public async Task<string> RateMovie(OutputMovies model, string user)
        {
            this.final = 0;

            //get current rated movie
            Movies movi = this.db.Movies
                    .Where(x => x.Id == model.Id)
                    .FirstOrDefault();

            if (movi != null)
            {
                //get collection of ratngs in numbers
                var sum = this.db.rating.Where(x => x.Movies == movi).Select(x => x.RatingMovies).ToList();

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
                if (sum.Count() != 0)
                {
                    //updating current rated movie in database
                    movi.Rate = this.final;
                    await UpdateMovie(movi);
                }

                if (user != null)
                {
                    //geting current user
                    var curUser = this.db.AspNetUsers
                        .Where(x => x.Id == user)
                        .FirstOrDefault();

                    //creating new object for database with added values
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
            return $"You have rated this {movi.Title} movie succesfuly";
        }

        

        public async Task<double> RateBook(OutputBooks model, string user)
        {
            this.final = 0;

            //get current rated book
            Books book = this.db.Books
                    .Where(x => x.Id == model.Id)
                    .FirstOrDefault();

            if (book != null)
            {
                //get collection of ratngs in numbers
                var sum = this.db.rating.Where(x => x.Books == book).Select(x => x.RatingBooks).ToList();

                var count = sum.Sum();

                int counts = sum.Count();

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


                if (user != null)
                {
                    //get current user
                    var curUser = this.db.AspNetUsers
                        .Where(x => x.Id == user)
                        .FirstOrDefault();

                    //creating new object for database with added values
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
            return this.final;
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