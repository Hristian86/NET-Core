using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessLogic.OutputModels
{
    public class Bookses
    {
        public Bookses()
        {
            
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
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
    }
}
