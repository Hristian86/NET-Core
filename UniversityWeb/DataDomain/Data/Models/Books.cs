using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataDomain.Data.Models
{
    public partial class Books
    {
        public Books()
        {
            Rentals = new HashSet<Rentals>();
        }

        public int Id { get; set; }

        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(30)]
        public string Author { get; set; }

        [MaxLength(50)]
        public string Genre { get; set; }

        [MaxLength(100)]
        public string Actors { get; set; }

        public string UserId { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? RealeseDate { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public string Picture { get; set; }

        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public double price { get; set; }

        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public double Discount { get; set; }
        public double Raiting { get; set; }

        [MaxLength(150)]
        public string Description { get; set; }

        public virtual ICollection<Rentals> Rentals { get; set; }
    }
}
