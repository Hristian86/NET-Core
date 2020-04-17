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

namespace MBshop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ShopsController : Controller
    {
        private readonly MovieShopDBSEContext db;

        public ShopsController(MovieShopDBSEContext db)
        {
            this.db = db;
        }

        // GET: Shops
        public async Task<IActionResult> Index()
        {
            var movieShopDBSEContext = db.Shops.Include(s => s.Books).Include(s => s.Movie).Include(s => s.User);

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