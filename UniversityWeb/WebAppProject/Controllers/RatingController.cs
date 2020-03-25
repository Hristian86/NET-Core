using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Db.Models;
using Microsoft.AspNetCore.Mvc;

namespace MBshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : Controller
    {
        public RatingController()
        {
        }
        
        public Movies Index()
        {
            Movies movie = new Movies
            {
                Title = "SHasda",
                Director = "Idk",
                Genre = "asd",
            };
            return movie;
        }
    }
}