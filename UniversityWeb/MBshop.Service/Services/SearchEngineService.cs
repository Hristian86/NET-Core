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
    public class SearchEngineService : ISearchEngineService
    {
        private readonly IViewMoviesService movies;
        private readonly IViewBooksService books;
        private readonly IUserShopedProductsService userItems;
        private readonly Status status;

        public SearchEngineService(IViewMoviesService movies,
            IViewBooksService books,
            IUserShopedProductsService userItems,
            Status status)
        {
            this.movies = movies;
            this.books = books;
            this.userItems = userItems;
            this.status = status;
        }


        public List<ViewProducts> Search(string searchItem, string user)
        {
            var movieses = movies.GetListOfMovies();
            var bookses = books.GetListOfBooks();

            var userPersonalMovies = userItems.PersonalMovies(user);

            var userPersonalBooks = userItems.PersonalBooks(user);

            status.StatusChekBooks(bookses,userPersonalBooks);

            //status chek for movies
            status.StatusChekMovies(movieses,userPersonalMovies);

            var result = movieses
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



            var result1 = bookses
                .Select(item => new ViewProducts
            {
                Id = item.Id,
                Title = item.Title,
                price = item.price,
                Picture = item.Picture,
                Genre = item.Genre,
                Status = item.Status,
                Rate = item.Rate,
                Type = WebConstansVariables.Book

            }).ToList();

            result.AddRange(result1);

            return result.ToList().Where(x => x.Title.ToLower().Contains(searchItem.ToLower())).ToList();
        }
    }
}
