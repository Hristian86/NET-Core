using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLogic.interfaces;
//using Db.Models;
using MBshop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MBshop.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IProfileEdit profEdit;
        private readonly IChatService msg;
        private string fullNameOfUsr;
        private string user;

        public ChatController(IChatService msg,
            IProfileEdit profEdit)
        {
            this.msg = msg;
            this.profEdit = profEdit;
        }

        [HttpGet(Name = "GetMessages")]
        [Route("GetMessages")]
        public async Task<ActionResult<List<ChatModel>>> GetMessages()
        {
            var messageses = msg.GetMessages();

            List<ChatModel> chats = new List<ChatModel>();

            if (User.Identity.Name != null)
            {
                this.user = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                this.fullNameOfUsr = await msg.GetFullName(user);

                //string curUserAvatar = CurrentUserAvatar();
            }
            
            foreach (var item in messageses)
            {

                ChatModel chat = new ChatModel
                {
                    Avatar = item.Avatar,
                    Content = item.Content,
                    Id = item.Id,
                    UserName = item.UserName,
                    DateT = item.DateT,
                    CurrentUser = this.fullNameOfUsr
                };

                chats.Add(chat);
            }

            return chats;
        }

        [HttpPost(Name = "Create")]
        [Route("Create")]
        [Authorize]
        public async Task<ChatModel> Create(ChatModel model)
        {

            string user = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            string fullNameOfUser = await msg.GetFullName(user);

            if (fullNameOfUser != null && model.Content.Length > 0)
            {

                await this.msg.CreateMessage(fullNameOfUser, model.Content, user, CurrentUserAvatar());

            }

            ChatModel message = new ChatModel
            {
                Content = model.Content
            };

            return message;
        }

        [Authorize]
        [HttpDelete(Name = "Delete")]
        [Route("Delete")]
        public async Task Delete(ChatModel model)
        {

            await this.msg.Delete(model.Id);

        }

        private string CurrentUserAvatar()
        {
            string avatar = profEdit.GetUserProperties(User.Identity.Name).Avatar;

            return avatar;
        }
    }
}