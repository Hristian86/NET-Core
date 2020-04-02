using System;
using System.Collections.Generic;
using System.Text;
using MBshopService.OutputModels;
using Db.Models;

namespace MBshopService.interfaces
{
    public interface IViewMovies
    {
        List<OutputMovies> GetListOfMovies();

        List<OutputMovies> SortMovies(int orderBy);

    }
}
