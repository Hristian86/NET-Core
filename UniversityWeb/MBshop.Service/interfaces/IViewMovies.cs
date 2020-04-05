using System;
using System.Collections.Generic;
using System.Text;
using MBshop.Service.OutputModels;
using MBshop.Models;

namespace MBshop.Service.interfaces
{
    public interface IViewMovies
    {
        List<OutputMovies> GetListOfMovies();

        List<OutputMovies> SortMovies(int orderBy);

    }
}
