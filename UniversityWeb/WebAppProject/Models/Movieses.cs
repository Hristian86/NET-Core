using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime RealeaseDate { get; set; }
        public int? RentMovieId { get; set; }
        public string Picture { get; set; }
        public string Genre { get; set; }

        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public double price { get; set; }

        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public double Discount { get; set; }
    }
}
