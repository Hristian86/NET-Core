using System;
using System.Collections.Generic;

namespace DataDomain.Data.Models
{
    public partial class Logs
    {
        public int LogId { get; set; }
        public DateTime? DateLoged { get; set; } = DateTime.UtcNow;
        public string UserLoged { get; set; }
    }
}
