using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MBshopService.OutputModels
{
    public class OutputCart
    {
        
        public int Id { get; set; }

        [MaxLength(20)]
        public string Type { get; set; }

        public string Picture { get; set; }

        public string Title { get; set; }

        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public double price { get; set; }

    }
}
