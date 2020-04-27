using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MBshop.Models.ViewMovies
{
    public class OutPutViewMovies
    {

        public OutPutViewMovies()
        {
            Shops = new HashSet<Shops>();
        }

        public int Id { get; set; }

        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(30)]
        public string Director { get; set; }

        [MaxLength(100)]
        public string Actors { get; set; }

        [MaxLength(150)]
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime RealeaseDate { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;

        public int? ShopsMovieId { get; set; }

        public string LinkForProductContentWhenPurchase { get; set; }

        public string Picture { get; set; }

        [MaxLength(50)]
        public string Genre { get; set; }

        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public double price { get; set; }

        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public double Discount { get; set; }

        [DisplayFormat(DataFormatString = "{0:f1}", ApplyFormatInEditMode = true)]
        public double? Raiting { get; set; }

        [DisplayFormat(DataFormatString = "{0:f1}", ApplyFormatInEditMode = true)]
        public double? Rate { get; set; }


        public virtual ICollection<Shops> Shops { get; set; }

        public virtual ICollection<Rating> Rating { get; set; }
            = new HashSet<Rating>();
    }
}
