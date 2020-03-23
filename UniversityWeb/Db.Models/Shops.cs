using System;
using System.Collections.Generic;

namespace Db.Models
{
    public partial class Shops
    {
        public int Id { get; set; }
        public double ShopedPrice { get; set; }
        public string UserId { get; set; }
        public int? MovieId { get; set; }
        public DateTime? RentedTime { get; set; } = DateTime.UtcNow;
        public int? BooksId { get; set; }

        public virtual Books Books { get; set; }
        public virtual Movies Movie { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
