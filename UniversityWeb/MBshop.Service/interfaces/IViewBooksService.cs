using System;
using System.Collections.Generic;
using System.Text;
using MBshop.Service.OutputModels;

namespace MBshop.Service.interfaces
{
    public interface IViewBooksService
    {
        List<OutputBooks> GetListOfBooks();

        List<OutputBooks> SortBooks(int orderBy);
    }
}
