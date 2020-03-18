using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.OutputModels;

namespace BusinessLogic.interfaces
{
    public interface IViewBooks
    {
        IReadOnlyList<Bookses> GetListOfBooks();
    }
}
