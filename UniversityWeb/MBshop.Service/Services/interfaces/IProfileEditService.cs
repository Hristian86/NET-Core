using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
//using MBshop.Models;
using MBshop.Models;

namespace MBshop.Service.interfaces
{
    public interface IProfileEditService
    {
        /// <summary>
        /// Saving changes for user profile
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="address"></param>
        /// <param name="userId"></param>
        Task<bool> SaveUserProperties(string avatar, string chatName, string firstName, string lastName, string address, string userId);

        Task DateCreatedAcc(string userId);


        /// <summary>
        /// Getting current user for the session
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        AspNetUsers GetUserProperties(string userName);
    }
}