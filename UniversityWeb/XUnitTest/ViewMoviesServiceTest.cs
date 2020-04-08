using System;
using System.Collections.Generic;
using System.Text;
using MBshop.Service.interfaces;
using MBshop.Service.OutputModels;
using Xunit;

namespace MBshop.Test
{
    public class ViewMoviesServiceTest
    {

        [Fact]
        public void GetAllFromViewMoviesServiceCount()
        {

            var service = new ViewMoviesService();

           var result =  service.GetListOfMovies().Count;

            Assert.Equal(3,result);
        }

        public class ViewMoviesService : IViewMoviesService
        {
            public List<OutputMovies> GetListOfMovies()
            {
                return new List<OutputMovies>()
                {
                    new OutputMovies(),
                    new OutputMovies(),
                    new OutputMovies()
                };
            }

            public List<OutputMovies> SortMovies(int orderBy)
            {
                throw new NotImplementedException();
            }
        }
    }
}
