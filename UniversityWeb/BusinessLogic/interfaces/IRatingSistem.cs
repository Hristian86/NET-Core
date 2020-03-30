using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.OutputModels;

namespace BusinessLogic.interfaces
{
    public interface IRatingSistem
    {
        Task<double> RateMovie(OutputMovies model, string user);
        Task<double> RateBook(OutputBooks model, string user);
    }
}
