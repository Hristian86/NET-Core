using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataDomain.Data.Models;

namespace BusinessLogic.interfaces
{
    public interface IAdminPanelMovies
    {
        Task<Movies> FindMovieById(int? id);

        Task SaveMovie(Movies movie);

        List<Movies> GetMovies();

        Task UpdateMovie(Movies movie);

        Task Remove(Movies movie);

        bool MovieExist(int id);
    }
}
