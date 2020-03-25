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

        [HttpGet(Name = "Hello")]
        [Route("Greet")]
        public ActionResult<List<Messages>> Hello()
        {
            List<Messages> messageses = this.db.Messages.ToList();

            //List<Messages> messageses = new List<Messages>();

            //Messages mass = new Messages
            //{
            //    UserId = User.Identity.Name,
            //    Content = "Dedagoznam"
            //};
            //messageses.Add(mass);
            //messageses.Add(mass);

            return messageses;
        }

        [HttpPost(Name = "Create")]
        [Route("Create")]
        public ChatModel Create(ChatModel model)
        {

            var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            Messages messageOrigin = new Messages
            {
                Content = model.Content,
                UserId = user,
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
    }
}
