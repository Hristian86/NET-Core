﻿using System;
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
        private readonly Status status;

        public BookListController(IViewBooksService books,
            IUserShopedProductsService userItems,
            Status status)
        {
            this.books = books;
            this.userItems = userItems;
            this.status = status;
        }

        [AllowAnonymous]
        public IActionResult BooksCollection(int orderBy, string searchItem)
        {
            var list = this.books.SortBooks(orderBy);
            
            if (User.Identity.Name != null)
            {

                //Get user id from cookies
                var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                //current user peronal books
                var userItm = this.userItems.PersonalBooks(user);

                if (userItm.Count != 0)
                {
                    //chek for possessed items in collections
                    this.status.StatusChekBooks(list, userItm);
                }

            }

            return this.View(list);
        }

        [HttpGet]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public IActionResult BookDetail(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error404Page", "Error404");
            }

            var book = this.books.GetListOfBooks()
                .FirstOrDefault(m => m.Id == id);

            string user = "";

            if (User.Identity.Name != null)
            {
                //Get user id from cookies
                user = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                bool userBook = this.userItems.PersonalBooks(user).Any(x => x.Id == book.Id);

                if (userBook)
                {
                    book.Status = true;
                }
            }

            //To Do status chek for purchase

            if (book == null)
            {
                return RedirectToAction("Error404Page", "Error404");
            }

            return this.View(book);
        }
    }
}