using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MBshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatBoxController : Controller
    {
        public IActionResult ChatPanel()
        {
            return View();
        }
    }
}