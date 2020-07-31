using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBshop.Service.interfaces;
using MBshop.Data.Data;
using MBshop.Models;
using Microsoft.EntityFrameworkCore;

namespace MBshop.Service.Services
{
    public class AdminPanel : IAdminPanel
    {
        private readonly MovieShopDBSEContext db;

        public AdminPanel(MovieShopDBSEContext db)
        {
            this.db = db;
        }

        //view shops
        public Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Shops, AspNetUsers> ViewShops()
        {
            var ShopList = this.db.Shops.Include(s => s.Books).Include(s => s.Movie).Include(s => s.User);

            return ShopList;
        }

        //chek for shops to be deleted
        public async Task<Shops> ChekViewShop(int id)
        {
            var shops = await this.db.Shops
                .Include(s => s.Books)
                .Include(s => s.Movie)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            return shops;
        }

        public async Task<string> DeleteViewShops(int id)
        {
            var shops = await this.db.Shops.FindAsync(id);
            this.db.Shops.Remove(shops);
            await this.db.SaveChangesAsync();

            return $"Success";
        }

        // loged users
        public List<Logs> LoggedUsers() => this.db.Logs
                .Select(x => x)
                .ToList();

        //Delete logs
        public Logs ChekForLog(string userName, int id) => this.db.Logs.Where(x => x.UserLoged == userName && x.LogId == id).FirstOrDefault();

        public async Task<string> DeleteLogsAfterTheChek(string userName, int id)
        {

            var log = this.db.Logs
                 .Where(x => x.UserLoged == userName && x.LogId == id)
                 .FirstOrDefault();

            this.db.Logs.Remove(log);

            await this.db.SaveChangesAsync();

            return $"Success";
        }

        public async Task<string> DeleteAllLogs()
        {
            var allLogs = this.db.Logs
                .Where(x => x.UserLoged != null)
                .ToList();

            this.db.Logs.RemoveRange(allLogs);

            await this.db.SaveChangesAsync();

            return $"Success";
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

        // Remove movie from database
        public async Task<string> RemoveMovie(int movieId)
        {
            string title = "";

            bool movieExists = this.db.Movies.Any(x => x.Id == movieId);

            if (!movieExists)
            {
                return $"Movie {title} not found";
            }

            var movieToBeRemoved = await this.db.Movies.FindAsync(movieId);

            title = movieToBeRemoved.Title;

            //cascade delete manualy 
            var shopMovies = this.db.Shops
                .Where(x => x.MovieId == movieToBeRemoved.Id)
                .ToList();

            var ratingMovies = this.db.Rating
                .Where(x => x.MoviesId == movieToBeRemoved.Id)
                .ToList();

            if (ratingMovies != null)
            {
                this.db.Rating.RemoveRange(ratingMovies);
            }

            if (shopMovies != null)
            {
                this.db.RemoveRange(shopMovies);
            }

            if (movieToBeRemoved != null)
            {
                this.db.Movies.Remove(movieToBeRemoved);

                await this.db.SaveChangesAsync();

                return $"Movie {title} has been deleted";
            }

            return $"Movie {title} not found";
        }

        //Remove book from database
        public async Task<string> RemoveBook(int bookId)
        {
            var title = "";

            bool bookExists = db.Books.Any(x => x.Id == bookId);

            if (!bookExists)
            {
                return $"Book {title} not found";
            }

            //fixing cascade delete manualy 
            var bookToBeRemoved = await this.db.Books
                .FindAsync(bookId);

            title = bookToBeRemoved.Title;

            var shopBooks = this.db.Shops
                .Where(x => x.BooksId == bookToBeRemoved.Id)
                .ToList();

            var ratingBooks = this.db.Rating
                .Where(x => x.BooksId == bookToBeRemoved.Id)
                .ToList();

            if (ratingBooks != null)
            {
                this.db.Rating.RemoveRange(ratingBooks);
            }

            if (shopBooks != null)
            {
                this.db.RemoveRange(shopBooks);
            }

            if (bookToBeRemoved != null)
            {
                this.db.Books.Remove(bookToBeRemoved);

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