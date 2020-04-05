using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MBshop.Service.OutputModels
{
    public class ViewProducts
    {
        
        public int Id { get; set; }

        [MaxLength(20)]
        public string Type { get; set; }

        [MaxLength(50)]
        public string Genre { get; set; }

        [DisplayFormat(DataFormatString = "{0:f1}", ApplyFormatInEditMode = true)]
        public double? Rate { get; set; }

        public string Picture { get; set; }

        [MaxLength(50)]
        public string Title { get; set; }

        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public double price { get; set; }

        public bool Status { get; set; }

    }
}
