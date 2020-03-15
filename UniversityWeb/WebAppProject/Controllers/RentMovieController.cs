using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebAppProject.Controllers
{
    public class RentMovieController : Controller
    {

        public RentMovieController()
        {

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}