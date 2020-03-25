using System;
using System.Linq;
using BusinessLogic.interfaces;
//using static AspWebTest.Areas.Identity.Pages.Account.LoginModel;
using Microsoft.AspNetCore.Identity;
using Data.Domain.Data;
using Db.Models;
using System.Threading.Tasks;

namespace BusinessLogic.Services
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
        public async Task SaveUserProperties(string firstName, string lastName, string address, string userId)
        {
            //method for getting user by id
            var currentUser = await FindUserIndDbRepository(userId); 

            bool Changes = false;

            //Null cheking
            if (firstName != null && firstName.Length <= 20)
            {
                currentUser.FirstName = firstName;
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

            if (Changes)
            {
                await db.SaveChangesAsync();
            }
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
    }
}