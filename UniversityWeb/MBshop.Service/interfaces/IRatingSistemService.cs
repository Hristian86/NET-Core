using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MBshop.Service.OutputModels;

namespace MBshop.Service.interfaces
{
    public interface IRatingSistemService
    {
        Task<string> RateMovie(OutputMovies model, string userId);
        Task<string> RateBook(OutputBooks model, string userId);
    }
}
