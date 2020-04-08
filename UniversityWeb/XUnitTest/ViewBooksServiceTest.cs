using System;
using System.Collections.Generic;
using System.Text;
using MBshop.Service.interfaces;
using MBshop.Service.OutputModels;
using Xunit;

namespace MBshop.Test
{
    public class ViewBooksServiceTest
    {

        [Fact]
        public void GetAllBooksCount()
        {

            var service = new ViewBooksService();

            var result = service.GetListOfBooks().Count;


            Assert.Equal(3,result);
        }

        public class ViewBooksService : IViewBooksService
        {
            public List<OutputBooks> GetListOfBooks()
            {
                return new List<OutputBooks>()
                {
                    new OutputBooks(),
                    new OutputBooks(),
                    new OutputBooks()
                };
            }

            public List<OutputBooks> SortBooks(int orderBy)
            {
                throw new NotImplementedException();
            }
        }


    }
}
