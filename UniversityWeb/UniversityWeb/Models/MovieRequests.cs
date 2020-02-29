using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityWeb.Models
{
    public class MovieRequests
    {
        [Required(ErrorMessage = "Movie is required for renting")]
        public string MovieName { get; set; }

        public string MovieTag { get; set; }

    }
}
