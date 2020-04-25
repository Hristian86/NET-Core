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

namespace MBshop.Controllers
{
    
    public class BooksController : Controller
    {
        private readonly IAdminPanel adminPanel;

        public BooksController(IAdminPanel adminPanel)
        {
            this.adminPanel = adminPanel;
        }

        // GET: Books
        [Authorize(Roles = "Admin,Moderator")]
        public IActionResult Index()
        {
            return this.View(this.adminPanel.GetBooks());
        }

        // GET: Books/Details
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books = await this.adminPanel.FindBookById(id);

            if (books == null)
            {
                return NotFound();
            }

            return View(books);
        }

        // GET: Books/Create
        [Authorize(Roles = "Admin,Moderator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [Authorize(Roles = "Admin,Moderator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Author,Genre,UserId,RealeseDate,Created,Picture,price,Discount,Raiting,Description,LinkForProductContentWhenPurchase")] Books books)
        {
            if (ModelState.IsValid)
            {

                GlobalAlertMessages.StatusMessage = await this.adminPanel.CreateBook(books);

                return RedirectToAction(nameof(Index));
            }
            return View(books);
        }

        // GET: Books/Edit
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books = await this.adminPanel.FindBookById(id);

            if (books == null)
            {
                return NotFound();
            }
            return View(books);
        }

        // POST: Books/Edit
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author,Genre,UserId,RealeseDate,Created,Picture,price,Discount,Raiting,Description,LinkForProductContentWhenPurchase")] Books books)
        {
            if (id != books.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    GlobalAlertMessages.StatusMessage = await this.adminPanel.UpdateBook(books);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BooksExists(books.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(books);
        }

        // GET: Books/Delete
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books = await this.adminPanel.FindBookById(id);

            if (books == null)
            {
                return NotFound();
            }

            return View(books);
        }

        // POST: Books/Delete
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
