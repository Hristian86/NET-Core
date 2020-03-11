﻿using System;
using System.Collections.Generic;

namespace DataDomain.Data.Models
{
    public partial class Rentals
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int? MovieId { get; set; }
        public DateTime? RentedTime { get; set; }
        public int? BooksId { get; set; }

        public virtual Books Books { get; set; }
        public virtual Movies Movie { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
