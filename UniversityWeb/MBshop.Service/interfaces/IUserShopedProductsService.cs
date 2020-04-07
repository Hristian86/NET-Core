﻿using System;
using System.Collections.Generic;
using System.Text;
using MBshop.Service.OutputModels;

namespace MBshop.Service.interfaces
{
    public interface IUserShopedProductsService
    {
        List<OutputMovies> PersonalMovies(string userId);

        List<OutputBooks> PersonalBooks(string userId);
    }
}
