using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MBshop.Service;
using MBshop.Service.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MBshop.Models;
using MBshop.Data.Data;
using MBshop.Service.OutputModels;
using MBshop.Service.WebConstants;
using MBshop.Service.Services;
using System.Security.Claims;

namespace MBshop.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IProfileEdit edits;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ILogger<HomeController> _logger;
        private readonly ISearchEngine search;
        private string user = "";

        //private UserNames names = new UserNames();

        public HomeController(
            ILogger<HomeController> logger,
            IProfileEdit edit,
            UserManager<IdentityUser> userManager,
            ISearchEngine search,
            IUserShopedProducts userItems
            )
        {
            this.search = search;
            this.edits = edit;
            this.userManager = userManager;
            _logger = logger;
        }


        public IActionResult Index()
        {

            //ViewData["error"] = "This is error";

            if (User.Identity.Name != null)
            {

                var curUser = edits.GetUserProperties(User.Identity.Name);

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SearchResult(string searchItem)
        {
            

            if (searchItem != null)
            {
                if (User.Identity.Name != null)
                {
                    user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                }

                var result = search.Search(searchItem,user);

                if (result.Count() == 0)
                {
                    return this.RedirectToAction("Index", "Home");
                }

                return View(result);

            }

            return this.RedirectToAction("Index", "Home");
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