using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataDomain.Data.Models
{
    public partial class Movies
    {
        public Movies()
        {
            Rentals = new HashSet<Rentals>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime RealeaseDate { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public int? RentMovieId { get; set; }
        public string Picture { get; set; }
        public string Genre { get; set; }

        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public double price { get; set; }

        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public double Discount { get; set; }

        public virtual ICollection<Rentals> Rentals { get; set; }
    }
}
