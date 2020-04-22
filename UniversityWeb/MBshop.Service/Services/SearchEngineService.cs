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

            status.StatusChekBooks(bookses, userPersonalBooks);

            //status chek for movies
            status.StatusChekMovies(movieses, userPersonalMovies);

            var result = movieses.Where(x => x.Title != null && x.Title.ToLower()
                .Contains(searchItem.ToLower()))
                .Select(item => new ViewProducts
                {
                    Id = item.Id,
                    Title = item.Title,
                    price = item.price,
                    Picture = item.Picture,
                    Genre = item.Genre,
                    Status = item.Status,
                    Rate = item.Rate,
                    Type = WebConstantsVariables.Movie

                }).ToList();

            result.AddRange(bookses.Where(x => x.Title != null && x.Title.ToLower()
                .Contains(searchItem.ToLower()))
                .Select(item => new ViewProducts
                {
                    Id = item.Id,
                    Title = item.Title,
                    price = item.price,
                    Picture = item.Picture,
                    Genre = item.Genre,
                    Status = item.Status,
                    Rate = item.Rate,
                    Type = WebConstantsVariables.Book

                }));

            //result.AddRange(result1);

            return result.ToList();
        }

        /// <summary>
        /// Combined collection of products and sorting
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<ViewProducts> ViewProducts(string userId, string orderBy)
        {
            var movieses = this.movies.GetListOfMovies();
            var bookses = this.books.GetListOfBooks();

            var userPersonalMovies = this.userItems.PersonalMovies(userId);

            var userPersonalBooks = this.userItems.PersonalBooks(userId);

            this.status.StatusChekBooks(bookses, userPersonalBooks);

            //status chek for movies
            this.status.StatusChekMovies(movieses, userPersonalMovies);

            var result = movieses
                .OrderByDescending(x => x.Rate)
                .Where(r => r.Rate != null)
                .Select(item => new ViewProducts
                {
                    Id = item.Id,
                    Title = item.Title,
                    price = item.price,
                    Picture = item.Picture,
                    Genre = item.Genre,
                    Status = item.Status,
                    Rate = Math.Round((double)item.Rate, 1),
                    Type = WebConstantsVariables.Movie

                }).ToList();

            var result1 = bookses
                .OrderByDescending(x => x.Rate)
                .Where(r => r.Rate != null)
                .Select(item => new ViewProducts
                {
                    Id = item.Id,
                    Title = item.Title,
                    price = item.price,
                    Picture = item.Picture,
                    Genre = item.Genre,
                    Status = item.Status,
                    Rate = Math.Round((double)item.Rate, 1),
                    Type = WebConstantsVariables.Book

                }).ToList();

            result.AddRange(result1);


            //Can be made orderBy
            if (orderBy != null)
            {
                if (orderBy == "TitleA-Z")
                {
                    result = result.OrderBy(x => x.Title).ToList();
                }
                else if (orderBy == "TitleZ-A")
                {
                    result = result.OrderByDescending(x => x.Title).ToList();
                }
                else if (orderBy == "Price0-9")
                {
                    result = result.OrderBy(x => x.price).ToList();
                }
                else if (orderBy == "Price9-0")
                {
                    result = result.OrderByDescending(x => x.price).ToList();
                }
                else if (orderBy == "RatingHigh")
                {
                    result = result.OrderByDescending(x => x.Rate).ToList();
                }
                else if (orderBy == "Ratinglow")
                {
                    result = result.OrderBy(x => x.Rate).ToList();
                }
            }

            return result.ToList();
        }

        /// <summary>
        /// pagination for all products
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orderBy"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public List<ViewProducts> ViewProductsWithPage(string userId, string orderBy, int page = 1, int pageSize = 5)
        {
            var movieses = this.movies.GetListOfMovies();
            var bookses = this.books.GetListOfBooks();

            var userPersonalMovies = this.userItems.PersonalMovies(userId);

            var userPersonalBooks = this.userItems.PersonalBooks(userId);

            this.status.StatusChekBooks(bookses, userPersonalBooks);

            //status chek for movies
            this.status.StatusChekMovies(movieses, userPersonalMovies);

            var result = movieses
                .Where(r => r.Rate != null)
                .Select(item => new ViewProducts
                {
                    Id = item.Id,
                    Title = item.Title,
                    price = item.price,
                    Picture = item.Picture,
                    Genre = item.Genre,
                    Status = item.Status,
                    Rate = Math.Round((double)item.Rate, 1),
                    Type = WebConstantsVariables.Movie

                }).ToList();

            var result1 = bookses
                .Where(r => r.Rate != null)
                .Select(item => new ViewProducts
                {
                    Id = item.Id,
                    Title = item.Title,
                    price = item.price,
                    Picture = item.Picture,
                    Genre = item.Genre,
                    Status = item.Status,
                    Rate = Math.Round((double)item.Rate, 1),
                    Type = WebConstantsVariables.Book

                }).ToList();

            result.AddRange(result1);


            //Can be made orderBy
            if (orderBy != null)
            {
                if (orderBy == "TitleA-Z")
                {
                    result = result.OrderBy(x => x.Title).ToList();
                }
                else if (orderBy == "TitleZ-A")
                {
                    result = result.OrderByDescending(x => x.Title).ToList();
                }
                else if (orderBy == "Price0-9")
                {
                    result = result.OrderBy(x => x.price).ToList();
                }
                else if (orderBy == "Price9-0")
                {
                    result = result.OrderByDescending(x => x.price).ToList();
                }
                else if (orderBy == "RatingHigh")
                {
                    result = result.OrderByDescending(x => x.Rate).ToList();
                }
                else if (orderBy == "Ratinglow")
                {
                    result = result.OrderBy(x => x.Rate).ToList();
                }
            }

            return result
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public int GetAllCount()
        {
            var countMovieses = this.movies.GetListOfMovies().Count;
            var countbBookses = this.books.GetListOfBooks().Count;

            return countMovieses + countbBookses;
        }

    }
}