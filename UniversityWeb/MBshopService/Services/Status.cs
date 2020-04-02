using System;
using System.Collections.Generic;
using System.Text;
using MBshopService.OutputModels;

namespace MBshopService.Services
{
    public class Status
    {
        public Status()
        {
        }

        public static string Message { get; set; }

        /// <summary>
        /// Cheking between database movies and purchased movies for status change button in view
        /// </summary>
        /// <param name="list"></param>
        /// <param name="userItm"></param>
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

        /// <summary>
        /// Cheking between database books and purchased movies for status change button in view
        /// </summary>
        /// <param name="list"></param>
        /// <param name="userItm"></param>
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