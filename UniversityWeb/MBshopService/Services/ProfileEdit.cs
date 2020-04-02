using System;
using System.Linq;
using MBshopService.interfaces;
//using static AspWebTest.Areas.Identity.Pages.Account.LoginModel;
using Microsoft.AspNetCore.Identity;
using Data.Domain.Data;
using Db.Models;
using System.Threading.Tasks;

namespace MBshopService.Services
{
    /// <summary>
    /// Changing profile atributes
    /// </summary>
    public class ProfileEdit : IProfileEdit
    {
        private MovieShopDBSEContext db;
        public ProfileEdit(MovieShopDBSEContext db)
        {
            this.db = db;
        }
        
        /// <summary>
        /// Saving changes for user profile
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="address"></param>
        /// <param name="userId"></param>
        public async Task<bool> SaveUserProperties(string avatar, string chatName, string firstName, string lastName, string address, string userId)
        {
            //method for getting user by id
            var currentUser = await FindUserIndDbRepository(userId); 

            bool Changes = false;
            bool chatNameChek = false;

            //Null cheking
            if (chatName != null && chatName.Length <= 20)
            {
                //cheking for unique nick name
                chatNameChek = ChekNickName(chatName);
                currentUser.ChatName = chatName;
                Changes = true;
            }

            if (firstName != null && firstName.Length <= 20)
            {
                currentUser.FirstName = firstName;
                Changes = true;
            }

            if (avatar != null)
            {
                currentUser.Avatar = avatar;
                Changes = true;
            }

            if (lastName != null && lastName.Length <= 20)
            {
                currentUser.LastName = lastName;
                Changes = true;
            }

            if (address != null && address.Length <= 50)
            {
                currentUser.Address = address;
                Changes = true;
            }

            if (Changes && !chatNameChek)
            {
                await db.SaveChangesAsync();
            }
            return chatNameChek;
        }

        /// <summary>
        /// Getting current user for the session
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public AspNetUsers GetUserProperties(string userName)
        {
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

        // Searching for the current user by id
        private async Task<AspNetUsers> FindUserIndDbRepository(string userId)
        {
            //var usr = db.AspNetUsers
            //    .Where(x => x.Id == userId)
            //    .FirstOrDefault();

            var usr = await db.AspNetUsers.FindAsync(userId);
            return usr;
        }

        private bool ChekNickName(string ChatName)
        {
            return this.db.AspNetUsers.Any(x => x.ChatName == ChatName);
        }
    }
}