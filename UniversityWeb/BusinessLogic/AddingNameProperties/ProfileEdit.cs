using System;
using System.Linq;
using BusinessLogic.interfaces;
//using static AspWebTest.Areas.Identity.Pages.Account.LoginModel;
using DataDomain.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogic
{
    public class ProfileEdit : IProfileEdit
    {
        private MovieRentalDBSContext db = new MovieRentalDBSContext();

        public void SaveUserProperties(string firstName, string lastName, string address, string userId)
        {
            var currentUser = FindDbRepository(userId);

            currentUser.FirstName = firstName;
            currentUser.LastName = lastName;
            currentUser.Address = address;

            db.SaveChanges();
        }

        private AspNetUsers FindDbRepository(string userId)
        {
            var usr = db.AspNetUsers.Where(x => x.Id == userId).FirstOrDefault();
            return usr;
        }
    }
}