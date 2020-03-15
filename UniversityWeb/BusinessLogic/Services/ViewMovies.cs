using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic.interfaces;
using DataDomain.Data.Models;

namespace BusinessLogic.Services
{
    public class ViewMovies : IViewMovies
    {
        //private readonly MovieRentalDBSEContext db = new MovieRentalDBSEContext();
        private readonly List<Movies> movies;

        public ViewMovies(MovieRentalDBSEContext dbs)
        {
            //this.db = dbs;
            movies = dbs.Movies.ToList();
        }

        public List<Movies> GetListOfMovies()
        {
            //CopyMovies();
            return this.movies;
        }

        //private void CopyMovies()
        //{
        //    this.movies = this.db.Movies.ToList();
        //}
    }
}
