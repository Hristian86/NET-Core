using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLogic.interfaces;
using BusinessLogic.Services;
using DataDomain.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppProject.Models;

namespace WebAppProject.Controllers
{
    public class MovieListController : Controller
    {
        private readonly IViewMovies _mods;
        private readonly IShopItems _shoping;
        private readonly IUserShopedProducts userItems;
        private readonly Status status;

        public MovieListController(IViewMovies mods,
            IShopItems shoping,
            IUserShopedProducts userItems,
            Status status
            )
        {
            this._mods = mods;
            this._shoping = shoping;
            this.userItems = userItems;
            this.status = status;
        }

        public IActionResult MovieCollection()
        {
            var list = this._mods.GetListOfMovies();

            //user if from cookies
            var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            //current user peronal movies
            var userItm = userItems.PersonalMovies(user);

            if (userItm.Count == 0)
            {
                return this.View(list);
            }

            //chek for possesed items in collections
            status.StatusChek(list,userItm);

            return this.View(list);
        }
    }
}