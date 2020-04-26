﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MBshop.Data.Data;
using MBshop.Models;
using Microsoft.AspNetCore.Authorization;
using MBshop.Service.StaticProperyes;

namespace MBshop.Controllers
{


    public class AdministrationController : Controller
    {
        private readonly MovieShopDBSEContext db;

        public AdministrationController(MovieShopDBSEContext db)
        {
            this.db = db;
        }

        // GET: Administration
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Index()
        {
            return View(await db.AspNetUsers.ToListAsync());
        }

        // GET: Administration/Details/5
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetUsers = await db.AspNetUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aspNetUsers == null)
            {
                return NotFound();
            }

            return View(aspNetUsers);
        }

        // GET: Administration/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administration/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,Avatar,ChatName,FirstName,LastName,Address,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount,RentalId,CreatedOn")] AspNetUsers aspNetUsers)
        {
            if (ModelState.IsValid)
            {
                db.Add(aspNetUsers);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aspNetUsers);
        }

        // GET: Administration/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetUsers = await db.AspNetUsers.FindAsync(id);
            if (aspNetUsers == null)
            {
                return NotFound();
            }
            return View(aspNetUsers);
        }

        // POST: Administration/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,Avatar,ChatName,FirstName,LastName,Address,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount,RentalId,CreatedOn")] AspNetUsers aspNetUsers)
        {
            string userName = "";

            userName = aspNetUsers.UserName;

            if (id != aspNetUsers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(aspNetUsers);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AspNetUsersExists(aspNetUsers.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                GlobalAlertMessages.StatusMessage = $"this user {userName} has been updated !";
                return RedirectToAction(nameof(Index));
            }
            GlobalAlertMessages.StatusMessage = $"this user {userName} not found !";
            return View(aspNetUsers);
        }

        // GET: Administration/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetUsers = await db.AspNetUsers
                .FirstOrDefaultAsync(m => m.Id == id);

            if (aspNetUsers == null)
            {
                return NotFound();
            }

            return View(aspNetUsers);
        }

        // POST: Administration/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            string userName = "";

            var aspNetUsers = await db.AspNetUsers.FindAsync(id);

            userName = aspNetUsers.UserName;

            bool chekForShops = this.db.Shops.Any(x => x.UserId == aspNetUsers.Id);

            bool chekForRatings = this.db.Rating.Any(x => x.UserId == aspNetUsers.Id);

            bool chekForCartItems = this.db.Cart.Any(x => x.UserId == aspNetUsers.Id);

            bool chekForMessages = this.db.Messages.Any(x => x.UserId == aspNetUsers.Id);

            if (chekForShops)
            {
                var userShops = this.db.Shops
                .Where(x => x.UserId == aspNetUsers.Id)
                .ToList();

                this.db.RemoveRange(userShops);
                await this.db.SaveChangesAsync();
            }

            if (chekForRatings)
            {
                var userRating = this.db.Rating.Where(x => x.UserId == aspNetUsers.Id).ToList();

                this.db.RemoveRange(userRating);
                await this.db.SaveChangesAsync();
            }

            if (chekForCartItems)
            {
                var userCart = this.db.Cart.Where(x => x.UserId == aspNetUsers.Id).ToList();

                this.db.RemoveRange(userCart);
                await this.db.SaveChangesAsync();
            }

            if (chekForMessages)
            {

                var userMessages = this.db.Messages.Where(x => x.UserId == aspNetUsers.Id).ToList();

                this.db.RemoveRange(userMessages);
                await this.db.SaveChangesAsync();
            }

            db.AspNetUsers.Remove(aspNetUsers);
            await db.SaveChangesAsync();
            GlobalAlertMessages.StatusMessage = $"This user {userName} has been deleted !";
            return RedirectToAction(nameof(Index));
        }

        private bool AspNetUsersExists(string id)
        {
            return db.AspNetUsers.Any(e => e.Id == id);
        }
    }
}