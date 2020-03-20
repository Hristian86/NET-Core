using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.interfaces;
using BusinessLogic.OutputModels;
using DataDomain.Data.Models;

namespace BusinessLogic.Services
{
    public class UserShopedProducts : IUserShopedProducts
    {
        private readonly MovieShopDBSEContext db;

        public UserShopedProducts(MovieShopDBSEContext db)
        {
            this.db = db;
        }

        public List<OutputMovies> PersonalItems(string id)
        {
            var display = Convert(id);
            return display;
        }

        private List<OutputMovies> Convert(string id)
        {
            var display = new List<OutputMovies>();

            var userMovis = this.db.Shops
                .Where(x => x.UserId == id)
                .Select(x => x.Movie)
                .ToList();

            foreach (var itemMovie in userMovis)
            {
                OutputMovies movie = new OutputMovies
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
