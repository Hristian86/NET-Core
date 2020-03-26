using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Db.Models;

namespace BusinessLogic.interfaces
{
    public interface IChatService
    {
        List<Messages> GetMessages();
        Task<string> GetFullName(string user);
        Task CreateMessage(string fullNameOfUser, string content, string user);
        Task Delete(int id);
    }
}
