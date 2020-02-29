﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.UserRental
{
    public class UserRentals
    {
        [Key]
        public int Id { get; set; }

        public string MovieRented { get; set; }

        public DateTime Date { get; set; }

        public int MovieId { get; set; }

        public string IdentityMovieId { get; set; }

        //public List<Movies> RentMovies { get; set; }
    }
}
