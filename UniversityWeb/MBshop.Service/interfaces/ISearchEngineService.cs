using System;
using System.Collections.Generic;
using System.Text;
using MBshop.Service.OutputModels;

namespace MBshop.Service.interfaces
{
    public interface ISearchEngineService
    {
        List<ViewProducts> Search(string searchItem,string user);
    }
}
