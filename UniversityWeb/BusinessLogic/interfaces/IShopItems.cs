using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
//using Db.Models;
using Db.Models;

namespace BusinessLogic.interfaces
{
    public interface IShopItems
    {
        public void BuyMovie(string userId, int MovieId);
    }
}
