using System;
using System.Collections.Generic;
using System.Text;
using DataDomain.Data.Models;

namespace BusinessLogic.interfaces
{
    public interface ICRUDoperations
    {
        List<Movies> GetMovies();

        void CreateMovie(Movies movie);

    }
}
