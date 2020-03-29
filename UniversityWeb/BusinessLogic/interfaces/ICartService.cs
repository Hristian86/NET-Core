using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.OutputModels;

namespace BusinessLogic.interfaces
{
    public interface ICartService
    {
        List<OutputCart> GetCartBascket();
        void AddToCartBook(int id, double price);
        void AddToCartMovie(int id, double price);
        void DisposeCartProducts();
        void RemoveMovie(int id);
        void RemoveBook(int id);
    }
}
