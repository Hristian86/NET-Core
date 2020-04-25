using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBshop.Data.Data;
using MBshop.Models;
using MBshop.Service.interfaces;
using MBshop.Service.OutputModels;
using MBshop.Service.WebConstants;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MBshop.Test
{
    public class ProfileEdintServiceTest
    {
        [Fact]
        public void ShouldReturnUserProfileProperties()
        {

            var service = new ProfileEditService();

            var user = service.GetUserProperties("icaka");

            AspNetUsers returnedUser = new AspNetUsers
            {
                UserName = "icaka",
                ChatName = "Naruto",
                FirstName = "ico",
                LastName = "ickov",
                Address = "Ruse"
            };

            Assert.Equal(returnedUser.ChatName, user.ChatName);
            Assert.Equal(returnedUser.FirstName, user.FirstName);
            Assert.Equal(returnedUser.LastName, user.LastName);
            Assert.Equal(returnedUser.Address, user.Address);

        }

    }

    public class ProfileEditService : IProfileEditService
    {
        public Task DateCreatedAcc(string userId)
        {
            throw new NotImplementedException();
        }

        public AspNetUsers GetUserProperties(string userName)
        {
            var options = new DbContextOptionsBuilder<MovieShopDBSEContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            MovieShopDBSEContext db = new MovieShopDBSEContext(options);

            AspNetUsers user = new AspNetUsers
            {
                Id = "asd",
                UserName = "icaka",
                ChatName = "Naruto",
                FirstName = "ico",
                LastName = "ickov",
                Address = "Ruse"
            };

            db.AspNetUsers.Add(user);
            db.SaveChanges();

            var usrs = db.AspNetUsers
                .Where(x => x.UserName == userName)
                .FirstOrDefault();

            //Returning this user for displaying details
            var userNms = new AspNetUsers
            {
                Avatar = usrs.Avatar,
                ChatName = usrs.ChatName,
                FirstName = usrs.FirstName,
                LastName = usrs.LastName,
                Address = usrs.Address
            };

            return userNms;
        }

        public Task<bool> SaveUserProperties(string avatar, string chatName, string firstName, string lastName, string address, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
