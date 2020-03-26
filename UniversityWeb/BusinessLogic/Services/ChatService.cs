using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Data.Domain.Data;
using Db.Models;
using System.Linq;
using BusinessLogic.interfaces;

namespace BusinessLogic.Services
{
    public class ChatService : IChatService
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

        public async Task Delete(int id)
        {
            var message = this.db.Messages.Where(x => x.Id == id).FirstOrDefault();
            this.db.Messages.Remove(message);
            await this.db.SaveChangesAsync();
        }

    }
}
