using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MBshop.Service.OutputModels;

namespace MBshop.Models
{
    public class ProductsViewPageListingModel
    {
        public IEnumerable<ViewProducts> Products { get; set; }

        public int CurrentPage { get; set; }

        public int PreviosPage => this.CurrentPage == 1 
            ? 1 
            : this.CurrentPage - 1;

        public int TotalPages { get; set; }

        public int NextPage => this.CurrentPage == this.TotalPages
            ? this.TotalPages
            : this.CurrentPage;
    }   
}
