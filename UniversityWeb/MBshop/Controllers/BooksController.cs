using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MBshop.Models;
using Microsoft.AspNetCore.Authorization;
using MBshop.Service.interfaces;
using MBshop.Service.StaticProperyes;
using AutoMapper;
using MBshop.Models.ViewBooks;

namespace MBshop.Controllers
{
    
    public class BooksController : Controller
    {
        private readonly IAdminPanel adminPanel;
        private readonly IMapper mapper;
        private List<OutPutViewBooks> booksMap;

        public BooksController(IAdminPanel adminPanel,
            IMapper mapper)
        {
            booksMap = new List<OutPutViewBooks>();
            this.adminPanel = adminPanel;
            this.mapper = mapper;
        }

        // GET: Books
        [Authorize(Roles = "Admin,Moderator")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Index()
        {
            var books = this.adminPanel.GetBooks();

            if (books == null)
            {
                return RedirectToAction("Error404Page", "Error404");
            }

            for (int i = 0; i < books.Count(); i++)
            {

                var booksViewModel = this.mapper.Map<OutPutViewBooks>(books[i]);

                this.booksMap.Add(booksViewModel);
            }

            return this.View(this.booksMap);
        }

        // GET: Books/Details
        [Authorize(Roles = "Admin,Moderator")]
        [ValidateAntiForgeryToken]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error404Page", "Error404");
            }

            var booksViewModel = this.mapper.Map<OutPutViewBooks>(await this.adminPanel.FindBookById(id));

            if (booksViewModel == null)
            {
                return RedirectToAction("Error404Page", "Error404");
            }

            return this.View(booksViewModel);
        }

        // GET: Books/Create
        [Authorize(Roles = "Admin,Moderator")]
        [ValidateAntiForgeryToken]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [Authorize(Roles = "Admin,Moderator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Author,Genre,UserId,RealeseDate,Created,Picture,price,Discount,Raiting,Description,LinkForProductContentWhenPurchase")] OutPutViewBooks books)
        {
            if (ModelState.IsValid)
            {
                var booksViewModel = this.mapper.Map<Books>(books);

                GlobalAlertMessages.StatusMessage = await this.adminPanel.CreateBook(booksViewModel);

                return RedirectToAction(nameof(Index));
            }

            return this.View(books);
        }

        // GET: Books/Edit
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error404Page", "Error404");
            }

            var booksViewModel = this.mapper.Map<OutPutViewBooks>(await this.adminPanel.FindBookById(id));


            if (booksViewModel == null)
            {
                return RedirectToAction("Error404Page", "Error404");
            }
            return this.View(booksViewModel);
        }

        // POST: Books/Edit
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author,Genre,UserId,RealeseDate,Created,Picture,price,Discount,Raiting,Description,LinkForProductContentWhenPurchase")] OutPutViewBooks books)
        {
            if (id != books.Id)
            {
                return RedirectToAction("Error404Page", "Error404");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var book = this.mapper.Map<Books>(books);

                    GlobalAlertMessages.StatusMessage = await this.adminPanel.UpdateBook(book);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BooksExists(books.Id))
                    {
                        return RedirectToAction("Error404Page", "Error404");
                    }
                    else
                    {
                        throw new DbUpdateConcurrencyException("Problem is in db");
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return this.View(books);
        }

        // GET: Books/Delete
        [Authorize(Roles = "Admin")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error404Page", "Error404");
            }

            var booksViewModel = this.mapper.Map<OutPutViewBooks>(await this.adminPanel.FindBookById(id));

            if (booksViewModel == null)
            {
                return RedirectToAction("Error404Page", "Error404");
            }

            return this.View(booksViewModel);
        }

        // POST: Books/Delete
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //fixing cascade delete manualy 

            GlobalAlertMessages.StatusMessage = await this.adminPanel.RemoveBook(id);

            return RedirectToAction(nameof(Index));
        }

        private bool BooksExists(int id)
        {
            return this.adminPanel.BookExist(id);
        }
    }
}