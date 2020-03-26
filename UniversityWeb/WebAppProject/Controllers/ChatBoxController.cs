using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.interfaces;
using Data.Domain.Data;
using Db.Models;
using MBshop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MBshop.Controllers
{
    
    public class ChatBoxController : Controller
    {
        private readonly MovieShopDBSEContext db;
        private readonly IProfileEdit edit;

        public ChatBoxController(MovieShopDBSEContext db,
            IProfileEdit edit)
        {
            this.db = db;
            this.edit = edit;
        }

        public IActionResult ChatPanel()
        {

            if (User.Identity.Name != null)
            {

                var curUser = edit.GetUserProperties(User.Identity.Name);

                //var usery = userManager.GetUserId(this.User);

                UserNames tempUser = new UserNames
                {
                    firstName = curUser.FirstName,
                    LastName = curUser.LastName,
                    Address = curUser.Address
                };
                return View(tempUser);
            }

            return this.View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete()
        {

            var remove = this.db.Messages.ToList();
            this.db.Messages.RemoveRange(remove);
            this.db.SaveChanges();

            return RedirectToAction("ChatPanel","ChatBox");
        }
    }
}