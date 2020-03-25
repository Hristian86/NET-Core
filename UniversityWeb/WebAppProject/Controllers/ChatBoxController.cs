using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MBshop.Controllers
{
    
    public class ChatBoxController : Controller
    {
        public IActionResult ChatPanel()
        {
            return this.View();
        }



    }
}