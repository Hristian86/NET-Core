using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Data.Domain.Data;
using Db.Models;
using System.Linq;

namespace BusinessLogic.Services
{
    public class ChatService
    {
        private readonly MovieShopDBSEContext db;

        public ChatService(MovieShopDBSEContext db)
        {
            this.db = db;
        }

        public List<Messages> GetMessages()
        {
            return this.db.Messages.ToList();
        }

        public async Task<string> GetFullName(string user)
        {
            var fullName = await this.db.AspNetUsers.FindAsync(user);

            var name = fullName.FirstName;

            return name;
        }

        public async Task CreateMessage(string fullNameOfUser, string content,string user)
        {
            Messages messageOrigin = new Messages
            {
                UserName = fullNameOfUser,
                Content = content,
                UserId = user
            };

            this.db.Messages.Add(messageOrigin);

            await this.db.SaveChangesAsync();
        }

    }
}
