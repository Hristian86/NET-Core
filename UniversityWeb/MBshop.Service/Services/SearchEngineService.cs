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

        /// <summary>
        /// Search for product in combine collection from database
        /// </summary>
        /// <param name="searchItem"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<ViewProducts> Search(string searchItem, string userId)
        {
            var movieses = this.movies.GetListOfMovies();
            var bookses = this.books.GetListOfBooks();

            var userPersonalMovies = this.userItems.PersonalMovies(userId);

            var userPersonalBooks = this.userItems.PersonalBooks(userId);

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

        /// <summary>
        /// Combined collection of products
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<ViewProducts> ViewProducts(string userId)
        {
            var movieses = this.movies.GetListOfMovies();
            var bookses = this.books.GetListOfBooks();

            var userPersonalMovies = this.userItems.PersonalMovies(userId);

            var userPersonalBooks = this.userItems.PersonalBooks(userId);

            this.status.StatusChekBooks(bookses, userPersonalBooks);

            //status chek for movies
            this.status.StatusChekMovies(movieses, userPersonalMovies);

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

            return result.ToList();
        }
    }
}