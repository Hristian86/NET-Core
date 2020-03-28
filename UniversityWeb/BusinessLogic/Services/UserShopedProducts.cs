using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.interfaces;
using BusinessLogic.OutputModels;
using Data.Domain.Data;

namespace BusinessLogic.Services
{
    public class UserShopedProducts : IUserShopedProducts
    {
        private readonly MovieShopDBSEContext db;

        public UserShopedProducts(MovieShopDBSEContext db)
        {
            this.db = db;
        }

        public List<OutputMovies> PersonalMovies(string id)
        {
            var display = ConvertMovie(id);
            return display;
        }

        public List<OutputBooks> PersonalBooks(string id)
        {
            var toBeDisplayed = ConvertBooks(id);
            return toBeDisplayed;
        }

        private List<OutputBooks> ConvertBooks(string id)
        {
            var disp = new List<OutputBooks>();

            var userBooks = this.db.Shops
                .Where(x => x.UserId == id)
                .Select(x => x.Books)
                .ToList();

            foreach (var itemBook in userBooks)
            {
                if (itemBook != null)
                {

                    OutputBooks book = new OutputBooks
                    {
                        Id = itemBook.Id,
                        Title = itemBook.Title,
                        Author = itemBook.Author,
                        Genre = itemBook.Genre,
                        Picture = itemBook.Picture,
                        Discount = itemBook.Discount,
                        price = itemBook.price,
                        RealeseDate = itemBook.RealeseDate,
                        
                        //new properties
                        Raiting = itemBook.Raiting,
                        Description = itemBook.Description,
                        LinkForProductContentWhenPurchase = itemBook.LinkForProductContentWhenPurchase
                    };
                    disp.Add(book);
                }

            }

            return disp;
        }

        private List<OutputMovies> ConvertMovie(string id)
        {
            var displays = new List<OutputMovies>();

            var userMovis = this.db.Shops
                .Where(x => x.UserId == id)
                .Select(x => x.Movie)
                .ToList();

            foreach (var itemMovie in userMovis)
            {
                if (itemMovie != null)
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
                        LinkForPurchasedContend = itemMovie.LinkForProductContentWhenPurchase,

                        //new properties
                        Rate = itemMovie.Rate,
                        Actors = itemMovie.Actors,
                        Raiting = itemMovie.Raiting,
                        Description = itemMovie.Description
                    };

                    displays.Add(movie);
                }
            }

            return displays;
        }
    }
}
