﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBshop.Service.interfaces;
using MBshop.Data.Data;
using MBshop.Models;

namespace MBshop.Service.Services
{
    public class AdminPanel : IAdminPanel
    {
        private readonly MovieShopDBSEContext db;

        public AdminPanel(MovieShopDBSEContext db)
        {
            this.db = db;
        }

        //Find
        public async Task<Movies> FindMovieById(int? id)
        {
            var movie = await this.db.Movies.FindAsync(id);

            return movie;
        }

        public async Task<Books> FindBookById(int? id)
        {
            var book = await this.db.Books.FindAsync(id);

            return book;
        }

        //Crete
        public async Task<string> CreateMovie(Movies movie)
        {
            movie.Rate = 0;

            this.db.Add(movie);

            await this.db.SaveChangesAsync();

            return $"Movie {movie.Title} created";
        }

        public async Task<string> CreateBook(Books book)
        {
            book.Rate = 0;

            this.db.Add(book);

            await this.db.SaveChangesAsync();

            return $"Book {book.Title} created";
        }

        //Get
        public List<Movies> GetMovies()
        {
            return this.db.Movies.ToList();
        }

        public List<Books> GetBooks()
        {
            return this.db.Books.ToList();
        }

        //Update
        public async Task<string> UpdateMovie(Movies movie)
        {
            this.db.Update(movie);

            await this.db.SaveChangesAsync();

            return $"Movie {movie.Title} updated successfully";
        }

        public async Task<string> UpdateBook(Books book)
        {
            this.db.Update(book);

            await this.db.SaveChangesAsync();

            return $"Book {book.Title} updated successfully";
        }

        //setted with interface Remove
        public async Task<string> RemoveMovie(int movieId)
        {
            string title = "";

            var movies = await this.db.Movies.FindAsync(movieId);

            title = movies.Title;

            //cascade delete manualy 
            var shopMovies = this.db.Shops
                .Where(x => x.MovieId == movies.Id)
                .ToList();

            var ratingMovies = this.db.Rating
                .Where(x => x.MoviesId == movies.Id)
                .ToList();

            if (ratingMovies != null)
            {
                this.db.Rating.RemoveRange(ratingMovies);
            }

            if (shopMovies != null)
            {
                this.db.RemoveRange(shopMovies);
            }

            if (movies != null)
            {
                this.db.Movies.Remove(movies);

                await this.db.SaveChangesAsync();

                return $"Movie {title} has been deleted";
            }

            return $"Movie {title} not found";
        }

        public async Task<string> RemoveBook(int bookId)
        {
            var title = "";

            //fixing cascade delete manualy 
            var books = await this.db.Books
                .FindAsync(bookId);

            title = books.Title;

            var shopBooks = this.db.Shops
                .Where(x => x.BooksId == books.Id)
                .ToList();

            var ratingBooks = this.db.Rating
                .Where(x => x.BooksId == books.Id)
                .ToList();

            if (ratingBooks != null)
            {
                this.db.Rating.RemoveRange(ratingBooks);
            }

            if (shopBooks != null)
            {
                this.db.RemoveRange(shopBooks);
            }

            if (books != null)
            {
                this.db.Books.Remove(books);

                await this.db.SaveChangesAsync();

                return $"Book {title} has been deleted";
            }

            return $"Book {title} not found";
        }

        //Bool chek for existing product
        public bool MovieExist(int id)
        {
            return this.db.Movies.Any(x => x.Id == id);
        }

        public bool BookExist(int id)
        {
            return this.db.Books.Any(x => x.Id == id);
        }

        //User role assignment
        public void GetUserRoles()
        {

        }

    }
}