using System;
using System.Collections.Generic;

namespace WebAppProject.Models
{
    public class Movieses
    {
        public Movieses()
        {
            
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public DateTime RealeaseDate { get; set; }
        public int? RentMovieId { get; set; }
        public string Picture { get; set; }
        public string Genre { get; set; }

    }
}
