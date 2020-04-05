using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBshop.Data.Data;
using MBshop.Service.interfaces;
using MBshop.Service.OutputModels;
using MBshop.Service.WebConstants;

namespace MBshop.Service.Services
{
    public class SearchEngine
    {
        private readonly IViewMovies movies;
        private readonly IViewBooks books;

        public SearchEngine(IViewMovies movies,
            IViewBooks books)
        {
            this.movies = movies;
            this.books = books;
        }


        public List<ViewProducts> Search(string searchItem)
        {
            var result = movies
                .GetListOfMovies()
                .Select(item => new ViewProducts
                {
                    Id = item.Id,
                    Title = item.Title,
                    price = item.price,
                    Picture = item.Picture,
                    Genre = item.Genre,
                    Status = item.Status,
                    Rate = item.Rate,
                    Type = WebConstansVariables.Movie

                }).ToList();

            var result1 = this.books.GetListOfBooks().Select(item => new ViewProducts
            {
                Id = item.Id,
                Title = item.Title,
                price = item.price,
                Picture = item.Picture,
                Genre = item.Genre,
                Status = item.Status,
                Rate = item.Rate,
                Type = WebConstansVariables.Movie

            }).ToList();

            result.AddRange(result1);

            return result.ToList().Where(x => x.Title.ToLower().Contains(searchItem.ToLower())).ToList();
        }
    }
}
