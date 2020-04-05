using System;
using System.Collections.Generic;
using System.Text;
using MBshop.Service.OutputModels;

namespace MBshop.Service.interfaces
{
    public interface ICartService
    {
        List<ViewProducts> GetCartBascket();
        string AddToCartBook(int id, double price, string userId);
        string AddToCartMovie(int id, double price, string userId);
        void DisposeCartProducts();
        void RemoveMovie(int id);
        void RemoveBook(int id);
    }
}
