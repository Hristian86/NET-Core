using System;
using System.Collections.Generic;
using System.Text;
using MBshop.Service.OutputModels;

namespace MBshop.Service.interfaces
{
    public interface ISearchEngine
    {
        List<ViewProducts> Search(string searchItem);
    }
}
