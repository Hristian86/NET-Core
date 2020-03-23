using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.OutputModels;
using Db.Models;

namespace BusinessLogic.interfaces
{
    public interface IViewMovies
    {
        List<OutputMovies> GetListOfMovies();

    }
}
