﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MBshop.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using MBshop.Data;
using Microsoft.AspNetCore.Authorization;
using MBshop.Service.interfaces;
using MBshop.Service.StaticProperyes;

namespace MBshop.Controllers
{

    
    public class MoviesController : Controller
    {
        private readonly IAdminPanel adminProducts;

        public MoviesController(IAdminPanel adminProducts)
        {
            this.adminProducts = adminProducts;
        }


        // GET: Movies
        [Authorize(Roles = "Admin,Moderator")]
        public IActionResult Index()
        {
            return this.View(this.adminProducts.GetMovies());
        }

        // GET: Movies/Details/5
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movies = await this.adminProducts.FindMovieById(id);

            if (movies == null)
            {
                return NotFound();
            }

            return View(movies);
        }

        // GET: Movies/Create
        [Authorize(Roles = "Admin,Moderator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        [Authorize(Roles = "Admin,Moderator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Director,RealeaseDate,Genre,price,Discount,Picture,Actors,Raiting,Description,LinkForProductContentWhenPurchase,Rate")] Movies movies)
        {

            if (ModelState.IsValid)
            {

               GlobalAlertMessages.StatusMessage = await this.adminProducts.CreateMovie(movies);

                return RedirectToAction(nameof(Index));
            }
            return View(movies);

        }

        // GET: Movies/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movies = await this.adminProducts.FindMovieById(id);

            if (movies == null)
            {
                return NotFound();
            }

            return View(movies);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Director,RealeaseDate,Genre,price,LinkForProductContentWhenPurchase,Discount,Picture,Actors,Raiting,Description,Rate")] Movies movies)
        {
            if (id != movies.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    GlobalAlertMessages.StatusMessage = await this.adminProducts.UpdateMovie(movies);

                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!MoviesExists(movies.Id))
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
            return View(movies);
        }

        // GET: Movies/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movies = await this.adminProducts.FindMovieById(id);

            if (movies == null)
            {
                return NotFound();
            }

            return View(movies);
        }

        // POST: Movies/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            GlobalAlertMessages.StatusMessage = await adminProducts.RemoveMovie(id);

            return RedirectToAction(nameof(Index));
        }

        private bool MoviesExists(int id)
        {
            return this.adminProducts.MovieExist(id);
        }
    }
}