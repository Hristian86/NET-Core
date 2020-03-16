using System;
using System.Collections.Generic;
using System.Text;
using DataDomain.Data.Models;
using BusinessLogic.OutputModels;

namespace BusinessLogic.interfaces
{
    public interface IConvertingCollection
    {
        List<Movieses> GetMovies(List<Movies> Mview);
    }
}
