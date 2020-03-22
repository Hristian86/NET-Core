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

        public MovieListController(IViewMovies mods,
            IShopItems shoping,
            IUserShopedProducts userItems
            )
        {
            this._mods = mods;
            this._shoping = shoping;
            this.userItems = userItems;
        }

        public IActionResult MovieCollection()
        {
            var list = this._mods.GetListOfMovies();

            var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var userItm = userItems.PersonalMovies(user);

            if (userItm.Count == 0)
            {
                return this.View(list);
            }

            for (int i = 0; i < list.Count; i++)
            {
                var curMovie = list[i];

                for (int j = 0; j < userItm.Count; j++)
                {
                    var userMovies = userItm[j];
                    if (curMovie.Id == userMovies.Id)
                    {
                        curMovie.Status = true;
                    }
                }

            }

            return this.View(list);

            //return this.View(this._mods.GetListOfMovies()
            //    .OrderBy(x => x.Title));
        }
    }
}