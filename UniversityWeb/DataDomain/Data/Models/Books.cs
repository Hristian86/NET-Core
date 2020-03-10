using System;
using System.Collections.Generic;

namespace DataDomain.Data.Models
{
    public partial class Books
    {
        public Books()
        {
            RentalsBooks = new HashSet<RentalsBooks>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string UserId { get; set; }
        public DateTime? RealeseDate { get; set; }
        public string Picture { get; set; }

        public virtual ICollection<RentalsBooks> RentalsBooks { get; set; }
    }
}
