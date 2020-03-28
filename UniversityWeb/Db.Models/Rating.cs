using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Db.Models
{
    public class Rating
    {

        [Key]
        public int Id { get; set; }

        public int? UserRateCount { get; set; }

        [DisplayFormat(DataFormatString = "{0:f1}", ApplyFormatInEditMode = true)]
        public double? RatingMovies { get; set; }

        [DisplayFormat(DataFormatString = "{0:f1}", ApplyFormatInEditMode = true)]
        public double? RatingBooks { get; set; }

        public string UserId { get; set; }

        public int? MoviesId { get; set; }

        public int? BooksId { get; set; }

        public virtual Books Books { get; set; }
        public virtual Movies Movies { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
