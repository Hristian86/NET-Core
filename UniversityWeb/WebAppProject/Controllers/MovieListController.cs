﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.interfaces;
using BusinessLogic.Services;
using DataDomain.Data.Models;
using Microsoft.AspNetCore.Mvc;
using WebAppProject.Models;

namespace WebAppProject.Controllers
{
    public class MovieListController : Controller
    {
        private readonly List<Movies> Mview;

        public MovieListController(MovieRentalDBSEContext db)
        {
            this.Mview = db.Movies.ToList();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Collection()
        {
            var display = new List<Movieses>();
           
            foreach (var item in Mview)
            {
                Movieses movie = new Movieses
                {
                    Id = item.Id,
                    Title = item.Title,
                    Director = item.Director,
                    Genre = item.Genre,
                    Picture = item.Picture,
                    RealeaseDate = item.RealeaseDate,
                    RentMovieId = item.RentMovieId
                };

                display.Add(movie);
            }


            return this.View(display);
        }
    }
}