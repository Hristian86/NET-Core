using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MBshop.Models;
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
        private const int pageSize = 5;
        static string order = "";

        public ProductsController(
            ISearchEngineService search)
        {
            this.search = search;
        }


        public IActionResult ProductsCollection(string orderBy,int page = 1)
        {
            string user = "";

            if (User.Identity.Name != null)
            {
                //Get user id from cookies
                user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            if (orderBy == null || orderBy == "")
            {
                orderBy = order;
            }
            else
            {
                //ViewData["order"] = orderBy;
                order = orderBy;
            }
            
            var result = this.search.ViewProductsWithPage(user,orderBy,page);

            ProductsViewPageListingModel model = new ProductsViewPageListingModel();

            model.Products = result;
            model.CurrentPage = page;
            model.TotalPages = (int)Math.Ceiling(this.search.GetAllCount() / (double)pageSize); 

            return this.View(model);
        }
    }
}