using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic.interfaces;
using BusinessLogic.OutputModels;
using DataDomain.Data;

namespace BusinessLogic.Services
{
    public class ViewBooks : IViewBooks
    {
        private readonly MovieShopDBSEContext _db;

        public ViewBooks(MovieShopDBSEContext db)
        {
            this._db = db;
        }

        public List<OutputBooks> GetListOfBooks()
        {
            return GetBooks();
        }

        private List<OutputBooks> GetBooks()
        {
            var booksDispplay = new List<OutputBooks>();

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
                    Raiting = itemBook.Raiting
                };

                booksDispplay.Add(book);
            }

            return booksDispplay;
        }
    }
}
