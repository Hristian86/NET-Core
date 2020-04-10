using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MBshop.Service.interfaces;
using MBshop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MBshop.Controllers
{
    
    public class ChatBoxController : Controller
    {
        private readonly IProfileEditService edit;
        private readonly IChatService msg;

        public ChatBoxController(
            IProfileEditService edit,
            IChatService msg)
        {
            this.edit = edit;
            this.msg = msg;
        }

        public IActionResult ChatPanel()
        {

            if (User.Identity.Name != null)
            {

                // all other properties in current logged user are null
                var curUser = this.edit.GetUserProperties(User.Identity.Name);

                UserNames tempUser = new UserNames
                {
                    ChatName = curUser.ChatName
                };

                return View(tempUser);
            }

            return this.View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete()
        {

            await this.msg.DeleteAllMessages();

            return RedirectToAction("ChatPanel","ChatBox");
        }
    }
}