using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBshop.Data.Data;
using MBshop.Models;
using MBshop.Service.interfaces;
using MBshop.Service.OutputModels;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MBshop.Test
{
    public class UserShopedProductsServiceTest
    {
        private string userId = "eec398cd-c542-4c6c-b90c-9689b6ab8ad7";

        [Fact]
        public void GetAllPurchasedBooksProducts()
        {
            var service = new UserShopedProductsService();

            var resultMovies = service.PersonalMovies(this.userId);

            var expectedMoviesData = GetActualMoviesData();

            Assert.Equal(expectedMoviesData.Count, resultMovies.Count);

            foreach (var actualData in resultMovies)
            {
                Assert.True(expectedMoviesData.Any(movie => actualData.Id == movie.Id), "The id is not matching");
            }
        }

        [Fact]
        public void GetAllPurchasedMovieProducts()
        {

            var service = new UserShopedProductsService();

            var resultBooks = service.PersonalBooks(this.userId);
            var expectedBooksData = GetActualBooksData();
            Assert.Equal(expectedBooksData.Count,resultBooks.Count);
            foreach (var actualData in resultBooks)
            {
                Assert.True(expectedBooksData.Any(book => actualData.Id == book.Id),"The id is not matching");
            }

        }

        private List<OutputMovies> GetActualMoviesData()
        {
            var movieses = new List<OutputMovies>();

            Shops shops = new Shops
            {
                Id = 1,
                BooksId = 1,
                UserId = userId
            };

            OutputMovies movies = new OutputMovies
            {
                Id = 1,
            };

            movieses.Add(movies);


            return movieses;
        }

        private List<OutputBooks> GetActualBooksData()
        {
            var bookses = new List<OutputBooks>();

            Shops shops = new Shops
            {
                Id = 1,
                BooksId = 1,
                UserId = userId
            };

            OutputBooks booki = new OutputBooks
            {
                Id = 1,
                UserId = userId
            };

            bookses.Add(booki);


            return bookses;
        }

        public class UserShopedProductsService : IUserShopedProductsService
        {
            public List<OutputBooks> PersonalBooks(string userId)
            {
                var options = new DbContextOptionsBuilder<MovieShopDBSEContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

                var db = new MovieShopDBSEContext(options);

                Shops shops = new Shops
                {
                    Id = 1,
                    BooksId = 1,
                    UserId = userId
                };

                Books booki = new Books
                {
                    Id = 1,
                };


                db.Books.Add(booki);
                db.SaveChanges();

                db.Shops.Add(shops);
                db.SaveChanges();


                var disp = new List<OutputBooks>();

                //getting personal collection of books for current user
                var userBooks = db.Shops
                    .Where(x => x.UserId == userId)
                    .Select(x => x.Books)
                    .ToList();

                //mapping to output model
                foreach (var itemBook in userBooks)
                {
                    if (itemBook != null)
                    {

                        OutputBooks book = new OutputBooks
                        {
                            Id = itemBook.Id,
                            Title = itemBook.Title,
                            Author = itemBook.Author,
                            Genre = itemBook.Genre,
                            Picture = itemBook.Picture,
                            Discount = itemBook.Discount,
                            price = itemBook.price,
                            RealeseDate = itemBook.RealeseDate,

                            //new properties
                            Raiting = itemBook.Raiting,
                            Description = itemBook.Description,
                            LinkForProductContentWhenPurchase = itemBook.LinkForProductContentWhenPurchase
                        };
                        disp.Add(book);
                    }

                }

                return disp;
            }

            public List<OutputMovies> PersonalMovies(string userId)
            {
                var options = new DbContextOptionsBuilder<MovieShopDBSEContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

                var db = new MovieShopDBSEContext(options);

                Shops shops = new Shops
                {
                    Id = 1,
                    MovieId = 1,
                    UserId = userId
                };

                Movies moviesq = new Movies
                {
                    Id = 1,
                };


                db.Movies.Add(moviesq);
                db.SaveChanges();

                db.Shops.Add(shops);
                db.SaveChanges();


                var displays = new List<OutputMovies>();

                //getting personal collection of movies for current user
                var userMovis = db.Shops
                    .Where(x => x.UserId == userId)
                    .Select(x => x.Movie)
                    .ToList();

                foreach (var itemMovie in userMovis)
                {
                    if (itemMovie != null)
                    {

                        //mapping to output model
                        OutputMovies movie = new OutputMovies
                        {
                            Id = itemMovie.Id,
                            Title = itemMovie.Title,
                            Director = itemMovie.Director,
                            Genre = itemMovie.Genre,
                            Picture = itemMovie.Picture,
                            Discount = itemMovie.Discount,
                            price = itemMovie.price,
                            RealeaseDate = itemMovie.RealeaseDate,
                            ShopsMovieId = itemMovie.ShopsMovieId,
                            LinkForPurchasedContend = itemMovie.LinkForProductContentWhenPurchase,

                            //new properties
                            Rate = itemMovie.Rate,
                            Actors = itemMovie.Actors,
                            Raiting = itemMovie.Raiting,
                            Description = itemMovie.Description
                        };

                        displays.Add(movie);
                    }
                }

                return displays;
            }
        }
    }
}
