using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MBshop.Service.OutputModels;

namespace MBshop.Service.interfaces
{
    public interface IRatingSistemService
    {
        Task<string> RateMovie(OutputMovies model, string user);
        Task<double> RateBook(OutputBooks model, string user);
    }
}
