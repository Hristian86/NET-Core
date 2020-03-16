using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.interfaces;
using DataDomain.Data.Models;
using BusinessLogic.OutputModels;

namespace BusinessLogic.Services
{
    public class ConvertingCollection : IConvertingCollection
    {
        public ConvertingCollection()
        {
        }
        public List<Movieses> GetMovies(List<Movies> Mview)
        {
            var display = new List<Movieses>();

            foreach (var item in Mview)
            {
                Movieses movie = new Movieses
                {
                    Id = item.Id,
                    Title = item.Title,
                    Director = item.Director,
                    Genre = item.Genre,
                    Picture = item.Picture,
                    RealeaseDate = item.RealeaseDate,
                    RentMovieId = item.RentMovieId
                };

                display.Add(movie);
            }
            return display;
        }

    }
}
