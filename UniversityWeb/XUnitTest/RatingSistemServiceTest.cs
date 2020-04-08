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
        public void ShouldReturnMovieDoesNotExist()
        {
            var service = new RatingSistemService();

            OutputMovies movie = new OutputMovies
            {
                Id = 1,
                Raiting = 3
            };

            string result = service.RateMovie(movie,"123");

            Assert.Equal("You have rated this  movie successfully",result.ToString());
        }

        [Fact]
        public void ShouldReturnUserIsNotFound()
        {
            var service = new RatingSistemService();

            OutputMovies movie = new OutputMovies
            {
                Id = 1,
                Raiting = 3
            };

            string result = service.RateMovie(movie, "12");

            Assert.Equal("User is not found", result.ToString());
        }

        [Fact]
        public void ShouldReturnMovieDoesNotExists()
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

        [Fact]
        public void ShouldReturnInvalidRatingBelowZero()
        {
            var service = new RatingSistemService();

            OutputMovies movie = new OutputMovies
            {
                Id = 1,
                Raiting = 0
            };

            string result = service.RateMovie(movie, "123");

            Assert.Equal("This rating is invalid for ", result.ToString());
        }

        [Fact]
        public void ShouldReturnInvalidRatingAboveFive()
        {
            var service = new RatingSistemService();

            OutputMovies movie = new OutputMovies
            {
                Id = 1,
                Raiting = 6
            };

            string result = service.RateMovie(movie, "123");

            Assert.Equal("This rating is invalid for ", result.ToString());
        }

        //Books tests

        [Fact]
        public void ShouldReturnBookDoesNotExist()
        {
            var service = new RatingSistemService();

            OutputBooks book = new OutputBooks
            {
                Id = 1,
                Raiting = 3
            };

            string result = service.RateBook(book, "123");

            Assert.Equal("You have rated this  book successfully", result.ToString());
        }

        [Fact]
        public void ShouldReturnUserIsNotFoundInBooks()
        {
            var service = new RatingSistemService();

            OutputBooks book = new OutputBooks
            {
                Id = 1,
                Raiting = 3
            };

            string result = service.RateBook(book, "12");

            Assert.Equal("User is not found", result.ToString());
        }

        [Fact]
        public void ShouldReturnBookDoesNotExists()
        {
            var service = new RatingSistemService();

            OutputBooks book = new OutputBooks
            {
                Id = 2,
                Raiting = 3
            };

            string result = service.RateBook(book, "123");

            Assert.Equal("Book does not exists", result.ToString());
        }

        [Fact]
        public void ShouldReturnInvalidRatingBelowZeroInBooks()
        {
            var service = new RatingSistemService();

            OutputBooks book = new OutputBooks
            {
                Id = 1,
                Raiting = 0
            };

            string result = service.RateBook(book, "123");

            Assert.Equal("This rating is invalid for ", result.ToString());
        }

        [Fact]
        public void ShouldReturnInvalidRatingAboveFiveInBooks()
        {
            var service = new RatingSistemService();

            OutputBooks book = new OutputBooks
            {
                Id = 1,
                Raiting = 6
            };

            string result = service.RateBook(book, "123");

            Assert.Equal("This rating is invalid for ", result.ToString());
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
                    Id = "123"
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

                    //changes
                    bool checkUser = db.AspNetUsers.Any(x => x.Id == userId);

                    if (userId != null && checkUser)
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

                        //changes
                        return $"You have rated this {movi.Title} movie successfully";
                    }
                    else
                    {
                        //changes
                        return $"User is not found";
                    }
                }
                else
                {
                    //changes
                    return $"Movie does not exists";
                }
            }

            public string RateBook(OutputBooks model, string userId)
            {
                var options = new DbContextOptionsBuilder<MovieShopDBSEContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

                var db = new MovieShopDBSEContext(options);

                var addBook = new Books
                {
                    Id = 1
                };
                db.Books.Add(addBook);

                var cuser = new AspNetUsers
                {
                    Id = "123"
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

                //get current rated book
                Books book = db.Books
                        .Where(x => x.Id == model.Id)
                        .FirstOrDefault();

                if (book != null)
                {
                    //get collection of ratngs in numbers
                    var sum = db.rating.Where(x => x.Books == book).Select(x => x.RatingBooks).ToList();

                    var count = sum.Sum();

                    int counts = sum.Count();

                    if (model.Raiting < 1 || model.Raiting > 5)
                    {
                        return $"This rating is invalid for {book.Title}";
                    }

                    double total = (double)count + (double)model.Raiting;

                    //display rating
                    final = total / (counts + 1);

                    if (sum.Count() == 0)
                    {
                        //updating current rated book in database
                        book.Rate = model.Raiting;
                        db.Books.Update(book);
                        db.SaveChanges();
                    }
                    else
                    {
                        //updating current rated book in database
                        book.Rate = final;
                        db.Books.Update(book);
                        db.SaveChanges();
                    }

                    //chek for loged existing user in database
                    bool checkUser = db.AspNetUsers.Any(x => x.Id == userId);

                    if (userId != null && checkUser)
                    {
                        //get current user
                        var curUser = db.AspNetUsers
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

                        db.rating.Add(nwRate);

                        db.SaveChanges();

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
        }

    }
}
