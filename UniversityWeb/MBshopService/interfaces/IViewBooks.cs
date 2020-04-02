using System;
using System.Collections.Generic;
using System.Text;
using MBshopService.OutputModels;

namespace MBshopService.interfaces
{
    public interface IViewBooks
    {
        List<OutputBooks> GetListOfBooks();

        List<OutputBooks> SortBooks(int orderBy);
    }
}
