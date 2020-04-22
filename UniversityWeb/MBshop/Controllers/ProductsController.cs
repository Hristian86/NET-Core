using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MBshop.Service.interfaces;
using MBshop.Service.OutputModels;
using MBshop.Service.Services;
using MBshop.Service.WebConstants;
using Microsoft.AspNetCore.Mvc;

namespace MBshop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ISearchEngineService search;

        public ProductsController(
            ISearchEngineService search)
        {
            this.search = search;
        }


        public IActionResult ProductsCollection(string orderBy)
        {
            string user = "";

            if (User.Identity.Name != null)
            {
                //Get user id from cookies
                user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            var result = this.search.ViewProducts(user,orderBy);

            return this.View(result);
        }
    }
}