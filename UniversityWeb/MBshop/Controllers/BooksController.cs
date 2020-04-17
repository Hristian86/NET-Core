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
    [Authorize(Roles = "Admin")]
    public class BooksController : Controller
    {
        private readonly IAdminPanel adminPanel;

        public BooksController(IAdminPanel adminPanel)
        {
            this.adminPanel = adminPanel;
        }

        // GET: Books
        public IActionResult Index()
        {
            return this.View(this.adminPanel.GetBooks());
        }

        // GET: Books/Details
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
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
