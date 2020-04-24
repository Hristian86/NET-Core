using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MBshop.Data.Data;
using MBshop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MBshop.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace MBshop.Controllers
{

    public class UserRolesController : Controller
    {
        private readonly MovieShopDBSEContext db;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ILogger<RegisterModel> logger;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly string adminRole = "Admin";
        private readonly string userRole = "User";
        private readonly string Moderator = "Moderator";

        public UserRolesController(MovieShopDBSEContext db,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            RoleManager<IdentityRole> roleManager
            )
        {
            this.db = db;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.roleManager = roleManager;
        }

        // GET: UserRoles
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Index()
        {
            var movieShopDBSEContext = db.AspNetUserRoles.Include(a => a.Role).Include(a => a.User);
            return View(await movieShopDBSEContext.ToListAsync());
        }

        // POST: UserRoles/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string roleId, string userId, string userName, string userRole)
        {
            var role = this.db.AspNetUserRoles.Where(x => x.UserId == userId).FirstOrDefault();

            var roleses = db.AspNetUserRoles.Include(a => a.Role).Include(a => a.User);

            var roleAssign = roleses.Where(x => x.UserId == userId).FirstOrDefault();

            var rolesColl = new string[3];
            rolesColl[0] = this.adminRole;
            rolesColl[1] = this.userRole;
            rolesColl[2] = this.Moderator;

            if (userId != role.UserId)
            {
                return NotFound();
            }

            var user = await userManager.FindByNameAsync(userName);

            if (roleAssign.Role.Name != null)
            {
                await userManager.RemoveFromRolesAsync(user,rolesColl);
            }

            var admin = await roleManager.RoleExistsAsync(this.adminRole);
            var usery = await roleManager.RoleExistsAsync(this.userRole);
            var moderat = await roleManager.RoleExistsAsync(this.Moderator);

            if (roleAssign.Role.Name == this.adminRole)
            {
                var urs = await this.userManager.RemoveFromRoleAsync(user, this.adminRole);
            }
            else if (roleAssign.Role.Name == this.userRole)
            {
                var urs = await this.userManager.RemoveFromRoleAsync(user, this.userRole);
            }
            else if (roleAssign.Role.Name == this.Moderator)
            {
                var urs = await this.userManager.RemoveFromRoleAsync(user, this.Moderator);
            }


            //TO DO Roles

            //var userRol = await userManager.GetUserAsync(this.User);

            if (userRole == this.adminRole)
            {
                await this.roleManager.CreateAsync(new IdentityRole { Name = this.adminRole });

                await this.userManager.AddToRoleAsync(user, adminRole);



            }
            else if (userRole == this.Moderator)
            {
                await this.roleManager.CreateAsync(new IdentityRole { Name = this.Moderator });

                await this.userManager.AddToRoleAsync(user, this.Moderator);
            }
            else if (userRole == this.userRole)
            {
                await this.roleManager.CreateAsync(new IdentityRole { Name = this.userRole });

                await this.userManager.AddToRoleAsync(user, userRole);
            }

            return View();
        }

    }
}
