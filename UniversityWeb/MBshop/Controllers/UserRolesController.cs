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
using MBshop.Service.StaticProperyes;
using MBshop.Service.Services;

namespace MBshop.Controllers
{

    public class UserRolesController : Controller
    {
        private readonly RoleService roleService;
        private string result = "";

        public UserRolesController(
            RoleService roleService
            )
        {
            this.roleService = roleService;
        }


        // GET: UserRoles
        [Authorize(Roles = "Admin,Moderator")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Index()
        {
            var roles = roleService.GetRoles();

            return this.View(roles.ToList());
        }

        // POST: UserRoles/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string roleId, string userId, string userName, string userRole)
        {
            try
            {
                result = await roleService.AssignRole(roleId, userId, userName, userRole);
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException("Somthing goes wrong with user-roles assigning" , e);
            }

            if (result == "Not found")
            {
                return RedirectToAction("Error404Page", "Error404");
            }

            return RedirectToAction("Index", "UserRoles");
        }
    }
}
