using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.interfaces;
using DataDomain.Data.Models;
using DataDomain.Data;

namespace BusinessLogic.Services
{
    public class AdminPanelMovies : IAdminPanelMovies
    {
        private readonly MovieShopDBSEContext db;

        public AdminPanelMovies(MovieShopDBSEContext db)
        {
            this.db = db;
        }

        public async Task<Movies> FindMovieById(int? id)
        {
            var movies = await db.Movies.FindAsync(id);

            return movies;
        }

        public async Task SaveMovie(Movies movie)
        {
            this.db.Add(movie);

            await this.db.SaveChangesAsync();
        }

        public List<Movies> GetMovies()
        {
            return this.db.Movies.ToList();
        }

        public async Task UpdateMovie(Movies movie)
        {
            this.db.Update(movie);

            await this.db.SaveChangesAsync();
        }

        public async Task Remove(Movies movie)
        {
            this.db.Remove(movie);

            await this.db.SaveChangesAsync();
        }

        public bool MovieExist(int id)
        {
            return this.db.Movies.Any(x => x.Id == id);
        }
    }
}