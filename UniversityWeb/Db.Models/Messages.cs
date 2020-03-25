using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Db.Models
{
    public partial class Messages
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public DateTime DateT { get; set; } = DateTime.UtcNow;

        [MaxLength(250)]
        public string Content { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
