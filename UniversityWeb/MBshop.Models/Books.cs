using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MBshop.Models
{
    public partial class Books
    {
        public Books()
        {
            Shops = new HashSet<Shops>();
        }

        public int Id { get; set; }

        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(30)]
        public string Author { get; set; }

        [MaxLength(50)]
        public string Genre { get; set; }

        public string UserId { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? RealeseDate { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public string Picture { get; set; }

        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public double price { get; set; }

        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public double Discount { get; set; }

        [DisplayFormat(DataFormatString = "{0:f1}", ApplyFormatInEditMode = true)]
        public double Raiting { get; set; }

        [DisplayFormat(DataFormatString = "{0:f1}", ApplyFormatInEditMode = true)]
        public double? Rate { get; set; }

        [MaxLength(150)]
        public string Description { get; set; }

        public bool Status { get; set; }

        public string LinkForProductContentWhenPurchase { get; set; }

        public virtual ICollection<Shops> Shops { get; set; }

        public virtual ICollection<Rating> Rating { get; set; }
            = new HashSet<Rating>();
    }
}
