using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBshopService.interfaces;
using MBshopService.OutputModels;
using Data.Domain.Data;

namespace MBshopService.Services
{
    public class ViewBooks : IViewBooks
    {
        private readonly MovieShopDBSEContext _db;
        private List<OutputBooks> booksDispplay;

        public ViewBooks(MovieShopDBSEContext db)
        {
            booksDispplay = new List<OutputBooks>();
            this._db = db;
        }

        public List<OutputBooks> GetListOfBooks()
        {
            return GetBooks();
        }

        private List<OutputBooks> GetBooks()
        {
            //var booksDispplay = new List<OutputBooks>();

            //var Bview = this._db.Books.ToList();

            foreach (var itemBook in this._db.Books)
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
                    Description = itemBook.Description,
                    Raiting = itemBook.Raiting,
                    Rate = itemBook.Rate,
                    LinkForProductContentWhenPurchase = itemBook.LinkForProductContentWhenPurchase

                };

                this.booksDispplay.Add(book);
            }

            return this.booksDispplay;
        }

        public List<OutputBooks> SortBooks(int orderBy)
        {
            if (orderBy == 1)
            {
                return GetBooks().OrderBy(x => x.Title).ToList();
            }
            else if (orderBy == 2)
            {
                return GetBooks().OrderByDescending(x => x.Title).ToList();
            }
            else if (orderBy == 3)
            {
                return GetBooks().OrderBy(x => x.price).ToList();
            }
            else if (orderBy == 4)
            {
                return GetBooks().OrderByDescending(x => x.price).ToList();
            }
            else
            {
                return GetBooks();
            }
        }
    }
}