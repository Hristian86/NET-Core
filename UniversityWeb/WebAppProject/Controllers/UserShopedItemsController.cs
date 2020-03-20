using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLogic.interfaces;
using BusinessLogic.OutputModels;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAppProject.Controllers
{
    public class UserShopedItemsController : Controller
    {
        private readonly IUserShopedProducts products;

        public UserShopedItemsController(IUserShopedProducts products)
        {
            this.products = products;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserShops()
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (user == null)
            {
                return NotFound();
            }
            IEnumerable<OutputMovies> movi = products.PersonalItems(user);
            return View(movi);
        }

    }
}