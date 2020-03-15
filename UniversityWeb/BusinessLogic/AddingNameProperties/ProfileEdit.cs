﻿using System;
using System.Linq;
using BusinessLogic.interfaces;
//using static AspWebTest.Areas.Identity.Pages.Account.LoginModel;
using DataDomain.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogic
{
    /// <summary>
    /// Changing profile atributes
    /// </summary>
    public class ProfileEdit : IProfileEdit
    {
        private MovieRentalDBSEContext db = new MovieRentalDBSEContext();
        
        /// <summary>
        /// Saving changes for user profile
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="address"></param>
        /// <param name="userId"></param>
        public void SaveUserProperties(string firstName, string lastName, string address, string userId)
        {
            //method for getting user id
            var currentUser = FinUserIndDbRepository(userId); 

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

            if (address != null && address.Length <= 30)
            {
                currentUser.Address = address;
                Changes = true;
            }

            if (Changes)
            {
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Getting current user for the session
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public AspNetUsers GetUserProperties(string userName)
        {
            var usrs = db.AspNetUsers.Where(x => x.UserName == userName).FirstOrDefault();

            //Returning this user for displaying details
            var userNms = new AspNetUsers
            {
                Id = usrs.Id,
                FirstName = usrs.FirstName,
                LastName = usrs.LastName,
                Address = usrs.Address
            };

            return userNms;
        }

        // Searching for the current user by id
        private AspNetUsers FinUserIndDbRepository(string userId)
        {
            var usr = db.AspNetUsers.Where(x => x.Id == userId).FirstOrDefault();
            return usr;
        }
    }
}