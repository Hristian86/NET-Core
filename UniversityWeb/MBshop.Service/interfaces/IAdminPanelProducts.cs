using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MBshop.Models;

namespace MBshop.Service.interfaces
{
    public interface IAdminPanelProducts
    {
        Task<Movies> FindMovieById(int? id);

        Task SaveMovie(Movies movie);

        List<Movies> GetMovies();

        Task UpdateMovie(Movies movie);

        Task RemoveMovie(Movies movie);

        bool MovieExist(int id);
    }
}
