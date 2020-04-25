using System;
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
        [Authorize(Roles = "Admin,Moderator")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shops = await db.Shops.FindAsync(id);
            db.Shops.Remove(shops);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "Admin,Moderator")]
        public IActionResult Logs()
        {

            var logs = this.db.Logs
                .Select(x => x)
                .ToList();

            return this.View(logs);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("DeleteLog")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteLog(string name, string userName,string hooks, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userLog = this.db.Logs.Where(x => x.UserLoged == userName && x.LogId == id).FirstOrDefault();

            if (name == "%name-no-name%" && userLog.UserLoged == userName && hooks == "%sid-ni-as-no-one%" && userLog.LogId == id)
            {
                this.db.Logs.Remove(userLog);

                await this.db.SaveChangesAsync();
            }

            return this.RedirectToAction("Logs", "Shops");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("DeleteLogs")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteLogs(string name, string userName, string hooks, int? id)
        {
            
            var allLogs = this.db.Logs
                .Where(x => x.UserLoged != null)
                .ToList();

                this.db.Logs.RemoveRange(allLogs);

                await this.db.SaveChangesAsync();
            

            return this.RedirectToAction("Logs", "Shops");
        }

        private bool ShopsExists(int id)
        {
            return db.Shops.Any(e => e.Id == id);
        }

    }
}