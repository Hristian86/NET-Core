using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.OutputModels;

namespace BusinessLogic.interfaces
{
    public interface IUserShopedProducts
    {
        List<OutputMovies> PersonalItems(string id);
    }
}
