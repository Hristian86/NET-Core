using System;
using System.Collections.Generic;

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
        public DateTime RealeaseDate { get; set; }
        public int? RentMovieId { get; set; }
        public string Picture { get; set; }

        public virtual ICollection<Rentals> Rentals { get; set; }
    }
}
