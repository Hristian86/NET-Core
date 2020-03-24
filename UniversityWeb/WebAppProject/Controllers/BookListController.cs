using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MBshop.Controllers
{
    public class BookListController : Controller
    {
        private readonly IViewBooks _books;

        public BookListController(IViewBooks books)
        {
            this._books = books;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BooksCollection()
        {
            return View(this._books.GetListOfBooks());
        }

    }
}