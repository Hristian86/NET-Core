using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MBshop.Service.OutputModels;

namespace MBshop.Service.interfaces
{
    public interface ICartService
    {
        List<ViewProducts> GetCartBasketUser(string userId);
        Task<string> AddToCartBook(int id, double price, string userId);
        Task<string> AddToCartMovie(int id, double price, string userId);
        Task<string> DisposeCartProducts(string userId);
        Task<string> RemoveMovie(int id,string userId);
        Task<string> RemoveBook(int id,string userId);
    }
}
