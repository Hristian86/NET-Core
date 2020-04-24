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
    public class AdminPanelTest
    {
        //remove movie tests
        [Fact]
        public void ShouldReturnRemovedMovieNameVampire()
        {

            var services = new AdminPanel();

            var result = services.RemoveMovie(1);

            Assert.Equal("Movie Vampire has been deleted", result.Result);

        }

        [Fact]
        public void ShouldReturnRemovedMovieNameJony()
        {
            var services = new AdminPanel();

            var result = services.RemoveMovie(3);

            Assert.Equal("Movie Jony has been deleted", result.Result);
        }

        [Fact]
        public void ShouldReturnMovieNotFound()
        {
            var services = new AdminPanel();

            var result = services.RemoveMovie(4);

            Assert.Equal("Movie  not found", result.Result);
        }

        //remove book test
        [Fact]
        public void ShouldReturnRemovedBookNameVampire()
        {

            var services = new AdminPanel();

            var result = services.RemoveBook(1);

            Assert.Equal("Book Vampire has been deleted", result.Result);

        }

        [Fact]
        public void ShouldReturnRemovedBookNameJony()
        {
            var services = new AdminPanel();

            var result = services.RemoveBook(3);

            Assert.Equal("Book Jony has been deleted", result.Result);
        }

        [Fact]
        public void ShouldReturnBookNotFound()
        {
            var services = new AdminPanel();

            var result = services.RemoveBook(4);

            Assert.Equal("Book  not found", result.Result);
        }



    }

    public class AdminPanel : IAdminPanel
    {
        public bool BookExist(int id)
        {
            throw new NotImplementedException();
        }

        public Task<string> CreateBook(Books book)
        {


            throw new NotImplementedException();
        }

        public Task<string> CreateMovie(Movies movie)
        {
            throw new NotImplementedException();
        }

        public Task<Books> FindBookById(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<Movies> FindMovieById(int? id)
        {
            throw new NotImplementedException();
        }

        public List<Books> GetBooks()
        {
            throw new NotImplementedException();
        }

        public List<Movies> GetMovies()
        {
            throw new NotImplementedException();
        }

        public bool MovieExist(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<string> RemoveBook(int bookId)
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
            await db.SaveChangesAsync();



            var title = "";

            bool bookExists = db.Books.Any(x => x.Id == bookId);

            if (!bookExists)
            {
                return $"Book {title} not found";
            }

            //fixing cascade delete manualy 
            var books = await db.Books
                .FindAsync(bookId);

            title = books.Title;

            var shopBooks = db.Shops
                .Where(x => x.BooksId == books.Id)
                .ToList();

            var ratingBooks = db.Rating
                .Where(x => x.BooksId == books.Id)
                .ToList();

            if (ratingBooks != null)
            {
                db.Rating.RemoveRange(ratingBooks);
            }

            if (shopBooks != null)
            {
                db.RemoveRange(shopBooks);
            }

            if (books != null)
            {
                db.Books.Remove(books);

                await db.SaveChangesAsync();

                return $"Book {title} has been deleted";
            }

            return $"Book {title} not found";
        }

        public async Task<string> RemoveMovie(int movieId)
        {

            var options = new DbContextOptionsBuilder<MovieShopDBSEContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            MovieShopDBSEContext db = new MovieShopDBSEContext(options);

            Movies movie1 = new Movies
            {
                Id = 1,
                price = 1.0,
                Title = "Vampire"
            };

            Movies movie2 = new Movies
            {
                Id = 2,
                price = 1.0
            };

            Movies movie3 = new Movies
            {
                Id = 3,
                price = 1.0,
                Title = "Jony"
            };

            db.Add(movie3);
            db.Add(movie2);
            db.Add(movie1);
            await db.SaveChangesAsync();


            string title = "";

            bool movieExists = db.Movies.Any(x => x.Id == movieId);

            if (!movieExists)
            {
                return $"Movie {title} not found";
            }

            var movies = await db.Movies.FindAsync(movieId);

            title = movies.Title;

            //cascade delete manualy 
            var shopMovies = db.Shops
                .Where(x => x.MovieId == movies.Id)
                .ToList();

            var ratingMovies = db.Rating
                .Where(x => x.MoviesId == movies.Id)
                .ToList();

            if (ratingMovies != null)
            {
                db.Rating.RemoveRange(ratingMovies);
            }

            if (shopMovies != null)
            {
                db.RemoveRange(shopMovies);
            }

            if (movies != null)
            {
                db.Movies.Remove(movies);

                await db.SaveChangesAsync();

                return $"Movie {title} has been deleted";
            }

            return $"Movie {title} not found";
        }

        public Task<string> UpdateBook(Books book)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateMovie(Movies movie)
        {
            throw new NotImplementedException();
        }
    }
}
