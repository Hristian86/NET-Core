using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniversityWeb.Models;

namespace UniversityWeb.Controllers
{
    public class HoAreWheController : Controller
    {
        //private List<MovieRequests> movies;
        public IActionResult AboutMe()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Auth()
        {
            return View("HoAreWhe" , "AboutMe");
        }
    }
}