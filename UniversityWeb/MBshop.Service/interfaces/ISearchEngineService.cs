using System;
using System.Collections.Generic;
using System.Text;
using MBshop.Service.OutputModels;

namespace MBshop.Service.interfaces
{
    public interface ISearchEngineService
    {
        List<ViewProducts> Search(string searchItem,string user, string orderBy);

        List<ViewProducts> ViewProducts(string userId,string orderBy);

        public List<ViewProducts> ViewProductsWithPage(string userId, string orderBy, int page = 1,int pageSize = 5);

        int GetAllCount();
    }
}
