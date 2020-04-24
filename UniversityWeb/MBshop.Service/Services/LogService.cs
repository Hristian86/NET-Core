using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MBshop.Data.Data;
using MBshop.Models;
using MBshop.Service.interfaces;

namespace MBshop.Service.Services
{
    public class LogService : ILogService
    {
        private readonly MovieShopDBSEContext db;

        public LogService(MovieShopDBSEContext db)
        {
            this.db = db;
        }

        public async Task LoggedUser(string userName)
        {
            Logs logUser = new Logs
            {
                UserLoged = userName,
            };

            this.db.Logs.Add(logUser);

            await this.db.SaveChangesAsync();
        } 
    }
}
