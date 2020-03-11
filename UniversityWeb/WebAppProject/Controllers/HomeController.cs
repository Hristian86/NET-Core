using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using BusinessLogic.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAppProject.Data;
using WebAppProject.Models;

namespace WebAppProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProfileEdit edits;
        private readonly ILogger<HomeController> _logger;

        private UserNames names = new UserNames();

        public HomeController(ILogger<HomeController> logger,IProfileEdit injEdit)
        {
            this.edits = injEdit;
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.Identity.Name != null)
            {

                var usr = edits.GetUserProperties(User.Identity.Name);

                UserNames tempUser = new UserNames
                {
                    firstName = usr.FirstName,
                    LastName = usr.LastName,
                    Address = usr.Address
                };
                return View(tempUser);
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}