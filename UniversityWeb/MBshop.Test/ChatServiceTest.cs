using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBshop.Data.Data;
using MBshop.Models;
using MBshop.Service.interfaces;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MBshop.Test
{
    public class ChatServiceTest
    {
        [Fact]
        public void ShoudReturnOneMessageInDatabase()
        {
            var service = new ChatService();

            var result = service.GetMessages().Count;

            Assert.Equal(1,result);

        }

        [Fact]
        public void ShouldReturnMessageCreated()
        {
            var service = new ChatService();

            string name = "icaka";
            string content = "hi";
            string user = "ico";
            string avatar = "img";

            var result = service.CreateMessage(name,content,user,avatar).Result;

            Assert.Equal("Message created successfully",result);

        }

        public class ChatService : IChatService
        {
            public async Task<string> CreateMessage(string fullNameOfUser, string content, string user, string avatar)
            {
                var options = new DbContextOptionsBuilder<MovieShopDBSEContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

                MovieShopDBSEContext db = new MovieShopDBSEContext(options);

                Messages messageOrigin = new Messages
                {
                    Avatar = avatar,
                    UserName = fullNameOfUser,
                    Content = content,
                    UserId = user
                };

                db.Messages.Add(messageOrigin);

                await db.SaveChangesAsync();

                return $"Message created successfully";
            }

            public Task Delete(int id)
            {
                throw new NotImplementedException();
            }

            public Task DeleteAllMessages()
            {
                throw new NotImplementedException();
            }

            public Task<string> GetFullName(string user)
            {
                throw new NotImplementedException();
            }

            public List<Messages> GetMessages()
            {
                var options = new DbContextOptionsBuilder<MovieShopDBSEContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

                MovieShopDBSEContext db = new MovieShopDBSEContext(options);

                Messages message = new Messages
                {
                    Content = "asd"
                };

                db.Add(message);
                db.SaveChanges();

                return db.Messages.ToList();
            }
        }
    }
}