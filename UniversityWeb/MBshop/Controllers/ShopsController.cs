﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MBshop.Models;
using Microsoft.AspNetCore.Authorization;
using MBshop.Data.Data;
using System.Security.Claims;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace MBshop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ShopsController : Controller
    {
        private readonly MovieShopDBSEContext db;
        private readonly UserManager<IdentityUser> userManager;

        public ShopsController(MovieShopDBSEContext db,
            UserManager<IdentityUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        // GET: Shops
        public async Task<IActionResult> Index()
        {
            var movieShopDBSEContext = db.Shops.Include(s => s.Books).Include(s => s.Movie).Include(s => s.User);

            var cookie = new CookieHeaderValue("session-id", "12345");



            if (User.Identity.Name != null)
            {

                var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email,"ho knows"),
                    new Claim("Hello","Hi")

                };

                var userIdentity = new ClaimsIdentity(userClaims, "User Identity");

                var userPrincipal = new ClaimsPrincipal(new[] { userIdentity });

                await HttpContext.SignInAsync(userPrincipal);

                //var usery = await userManager.GetUserAsync(this.User);

                //var claimses = await userManager.GetClaimsAsync(usery);

                

                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var role = User.FindFirst(ClaimTypes.Role).Value;
                var name = User.FindFirst(ClaimTypes.Name).Value;

                ViewData["Cookie"] = "User: " + userId.ToString() + " Role: " + role.ToString()
                + " Name: " + name.ToString();
            }
            //var cookie = HttpContext.Response.Cookies;


            return View(await movieShopDBSEContext.ToListAsync());
        }

        // GET: Shops/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shops = await db.Shops
                .Include(s => s.Books)
                .Include(s => s.Movie)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shops == null)
            {
                return NotFound();
            }

            return View(shops);
        }

        // POST: Shops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shops = await db.Shops.FindAsync(id);
            db.Shops.Remove(shops);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShopsExists(int id)
        {
            return db.Shops.Any(e => e.Id == id);
        }
    }
}