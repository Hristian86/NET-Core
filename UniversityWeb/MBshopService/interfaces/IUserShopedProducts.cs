using System;
using System.Collections.Generic;
using System.Text;
using MBshopService.OutputModels;

namespace MBshopService.interfaces
{
    public interface IUserShopedProducts
    {
        List<OutputMovies> PersonalMovies(string id);

        List<OutputBooks> PersonalBooks(string id);
    }
}
