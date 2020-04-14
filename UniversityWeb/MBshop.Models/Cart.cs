using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MBshop.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(20)]
        public string Type { get; set; }

        [MaxLength(50)]
        public string Genre { get; set; }

        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public double? Rate { get; set; }

        public string Picture { get; set; }

        [MaxLength(50)]
        public string Title { get; set; }

        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public double price { get; set; }

        public bool Status { get; set; }

        public string UserId { get; set; }

        public AspNetUsers User { get; set; }

        public int ProductId { get; set; }

        //public int? MovieId { get; set; }

        //public Movies Movie { get; set; }

        //public int? BookId { get; set; }

        //public Books Book { get; set; }
    }
}
