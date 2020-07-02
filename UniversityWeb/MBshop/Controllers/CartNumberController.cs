using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MBshop.Service.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MBshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartNumberController : ControllerBase
    {
        private readonly ICartService basket;

        public CartNumberController(ICartService basket)
        {
            this.basket = basket;
        }

        [Authorize]
        [HttpGet]
        public int CartCount()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            int count = this.basket.GetCartBasketUser(userId).Count();

            return count;
        }
    }
}