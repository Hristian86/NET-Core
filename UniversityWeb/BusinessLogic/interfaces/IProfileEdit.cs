using System;
using System.Collections.Generic;
using System.Text;
using DataDomain.Data.Models;

namespace BusinessLogic.interfaces
{
    public interface IProfileEdit
    {
        /// <summary>
        /// Saving changes for user profile
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="address"></param>
        /// <param name="userId"></param>
        public void SaveUserProperties(string firstName, string lastName, string address, string userId);


        /// <summary>
        /// Getting current user for the session
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        AspNetUsers GetUserProperties(string userName);
    }
}
