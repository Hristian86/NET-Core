using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MBshop.Service.interfaces;
using MBshop.Service.OutputModels;
using MBshop.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MBshop.Controllers
{
    public class BookListController : Controller
    {
        private readonly IViewBooksService books;
        private readonly IUserShopedProductsService userItems;
        private readonly IShopItemsService shoping;
        private readonly Status status;
        private List<OutputBooks> list = new List<OutputBooks>();

        public BookListController(IViewBooksService books,
            IUserShopedProductsService userItems,
            IShopItemsService shoping,
            Status status)
        {
            this.books = books;
            this.userItems = userItems;
            this.shoping = shoping;
            this.status = status;
        }


        public IActionResult BooksCollection(int orderBy, string searchItem)
        {

            if (searchItem != null)
            {
                List<OutputBooks> result = this.books.GetListOfBooks()
                    .Where(x => x.Title.ToLower().Contains(searchItem.ToLower()))
                    .ToList();

                return this.View(result);
            }

            this.list = this.books.SortBooks(orderBy);
            
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

            return this.View(this.list);

            //return View(this.books.GetListOfBooks());
        }

        [HttpGet]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public IActionResult BookDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = books.GetListOfBooks()
                .FirstOrDefault(m => m.Id == id);

            string user = "";

            if (User.Identity.Name != null)
            {
                //Get user id from cookies
                user = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                bool userBook = userItems.PersonalBooks(user).Any(x => x.Id == book.Id);

                if (userBook)
                {
                    book.Status = true;
                }
            }

            //To Do status chek for purchase

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

    }
}