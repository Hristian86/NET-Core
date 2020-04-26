using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MBshop.Areas.Identity.Pages.Account;
using MBshop.Data.Data;
using MBshop.Service.Services;
using MBshop.Service.StaticProperyes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MBshop.Areas.Administration.Controllers
{
    [Area("Home")]
    public class HomeController : Controller
    {

        public HomeController()
        {

        }

        [Authorize(Roles = "Admin,Moderator")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Index()
        {
            return this.View();
        }

    }
}