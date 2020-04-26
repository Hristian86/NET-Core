using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBshop.Data.Data;
using MBshop.Models;
using MBshop.Service.interfaces;
using MBshop.Service.OutputModels;
using MBshop.Service.WebConstants;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MBshop.Test
{
    public class SearchEngineTest
    {

        [Fact]
        public void ShouldReturnSearchCollection()
        {
            var service = new SearchEngine();

            var result = service.ViewProducts(null,null);

            Assert.Equal(6,result.Count);

        }

        [Fact]
        public void ShouldReturnSearchResultCountOfItemsJony()
        {
            var service = new SearchEngine();

            var result = service.Search("Jony", null);

            Assert.Equal(2,result.Count);
        }

        [Fact]
        public void ShouldReturnSearchResultCountOfItemsVampire()
        {
            var service = new SearchEngine();

            var result = service.Search("Vampire", null);

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void ShouldReturnSearchResultCountOfItemsForNull()
        {
            var service = new SearchEngine();
             
            var result = service.Search("", null);

            Assert.NotEqual(1, result.Count);
        }

        [Fact]
        public void ShouldReturnSearchResultCountOfItemsForLetter_v_()
        {
            var service = new SearchEngine();

            var result = service.Search("v", null);

            Assert.Equal(2, result.Count);
        }
    }
    public class SearchEngine : ISearchEngineService
    {
        public int GetAllCount()
        {
            throw new NotImplementedException();
        }

        public List<ViewProducts> Search(string searchItem, string user)
        {
            var options = new DbContextOptionsBuilder<MovieShopDBSEContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            MovieShopDBSEContext db = new MovieShopDBSEContext(options);

            Movies movie1 = new Movies
            {
                Id = 1,
                price = 1.0,
                Title = "Vampire",
                Rate = 1
            };

            Movies movie2 = new Movies
            {
                Id = 2,
                price = 1.0,
                Rate = 1
            };

            Movies movie3 = new Movies
            {
                Id = 3,
                price = 1.0,
                Title = "Jony",
                Rate = 1
            };

            Books booki = new Books
            {
                Id = 1,
                price = 1.0,
                Title = "Vampire",
                Rate = 1
            };

            Books book2 = new Books
            {
                Id = 2,
                price = 1.0,
                Rate = 1
            };

            Books book3 = new Books
            {
                Id = 3,
                price = 1.0,
                Title = "Jony",
                Rate = 1
            };

            db.Books.Add(book3);
            db.Books.Add(book2);
            db.Books.Add(booki);

            db.Add(movie3);
            db.Add(movie2);
            db.Add(movie1);
            db.SaveChangesAsync();

            var movieses = db.Movies.ToList();
            var bookses = db.Books.ToList();

            var result = movieses.Where(x => x.Title != null && x.Title.ToLower()
                .Contains(searchItem.ToLower()))
                .Select(item => new ViewProducts
                {
                    Id = item.Id,
                    Title = item.Title,
                    price = item.price,
                    Picture = item.Picture,
                    Genre = item.Genre,
                    Rate = item.Rate,
                    Type = WebConstantsVariables.Movie

                }).ToList();

            result.AddRange(bookses.Where(x => x.Title != null && x.Title.ToLower()
                .Contains(searchItem.ToLower()))
                .Select(item => new ViewProducts
                {
                    Id = item.Id,
                    Title = item.Title,
                    price = item.price,
                    Picture = item.Picture,
                    Genre = item.Genre,
                    Rate = item.Rate,
                    Type = WebConstantsVariables.Book

                }));

            //result.AddRange(result1);

            return result.ToList();
        }

        public List<ViewProducts> Search(string searchItem, string user, string orderBy)
        {
            throw new NotImplementedException();
        }

        public List<ViewProducts> ViewProducts(string userId, string orderBy)
        {
            var options = new DbContextOptionsBuilder<MovieShopDBSEContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            MovieShopDBSEContext db = new MovieShopDBSEContext(options);

            Movies movie1 = new Movies
            {
                Id = 1,
                price = 1.0,
                Title = "Vampire",
                Rate = 1
            };

            Movies movie2 = new Movies
            {
                Id = 2,
                price = 1.0,
                Rate = 1
            };

            Movies movie3 = new Movies
            {
                Id = 3,
                price = 1.0,
                Title = "Jony",
                Rate = 1
            };

            Books booki = new Books
            {
                Id = 1,
                price = 1.0,
                Title = "Vampire",
                Rate = 1
            };

            Books book2 = new Books
            {
                Id = 2,
                price = 1.0,
                Rate = 1
            };

            Books book3 = new Books
            {
                Id = 3,
                price = 1.0,
                Title = "Jony",
                Rate = 1
            };

            db.Books.Add(book3);
            db.Books.Add(book2);
            db.Books.Add(booki);

            db.Add(movie3);
            db.Add(movie2);
            db.Add(movie1);
            db.SaveChangesAsync();

            var movieses = db.Movies.ToList();
            var bookses = db.Books.ToList();

            var result = movieses
                .Where(r => r.Rate != null)
                .Select(item => new ViewProducts
                {
                    Id = item.Id,
                    Title = item.Title,
                    price = item.price,
                    Picture = item.Picture,
                    Genre = item.Genre,
                    Rate = Math.Round((double)item.Rate, 1),
                    Type = WebConstantsVariables.Movie

                }).ToList();

            var result1 = bookses
                .Where(r => r.Rate != null)
                .Select(item => new ViewProducts
                {
                    Id = item.Id,
                    Title = item.Title,
                    price = item.price,
                    Picture = item.Picture,
                    Genre = item.Genre,
                    Rate = Math.Round((double)item.Rate, 1),
                    Type = WebConstantsVariables.Book

                }).ToList();

            result.AddRange(result1);


            //Can be made orderBy
            if (orderBy != null)
            {
                if (orderBy == "TitleA-Z")
                {
                    result = result.OrderBy(x => x.Title).ToList();
                }
                else if (orderBy == "TitleZ-A")
                {
                    result = result.OrderByDescending(x => x.Title).ToList();
                }
                else if (orderBy == "Price0-9")
                {
                    result = result.OrderBy(x => x.price).ToList();
                }
                else if (orderBy == "Price9-0")
                {
                    result = result.OrderByDescending(x => x.price).ToList();
                }
                else if (orderBy == "RatingHigh")
                {
                    result = result.OrderByDescending(x => x.Rate).ToList();
                }
                else if (orderBy == "Ratinglow")
                {
                    result = result.OrderBy(x => x.Rate).ToList();
                }
            }

            return result.ToList();
        }

        public List<ViewProducts> ViewProductsWithPage(string userId, string orderBy, int page = 1)
        {
            throw new NotImplementedException();
        }

        public List<ViewProducts> ViewProductsWithPage(string userId, string orderBy, int page = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }
    }
}
