using System;
using System.Collections.Generic;

namespace DataDomain.Data.Models
{
    public partial class RentalsBooks
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int? BooksId { get; set; }
        public DateTime? RentedDate { get; set; }

        public virtual Books Books { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
