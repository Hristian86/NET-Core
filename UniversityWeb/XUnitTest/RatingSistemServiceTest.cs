using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBshop.Data.Data;
using MBshop.Models;
using MBshop.Service.interfaces;
using MBshop.Service.OutputModels;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MBshop.Test
{
    public class RatingSistemServiceTest
    {
        [Fact]
        public void ShouldReturnMovieDoesNotExists()
        {
            var service = new RatingSistemService();

            OutputMovies movie = new OutputMovies
            {
                Id = 1,
                Raiting = 3
            };

            string result = service.RateMovie(movie,"123");

            Assert.Equal("Movie does not exists",result.ToString());
        }

        [Fact]
        public void asd()
        {
            var service = new RatingSistemService();

            OutputMovies movie = new OutputMovies
            {
                Id = 2,
                Raiting = 3
            };

            string result = service.RateMovie(movie, "123");

            Assert.Equal("Movie does not exists", result.ToString());
        }

        public class RatingSistemService
        {
            public string RateMovie(OutputMovies model, string userId)
            {
                var options = new DbContextOptionsBuilder<MovieShopDBSEContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

                var db = new MovieShopDBSEContext(options);

                var addMovie = new Movies
                {
                    Id = 1
                };
                db.Movies.Add(addMovie);

                var cuser = new AspNetUsers
                {
                    Id = "ee"
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


                double final = 0;

                //get current rated movie
                Movies movi = db.Movies
                        .Where(x => x.Id == model.Id)
                        .FirstOrDefault();

                if (movi != null)
                {
                    //get collection of ratngs in numbers
                    var sum = db.rating.Where(x => x.Movies == movi).Select(x => x.RatingMovies).ToList();

                    var count = sum.Sum();

                    int counts = sum.Count();

                    if (model.Raiting < 1 || model.Raiting > 5)
                    {
                        return $"This rating is invalid for {movi.Title}";
                    }

                    double total = (double)count + (double)model.Raiting;

                    //display rating
                    final = total / (counts + 1);

                    if (sum.Count() == 0)
                    {
                        //updating current rated movie in database
                        movi.Rate = model.Raiting;
                        db.Movies.Update(movi);
                        db.SaveChanges();
                    }
                    if (sum.Count() != 0)
                    {
                        //updating current rated movie in database
                        movi.Rate = final;
                        db.Movies.Update(movi);
                        db.SaveChanges();
                    }

                    if (userId != null)
                    {
                        //geting current user
                        var curUser = db.AspNetUsers
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

                        db.rating.Add(nwRate);

                        db.SaveChanges();


                        total = 0;

                        return $"You have rated this {movi.Title} movie succesfuly";
                    }
                    else
                    {
                        return $"User is not found";
                    }
                }
                else
                {
                    return $"Movie does not exists";
                }
            }

            public Task<double> RateBook(OutputBooks model, string user)
            {
                throw new NotImplementedException();
            }
        }

    }
}
