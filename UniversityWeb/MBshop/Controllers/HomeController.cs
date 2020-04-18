﻿using System;
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
        private readonly IProfileEditService edits;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ILogger<HomeController> _logger;
        private readonly ISearchEngineService search;
        private readonly ICartService cartBasket;
        private string user = "";

        //private UserNames names = new UserNames();

        public HomeController(
            ILogger<HomeController> logger,
            IProfileEditService edit,
            UserManager<IdentityUser> userManager,
            ISearchEngineService search,
            IUserShopedProductsService userItems,
            ICartService cartBasket
            )
        {
            this.search = search;
            this.cartBasket = cartBasket;
            this.edits = edit;
            this.userManager = userManager;
            _logger = logger;
        }


        public IActionResult Index()
        {

            //ViewData["error"] = "This is error";

            if (User.Identity.Name != null)
            {

                var curUser = this.edits.GetUserProperties(User.Identity.Name);

                //var usery = userManager.GetUserId(this.User);

                UserNames tempUser = new UserNames
                {
                    firstName = curUser.FirstName,
                    LastName = curUser.LastName,
                    Address = curUser.Address
                };
                //return View(tempUser);
            }

            string user = "";

            if (User.Identity.Name != null)
            {
                //Get user id from cookies
                user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            var result = this.search.ViewProducts(user);

            //Can be made orderBy

            return this.View(result.OrderByDescending(x => x.Rate).ThenBy(x => x.Title).Take(5).ToList());

            //return View();
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

                var result = this.search.Search(searchItem,user);

                ViewData["Search"] = searchItem;

                return this.View(result);

            }

            return this.View();
        }

        public IActionResult Privacy()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            //To Do error log in database

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}