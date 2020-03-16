using System;
using System.Collections.Generic;

namespace DataDomain.Data.Models
{
    public partial class Books
    {
        public Books()
        {
            Rentals = new HashSet<Rentals>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string UserId { get; set; }
        public DateTime? RealeseDate { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public string Picture { get; set; }
        public float price { get; set; }
        public float Discount { get; set; }

        public virtual ICollection<Rentals> Rentals { get; set; }
    }
}
