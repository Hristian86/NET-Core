using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityWeb.Data;
using UniversityWeb.Models;

namespace UniversityWeb.Controllers
{
    public class MovieRentalController : Controller
    {
        //private List<Movies> movieList;
        //public MovieRentalController(Movies movi)
        //{

        //    movieList = new List<Movies>();
        //}

        // GET: MovieRental
        public ActionResult Index()
        {
            return View();
        }

        // GET: MovieRental/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MovieRental/Create
        public ActionResult CreateM()
        {
            return View();
        }

        // POST: MovieRental/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(Movies movies)
        {
            try
            {
                var movieses = new Movies()
                {
                    MovieName = movies.MovieName,
                    MovieAuthor = movies.MovieAuthor,
                    MovieRating = movies.MovieRating,
                    MovieRelease = DateTime.Now
                };

                MovieRentalContext mov = new MovieRentalContext();


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MovieRental/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MovieRental/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MovieRental/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MovieRental/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}