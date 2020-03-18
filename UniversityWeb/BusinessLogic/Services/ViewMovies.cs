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
        private readonly MovieShopDBSEContext db;

        public ViewMovies(MovieShopDBSEContext dbs)
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

            foreach (var itemMovie in Mview)
            {
                Movieses movie = new Movieses
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

                    //new properties
                    Actors = itemMovie.Actors,
                    Raiting = itemMovie.Raiting,
                    Description = itemMovie.Description
                };

                display.Add(movie);
            }
            return display;
        }
    }
}