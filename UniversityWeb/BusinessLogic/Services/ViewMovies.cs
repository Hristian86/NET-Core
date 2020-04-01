using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic.interfaces;
using BusinessLogic.OutputModels;
using Data.Domain.Data;
using Db.Models;

namespace BusinessLogic.Services
{
    public class ViewMovies : IViewMovies
    {
        private readonly MovieShopDBSEContext db;
        private List<OutputMovies> display;

        public ViewMovies(MovieShopDBSEContext dbs)
        {
            display = new List<OutputMovies>();
            this.db = dbs;
        }

        public List<OutputMovies> GetListOfMovies()
        {
            return GetMovies();
        }

        private List<OutputMovies> GetMovies()
        {
            //var display = new List<OutputMovies>();

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

                this.display.Add(movie);
            }
            return this.display;
        }

        public List<OutputMovies> SortMovies(int orderBy)
        {
            if (orderBy == 1)
            {
                //order by title A-Z
                return GetMovies().OrderBy(x => x.Title).ToList();
            }
            else if (orderBy == 2)
            {
                //order by title Z-A
                return GetMovies().OrderByDescending(x => x.Title).ToList();
            }
            else if (orderBy == 3)
            {
                //order by price 0-9
                return GetMovies().OrderBy(x => x.price).ToList();
            }
            else if (orderBy == 4)
            {
                //order by price 9-0
                return GetMovies().OrderByDescending(x => x.price).ToList();
            }
            else
            {
                return GetMovies();
            }
        }

    }
}