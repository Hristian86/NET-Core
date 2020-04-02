using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MBshopService.interfaces;

namespace MBshopService.OutputModels
{
    public class OutputMovies
    {
        public OutputMovies()
        {
            
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

        public int? ShopsMovieId { get; set; }

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
        public string LinkForPurchasedContend { get; set; }
        public bool Status { get; set; }
    }
}
