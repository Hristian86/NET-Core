using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.OutputModels;
using DataDomain.Data.Models;

namespace BusinessLogic.interfaces
{
    public interface IViewMovies
    {
        List<Movieses> GetListOfMovies();

    }
}
