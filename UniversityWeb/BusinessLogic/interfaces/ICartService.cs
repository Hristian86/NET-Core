using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.OutputModels;

namespace BusinessLogic.interfaces
{
    public interface ICartService
    {
        List<OutputCart> GetCartBascket();
        string AddToCartBook(int id, double price);
        string AddToCartMovie(int id, double price);
        void DisposeCartProducts();
        void RemoveMovie(int id);
        void RemoveBook(int id);
    }
}
