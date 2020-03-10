using System;
using System.Collections.Generic;

namespace DataDomain.Data.Models
{
    public partial class Rentalses
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int? MovieId { get; set; }

        public virtual Movies Movie { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
