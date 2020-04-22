using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MBshop.Models
{
    public class ChatModel
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string UserName { get; set; }

        public DateTime? DateT { get; set; }

        [MaxLength(99,ErrorMessage = "To long sentance")]
        public string Content { get; set; }

        [MaxLength(50)]
        public string CurrentUser { get; set; }

        public string Avatar { get; set; }
    }
}
