using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using BusinessLogic.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MBshop.Models;
using Data.Domain.Data;
using BusinessLogic.OutputModels;

namespace MBshop.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IProfileEdit _edits;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ILogger<HomeController> _logger;
        private readonly IViewMovies _mods;

        //private UserNames names = new UserNames();

        public HomeController(
            ILogger<HomeController> logger,
            IProfileEdit edit,
            UserManager<IdentityUser> userManager,
            IViewMovies mods
            )
        {
            this._mods = mods;
            this._edits = edit;
            this.userManager = userManager;
            _logger = logger;
        }


        public IActionResult Index()
        {

            ViewData["error"] = "This is error";

            if (User.Identity.Name != null)
            {

                var curUser = _edits.GetUserProperties(User.Identity.Name);

                //var usery = userManager.GetUserId(this.User);

                UserNames tempUser = new UserNames
                {
                    firstName = curUser.FirstName,
                    LastName = curUser.LastName,
                    Address = curUser.Address
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