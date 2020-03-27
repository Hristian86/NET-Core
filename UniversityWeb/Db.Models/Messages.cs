using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Db.Models
{
    public partial class Messages
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? DateT { get; set; } = DateTime.UtcNow;

        public string Content { get; set; }

        public string Avatar { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
