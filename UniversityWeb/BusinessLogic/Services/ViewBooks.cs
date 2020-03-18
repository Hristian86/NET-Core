using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic.interfaces;
using BusinessLogic.OutputModels;
using DataDomain.Data.Models;

namespace BusinessLogic.Services
{
    public class ViewBooks : IViewBooks
    {
        private readonly MovieRentalDBSEContext _db;

        public ViewBooks(MovieRentalDBSEContext db)
        {
            this._db = db;
        }

        public IReadOnlyList<Bookses> GetListOfBooks()
        {
            return GetBooks();
        }

        private IReadOnlyList<Bookses> GetBooks()
        {
            var booksDispplay = new List<Bookses>();

            var Mview = this._db.Books.ToList();

            foreach (var item in Mview)
            {
                Bookses book = new Bookses
                {
                    Id = item.Id,
                    Title = item.Title,
                    Author = item.Author,
                    Genre = item.Genre,
                    Picture = item.Picture,
                    Discount = item.Discount,
                    price = item.price,
                    RealeseDate = item.RealeseDate,
                };

                booksDispplay.Add(book);
            }
            return booksDispplay;
        }
    }
}
