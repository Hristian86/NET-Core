using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MBshopService.OutputModels;

namespace MBshopService.interfaces
{
    public interface IRatingSistem
    {
        Task<double> RateMovie(OutputMovies model, string user);
        Task<double> RateBook(OutputBooks model, string user);
    }
}
