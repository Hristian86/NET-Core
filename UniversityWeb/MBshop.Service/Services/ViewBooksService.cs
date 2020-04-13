using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBshop.Service.interfaces;
using MBshop.Service.OutputModels;
using MBshop.Data.Data;

namespace MBshop.Service.Services
{
    public class ViewBooksService : IViewBooksService
    {
        private readonly MovieShopDBSEContext db;

        public ViewBooksService(MovieShopDBSEContext db)
        {
            this.db = db;
        }

        /// <summary>
        /// Mapped books to output model
        /// </summary>
        /// <returns></returns>
        public List<OutputBooks> GetListOfBooks() => this.db
            .Books
            .Select(itemBook => new OutputBooks
        {
            Id = itemBook.Id,
            Title = itemBook.Title,
            Author = itemBook.Author,
            Genre = itemBook.Genre,
            Picture = itemBook.Picture,
            Discount = itemBook.Discount,
            price = itemBook.price,
            RealeseDate = itemBook.RealeseDate,
            Description = itemBook.Description,
            Raiting = itemBook.Raiting,
            Rate = itemBook.Rate,
            LinkForProductContentWhenPurchase = itemBook.LinkForProductContentWhenPurchase
        }).ToList();

        /// <summary>
        /// Sorting books
        /// </summary>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<OutputBooks> SortBooks(int orderBy)
        {
            if (orderBy == 1)
            {
                return GetListOfBooks().OrderBy(x => x.Title).ToList();
            }
            else if (orderBy == 2)
            {
                return GetListOfBooks().OrderByDescending(x => x.Title).ToList();
            }
            else if (orderBy == 3)
            {
                return GetListOfBooks().OrderBy(x => x.price).ToList();
            }
            else if (orderBy == 4)
            {
                return GetListOfBooks().OrderByDescending(x => x.price).ToList();
            }
            else
            {
                return GetListOfBooks();
            }
        }
    }
}