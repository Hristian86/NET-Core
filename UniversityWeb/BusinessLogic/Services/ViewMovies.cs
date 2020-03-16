using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic.interfaces;
using BusinessLogic.OutputModels;
using DataDomain.Data.Models;

namespace BusinessLogic.Services
{
    public class ViewMovies : IViewMovies
    {
        private readonly MovieRentalDBSEContext db;

        public ViewMovies(MovieRentalDBSEContext dbs)
        {
            this.db = dbs;
        }

        public List<Movieses> GetListOfMovies()
        {
            return GetMovies();
        }

        private List<Movieses> GetMovies()
        {
            var display = new List<Movieses>();

            var Mview = this.db.Movies.ToList();

            foreach (var item in Mview)
            {
                Movieses movie = new Movieses
                {
                    Id = item.Id,
                    Title = item.Title,
                    Director = item.Director,
                    Genre = item.Genre,
                    Picture = item.Picture,
                    Discount = item.Discount,
                    price = item.price,
                    RealeaseDate = item.RealeaseDate,
                    RentMovieId = item.RentMovieId
                };

                display.Add(movie);
            }
            return display;
        }
    }
}