using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic.interfaces;
using BusinessLogic.OutputModels;
using Data.Domain.Data;

namespace BusinessLogic.Services
{
    public class ViewMovies : IViewMovies
    {
        private readonly MovieShopDBSEContext db;

        public ViewMovies(MovieShopDBSEContext dbs)
        {
            this.db = dbs;
        }

        public List<OutputMovies> GetListOfMovies()
        {
            return GetMovies();
        }

        private List<OutputMovies> GetMovies()
        {
            var display = new List<OutputMovies>();

            foreach (var itemMovie in this.db.Movies)
            {

                OutputMovies movie = new OutputMovies
                {
                    Status = false,
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
                    Rate = itemMovie.Rate,
                    Actors = itemMovie.Actors,
                    Raiting = itemMovie.Raiting,
                    Description = itemMovie.Description
                };

                display.Add(movie);
            }
            return display;
        }

        public List<OutputMovies> SortMovies(int orderBy)
        {
            if (orderBy == 1)
            {
                return GetMovies().OrderBy(x => x.Title).ToList();
            }
            else if (orderBy == 2)
            {
                return GetMovies().OrderByDescending(x => x.Title).ToList();
            }
            else if (orderBy == 3)
            {
                return GetMovies().OrderBy(x => x.price).ToList();
            }
            else if (orderBy == 4)
            {
                return GetMovies().OrderByDescending(x => x.price).ToList();
            }
            else
            {
                return GetMovies();
            }
        }

    }
}