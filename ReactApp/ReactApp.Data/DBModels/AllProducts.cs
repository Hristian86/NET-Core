using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReactApp.Data.DBModels
{
    public class AllProducts
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(80)]
        public string Title { get; set; }

        [MaxLength(30)]
        public string Type { get; set; }

        public double Price { get; set; }

        public bool Status { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public int? ReviewsId { get; set; }

        public virtual HashSet<Reviews> Reviews { get; set; } = new HashSet<Reviews>();

    }
}
