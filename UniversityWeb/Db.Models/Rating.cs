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

        public int UserRata { get; set; }

        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public double FinalRatingForMovies { get; set; }

        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public double FinalRatingForBooks { get; set; }

        public int UserId { get; set; }

        public int? MoviesId { get; set; }

        public int? BooksId { get; set; }

        public virtual Books Books { get; set; }
        public virtual Movies Movies { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
