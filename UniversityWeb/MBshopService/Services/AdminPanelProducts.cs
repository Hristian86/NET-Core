using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBshopService.interfaces;
using Data.Domain.Data;
using Db.Models;

namespace MBshopService.Services
{
    public class AdminPanelProducts : IAdminPanelProducts
    {
        private readonly MovieShopDBSEContext db;

        public AdminPanelProducts(MovieShopDBSEContext db)
        {
            this.db = db;
        }

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

        public async Task SaveMovie(Movies movie)
        {
            this.db.Add(movie);

            await this.db.SaveChangesAsync();
        }

        public async Task SaveBook(Books book)
        {
            this.db.Add(book);

            await this.db.SaveChangesAsync();
        }

        public List<Movies> GetMovies()
        {
            return this.db.Movies.ToList();
        }

        public List<Books> GetBooks()
        {
            return this.db.Books.ToList();
        }

        public async Task UpdateMovie(Movies movie)
        {
            this.db.Update(movie);

            await this.db.SaveChangesAsync();
        }

        public async Task UpdateBook(Books book)
        {
            this.db.Update(book);

            await this.db.SaveChangesAsync();
        }

        public async Task RemoveMovie(Movies movie)
        {
            this.db.Remove(movie);

            await this.db.SaveChangesAsync();
        }

        public async Task RemoveBook(Books book)
        {
            this.db.Remove(book);

            await this.db.SaveChangesAsync();
        }

        public bool MovieExist(int id)
        {
            return this.db.Movies.Any(x => x.Id == id);
        }

        public bool BookExist(int id)
        {
            return this.db.Books.Any(x => x.Id == id);
        }
    }
}