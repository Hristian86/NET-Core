using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Data.Domain.Data;
using Db.Models;
using MBshop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MBshop.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly MovieShopDBSEContext db;

        public ChatController(MovieShopDBSEContext db)
        {
            this.db = db;
        }

        [HttpGet(Name = "GetMessages")]
        [Route("GetMessages")]
        public ActionResult<List<ChatModel>> GetMessages()
        {
            List<Messages> messageses = this.db.Messages.ToList();

            List<ChatModel> chats = new List<ChatModel>();

            foreach (var item in messageses)
            {
                ChatModel chat = new ChatModel
                {
                    Content = item.Content,
                    UserId = item.UserId,
                    Id = item.Id,
                    UserName = item.UserName,
                    DateT = item.DateT,
                    CurrentUser = User.Identity.Name
                };

                chats.Add(chat);
            }

            return chats;
        }

        [HttpPost(Name = "Create")]
        [Route("Create")]
        public ChatModel Create(ChatModel model)
        {

            var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            Messages messageOrigin = new Messages
            {
                UserName = User.Identity.Name,
                Content = model.Content,
                UserId = user
            };

            this.db.Messages.Add(messageOrigin);

            this.db.SaveChanges();

            ChatModel message = new ChatModel
            {
                UserId = User.Identity.Name,
                Content = model.Content
            };

            return message;
        }

        [HttpDelete(Name = "Delete")]
        [Route("Delete")]
        public void Delete(ChatModel model)
        {
            var message = this.db.Messages.Where(x => x.Id == model.Id).FirstOrDefault();
            this.db.Messages.Remove(message);
            this.db.SaveChanges();
        }
    }
}
