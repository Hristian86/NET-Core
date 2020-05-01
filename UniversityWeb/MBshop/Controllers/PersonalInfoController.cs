using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MBshop.Controllers
{
    public class PersonalInfoController : Controller
    {
        public IActionResult ContactInfo() => this.View();
    }
}