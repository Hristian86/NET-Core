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

        public List<Bookses> GetListOfBooks()
        {
            return GetBooks();
        }

        private List<Bookses> GetBooks()
        {
            var booksDispplay = new List<Bookses>();

            var Bview = this._db.Books.ToList();

            foreach (var itemBook in Bview)
            {
                Bookses book = new Bookses
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
                    Raiting = itemBook.Raiting
                };

                booksDispplay.Add(book);
            }
            return booksDispplay;
        }
    }
}
