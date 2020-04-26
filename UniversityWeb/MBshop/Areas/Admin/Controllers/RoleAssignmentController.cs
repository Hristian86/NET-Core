using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MBshop.Areas.Identity.Pages.Account;
using MBshop.Data.Data;
using MBshop.Service.interfaces;
using MBshop.Service.Services;
using MBshop.Service.StaticProperyes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MBshop.Areas.Administration.Controllers
{
    [Area("Admin")]
    public class RoleAssignmentController : Controller
    {
        private readonly IRoleService roleService;
        private string result = "";

        public RoleAssignmentController(IRoleService roleService)
        {
            this.roleService = roleService;
        }


        //GET: UserRoles
       [Authorize(Roles = "Admin")]
       [AutoValidateAntiforgeryToken]
        public IActionResult Index()
        {
            var roles = roleService.GetRoles();

            return this.View(roles.ToList());
        }

        // POST: UserRoles/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(string roleId, string userId, string userName, string userRole)
        {
            try
            {
                this.result = await roleService.AssignRole(roleId, userId, userName, userRole);
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException("Somthing goes wrong with user-roles assigning", e);
            }

            if (this.result == "Not found")
            {
                return RedirectToAction("Error404Page", "Error404");
            }

            return RedirectToAction("Index", "RoleAssignment", "Admin");
        }
    }
}