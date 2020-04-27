using System;
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
using AutoMapper;
using MBshop.Models.ViewMovies;

namespace MBshop.Controllers
{


    public class MoviesController : Controller
    {
        private readonly IAdminPanel adminProducts;
        private readonly IMapper mapper;
        private List<OutPutViewMovies> movieMap; //= new List<OutPutViewMovies>();

        public MoviesController(IAdminPanel adminProducts,
            IMapper mapper)
        {
            this.movieMap = new List<OutPutViewMovies>();
            this.adminProducts = adminProducts;
            this.mapper = mapper;
        }


        // GET: Movies
        [Authorize(Roles = "Admin,Moderator")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Index()
        {

            var movies = this.adminProducts.GetMovies().ToList();

            if (movies == null)
            {
                return RedirectToAction("Error404Page", "Error404");
            }

            for (int i = 0; i < movies.Count(); i++)
            {

                var moviesViewModel = this.mapper.Map<OutPutViewMovies>(movies[i]);

                this.movieMap.Add(moviesViewModel);
            }

            return this.View(this.movieMap);
        }

        // GET: Movies/Details/5
        [Authorize(Roles = "Admin,Moderator")]
        [ValidateAntiForgeryToken]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error404Page", "Error404");
            }

            var moviesViewModel = this.mapper.Map<OutPutViewMovies>(await this.adminProducts.FindMovieById(id));

            if (moviesViewModel == null)
            {
                return RedirectToAction("Error404Page", "Error404");
            }

            return View(moviesViewModel);
        }

        // GET: Movies/Create
        [Authorize(Roles = "Admin,Moderator")]
        [ValidateAntiForgeryToken]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        [Authorize(Roles = "Admin,Moderator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Director,RealeaseDate,Genre,price,Discount,Picture,Actors,Raiting,Description,LinkForProductContentWhenPurchase,Rate")] OutPutViewMovies movies)
        {

            if (ModelState.IsValid)
            {

                var moviesViewModel = this.mapper.Map<Movies>(movies);

                GlobalAlertMessages.StatusMessage = await this.adminProducts.CreateMovie(moviesViewModel);

                return RedirectToAction(nameof(Index));
            }

            return View(movies);

        }

        // GET: Movies/Edit/5
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error404Page", "Error404");
            }

            var moviesViewModel = this.mapper.Map<OutPutViewMovies>(await this.adminProducts.FindMovieById(id));

            if (moviesViewModel == null)
            {
                return RedirectToAction("Error404Page", "Error404");
            }

            return View(moviesViewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Director,RealeaseDate,Genre,price,LinkForProductContentWhenPurchase,Discount,Picture,Actors,Raiting,Description,Rate")] OutPutViewMovies movies)
        {
            if (id != movies.Id)
            {
                return RedirectToAction("Error404Page", "Error404");
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var moviesViewModel = this.mapper.Map<Movies>(movies);

                    GlobalAlertMessages.StatusMessage = await this.adminProducts.UpdateMovie(moviesViewModel);

                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!MoviesExists(movies.Id))
                    {
                        return RedirectToAction("Error404Page", "Error404");
                    }
                    else
                    {
                        throw new DbUpdateConcurrencyException("Problem with updaiting db");
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movies);
        }

        // GET: Movies/Delete/5
        [Authorize(Roles = "Admin")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error404Page", "Error404");
            }

            var moviesViewModel = this.mapper.Map<OutPutViewMovies>(await this.adminProducts.FindMovieById(id));

            if (moviesViewModel == null)
            {
                return RedirectToAction("Error404Page", "Error404");
            }

            return View(moviesViewModel);
        }

        // POST: Movies/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AutoValidateAntiforgeryToken]
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