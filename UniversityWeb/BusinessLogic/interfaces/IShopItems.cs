using System;
using System.Collections.Generic;
using System.Text;
using DataDomain.Data.Models;

namespace BusinessLogic.interfaces
{
    public interface IShopItems
    {
        public void BuyMovie(string userId, int MovieId);
    }
}
