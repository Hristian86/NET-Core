using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBshop.Service.interfaces;
using MBshop.Service.OutputModels;
using MBshop.Data.Data;
using MBshop.Models;

namespace MBshop.Service.Services
{
    public class ViewMoviesService : IViewMoviesService
    {
        private readonly MovieShopDBSEContext db;

        public ViewMoviesService(MovieShopDBSEContext dbs)
        {
            this.db = dbs;
        }

        public List<OutputMovies> GetListOfMovies() => this.db.Movies.Select(itemMovie => new OutputMovies
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
        }).ToList();


        public List<OutputMovies> SortMovies(int orderBy)
        {
            if (orderBy == 1)
            {
                //order by title A-Z
                return GetListOfMovies().OrderBy(x => x.Title).ToList();
            }
            else if (orderBy == 2)
            {
                //order by title Z-A
                return GetListOfMovies().OrderByDescending(x => x.Title).ToList();
            }
            else if (orderBy == 3)
            {
                //order by price 0-9
                return GetListOfMovies().OrderBy(x => x.price).ToList();
            }
            else if (orderBy == 4)
            {
                //order by price 9-0
                return GetListOfMovies().OrderByDescending(x => x.price).ToList();
            }
            else
            {
                return GetListOfMovies();
            }
        }
    }
}