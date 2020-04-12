using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MBshop.Service.interfaces;
//using MBshop.Models;
using MBshop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MBshop.Service.StaticProperyes;

namespace MBshop.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IProfileEditService profEdit;
        private readonly IChatService msg;
        private string fullNameOfUser;
        private string userId;

        public ChatController(IChatService msg,
            IProfileEditService profEdit)
        {
            this.msg = msg;
            this.profEdit = profEdit;
        }

        [HttpGet(Name = "GetMessages")]
        [Route("GetMessages")]
        public async Task<ActionResult<List<ChatModel>>> GetMessages()
        {

            if (User.Identity.Name != null)
            {
                this.userId = GetUserId();

                this.fullNameOfUser = await this.msg.GetFullName(userId);

                //string curUserAvatar = CurrentUserAvatar();
            }

            var messageses = this.msg.GetMessages();

            var messages = messageses.Select(item => new ChatModel
            {
                Avatar = item.Avatar,
                Content = item.Content,
                Id = item.Id,
                UserName = item.UserName,
                DateT = item.DateT,
                CurrentUser = this.fullNameOfUser
            }).ToList();

            this.fullNameOfUser = "";

            return messages;
        }

        [HttpPost(Name = "Create")]
        [Route("Create")]
        [Authorize]
        public async Task<ChatModel> Create(ChatModel model)
        {

            this.userId = GetUserId();

            this.fullNameOfUser = await this.msg.GetFullName(this.userId);

            if (this.fullNameOfUser != null && model.Content.Length > 1)
            {

                // creating message in database
                string responce = await this.msg.CreateMessage(this.fullNameOfUser, model.Content, userId, CurrentUserAvatar());

                if (responce == "User account is required!")
                {
                    GlobalAlertMessages.MessageForStaatus = responce;
                }
            }

            ChatModel message = new ChatModel
            {
                //Content = model.Content
            };

            this.fullNameOfUser = "";

            return message;
        }

        [Authorize]
        [HttpDelete(Name = "Delete")]
        [Route("Delete")]
        public async Task Delete(ChatModel model)
        {

            await this.msg.Delete(model.Id);

        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        private string CurrentUserAvatar()
        {
            string avatar = this.profEdit.GetUserProperties(User.Identity.Name).Avatar;

            return avatar;
        }
    }
}