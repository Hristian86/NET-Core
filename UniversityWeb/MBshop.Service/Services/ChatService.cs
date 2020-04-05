using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MBshop.Data.Data;
using MBshop.Models;
using System.Linq;
using MBshop.Service.interfaces;

namespace MBshop.Service.Services
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

        public async Task DeleteAllMessages()
        {
            var remove = this.db.Messages.ToList();
            this.db.Messages.RemoveRange(remove);
            await this.db.SaveChangesAsync();
        }

        public async Task<string> GetFullName(string user)
        {
            var fullName = await this.db.AspNetUsers.FindAsync(user);

            var name = fullName.ChatName;

            return name;
        }

        public async Task CreateMessage(string fullNameOfUser, string content,string user, string avatar)
        {
            Messages messageOrigin = new Messages
            {
                Avatar = avatar,
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