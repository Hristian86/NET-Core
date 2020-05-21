using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ReactApp.Data.DBModels
{
    public class VideoCards : AllProducts
    {
        public VideoCards()
        {
            
        }

        [MaxLength(80)]
        public string Brand { get; set; }

        [MaxLength(80)]
        public string Model { get; set; }


    }
}
