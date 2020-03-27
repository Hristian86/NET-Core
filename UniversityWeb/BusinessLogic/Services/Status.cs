using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.OutputModels;

namespace BusinessLogic.Services
{
    public class Status
    {
        public Status()
        {
        }

        public void StatusChekMovies(List<OutputMovies> list, List<OutputMovies> userItm)
        {

            for (int i = 0; i < list.Count; i++)
            {
                var curMovie = list[i];

                for (int j = 0; j < userItm.Count; j++)
                {
                    var userMovies = userItm[j];

                    if (curMovie.Id == userMovies.Id)
                    {
                        curMovie.Status = true;
                        break;
                    }
                }
            }
        }

        public void StatusChekBooks(List<OutputBooks> list, List<OutputBooks> userItm)
        {

            for (int i = 0; i < list.Count; i++)
            {
                var curMovie = list[i];

                for (int j = 0; j < userItm.Count; j++)
                {
                    var userMovies = userItm[j];

                    if (curMovie.Id == userMovies.Id)
                    {
                        curMovie.Status = true;
                        break;
                    }
                }
            }
        }

    }
}