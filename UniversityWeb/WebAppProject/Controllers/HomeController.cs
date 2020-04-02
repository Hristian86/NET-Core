using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MBshopService;
using MBshopService.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MBshop.Models;
using Data.Domain.Data;
using MBshopService.OutputModels;
using MBshopService.WebConstants;

namespace MBshop.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IProfileEdit edits;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ILogger<HomeController> _logger;
        private readonly IViewMovies movies;
        private readonly IViewBooks books;
        private List<ViewProducts> allProducts = new List<ViewProducts>();

        //private UserNames names = new UserNames();

        public HomeController(
            ILogger<HomeController> logger,
            IProfileEdit edit,
            UserManager<IdentityUser> userManager,
            IViewMovies movies,
            IViewBooks books
            )
        {
            this.movies = movies;
            this.books = books;
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


                foreach (var item in this.movies.GetListOfMovies())
                {
                    ViewProducts product = new ViewProducts
                    {
                        Id = item.Id,
                        Title = item.Title,
                        price = item.price,
                        Picture = item.Picture,
                        Genre = item.Genre,
                        Status = item.Status,
                        Rate = item.Rate,
                        Type = WebConstansVariables.Movie
                    };

                    this.allProducts.Add(product);
                }

                foreach (var item in this.books.GetListOfBooks())
                {
                    ViewProducts product = new ViewProducts
                    {
                        Id = item.Id,
                        Title = item.Title,
                        price = item.price,
                        Picture = item.Picture,
                        Genre = item.Genre,
                        Status = item.Status,
                        Rate = item.Rate,
                        Type = WebConstansVariables.Book
                    };

                    this.allProducts.Add(product);
                }

                if (searchItem != null)
                {

                    var result = this.allProducts.Where(x => x.Title.ToLower().Contains(searchItem.ToLower())).ToList();

                    return View(result);
                }

            }

            return this.View(this.allProducts);
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