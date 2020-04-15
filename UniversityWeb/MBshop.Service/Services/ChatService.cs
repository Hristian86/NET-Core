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

        /// <summary>
        /// Get all messages in database
        /// </summary>
        /// <returns></returns>
        public List<Messages> GetMessages()
        {
            return this.db.Messages.ToList();
        }

        /// <summary>
        /// Only Admin : Can remove all messages
        /// </summary>
        /// <returns></returns>
        public async Task DeleteAllMessages()
        {
            var removeAllMessages = this.db.Messages.ToList();
            this.db.Messages.RemoveRange(removeAllMessages);
            await this.db.SaveChangesAsync();
        }

        /// <summary>
        /// Get Chat name for current logged user from manage page
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<string> GetFullName(string user)
        {
            var fullName = await this.db.AspNetUsers.FindAsync(user);

            var name = fullName.ChatName;

            return name;
        }

        /// <summary>
        /// Creating message only if user is logged and has chat name
        /// </summary>
        /// <param name="fullNameOfUser"></param>
        /// <param name="content"></param>
        /// <param name="userId"></param>
        /// <param name="avatar"></param>
        /// <returns></returns>
        public async Task<string> CreateMessage(string fullNameOfUser, string content, string userId, string avatar)
        {

            Messages messageOrigin = new Messages
            {
                Avatar = avatar,
                UserName = fullNameOfUser,
                Content = content,
                UserId = userId,
            };

            if (userId == null || userId.Length < 1)
            {
                return $"User account is required!";
            }

            this.db.Messages.Add(messageOrigin);

            await this.db.SaveChangesAsync();

            return $"Message created successfully";
        }

        /// <summary>
        /// User can delete personal messages
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            var message = this.db.Messages.Where(x => x.Id == id).FirstOrDefault();
            this.db.Messages.Remove(message);
            await this.db.SaveChangesAsync();
        }
    }
}