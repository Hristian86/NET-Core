using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLogic.Services;
using Data.Domain.Data;
using Db.Models;
using MBshop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MBshop.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly MovieShopDBSEContext db;
        private readonly ChatService msg;
        private string user = "";
        private string fullNameOfUsr = "";

        public ChatController(MovieShopDBSEContext db,
            ChatService msg)
        {
            this.db = db;
            this.msg = msg;
        }

        [HttpGet(Name = "GetMessages")]
        [Route("GetMessages")]
        public async Task<ActionResult<List<ChatModel>>> GetMessages()
        {
            List<Messages> messageses = msg.GetMessages();

            List<ChatModel> chats = new List<ChatModel>();

            if (User.Identity.Name != null)
            {
                this.user = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                this.fullNameOfUsr = await msg.GetFullName(user);
            }


            foreach (var item in messageses)
            {
                ChatModel chat = new ChatModel
                {
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
        public async Task<ChatModel> Create(ChatModel model)
        {

            var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            string fullNameOfUser = await msg.GetFullName(user);

            if (fullNameOfUser != null && model.Content.Length > 0)
            {

                await this.msg.CreateMessage(fullNameOfUser, model.Content, user);

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
        public void Delete(ChatModel model)
        {
            var message = this.db.Messages.Where(x => x.Id == model.Id).FirstOrDefault();
            this.db.Messages.Remove(message);
            this.db.SaveChanges();
        }

        private async Task<string> GetFullName(string user)
        {
            var fullName = await this.db.AspNetUsers.FindAsync(user);

            var names = fullName.FirstName;

            return names;
        }
    }
}