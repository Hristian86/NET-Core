using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ReactApp.Data.DBModels
{
    public class Reviews
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        [MaxLength(250)]
        public string Content { get; set; }

        public virtual User User { get; set; }
    }
}
