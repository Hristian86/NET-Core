using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
//using MBshop.Models;
using MBshop.Models;

namespace MBshop.Service.interfaces
{
    public interface IShopItems
    {
        Task<string> BuyMovie(string userId, int MovieId);
        Task BuyBook(string userId, int bookId);
    }
}
