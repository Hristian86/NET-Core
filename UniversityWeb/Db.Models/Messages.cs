using System;
using System.Collections.Generic;

namespace Db.Models
{
    public partial class Messages
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
