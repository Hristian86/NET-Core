using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.interfaces;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using WebAppProject.Models;

namespace WebAppProject.Controllers
{
    public class MovieListController : Controller
    {

        //private readonly IViewMovies Mview;

        //public MovieListController(ViewMovies movie)
        //{
        //    this.Mview = movie;
        //}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Collection()
        {

            IViewMovies mov = new ViewMovies();

            var list = mov.GetListOfMovies();

            var display = new List<Movieses>();

            foreach (var item in list)
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