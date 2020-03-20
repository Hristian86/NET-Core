using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.OutputModels;

namespace BusinessLogic.interfaces
{
    public interface IUserShopedProducts
    {
        List<OutputMovies> PersonalMovies(string id);

        List<OutputBooks> PersonalBooks(string id);
    }
}
