using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLogic.interfaces;
using BusinessLogic.OutputModels;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MBshop.Controllers
{
    public class BookListController : Controller
    {
        private readonly IViewBooks books;
        private readonly IUserShopedProducts userItems;
        private readonly IShopItems shoping;
        private readonly Status status;

        public BookListController(IViewBooks books,
            IUserShopedProducts userItems,
            IShopItems shoping,
            Status status)
        {
            this.books = books;
            this.userItems = userItems;
            this.shoping = shoping;
            this.status = status;
        }


        public IActionResult BooksCollection()
        {
            var list = this.books.GetListOfBooks();

            if (User.Identity.Name != null)
            {

                //Get user id from cookies
                var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                //current user peronal books
                var userItm = userItems.PersonalBooks(user);

                if (userItm.Count != 0)
                {
                    //chek for possessed items in collections
                    status.StatusChekBooks(list, userItm);
                }

            }

            return this.View(list);

            //return View(this.books.GetListOfBooks());
        }

        [HttpGet]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public IActionResult PurchaseBook(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = books.GetListOfBooks()
                .FirstOrDefault(m => m.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> PurchaseBook(int id, [Bind("Id,Title,Author,Genre,UserId,RealeseDate,Created,Picture,price,Discount,Raiting,Description")] OutputBooks book)
        {

            var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (BookExists(book.Id))
                {
                    var movi = this.books.GetListOfBooks()
                        .Where(x => x.Id == book.Id && x.price == book.price)
                        .FirstOrDefault();

                    await this.shoping.BuyBook(user, book.Id);
                }
                else
                {
                    return NotFound();
                }

                return RedirectToAction("MovieCollection", "MovieList");

            }
            return View(book);
        }

        private bool BookExists(int id)
        {
            return this.books.GetListOfBooks()
                .Any(x => x.Id == id);
        }

    }
}