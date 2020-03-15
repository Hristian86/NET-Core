using System;
using System.Collections.Generic;
using System.Text;
using DataDomain.Data.Models;

namespace BusinessLogic.Services
{
    public class GetDataFromDatabase
    {
        //private MovieRentalDBSEContext db = new MovieRentalDBSEContext();
        private readonly MovieRentalDBSEContext db;
        public GetDataFromDatabase(MovieRentalDBSEContext dbs)
        {
            this.db = dbs;
        }
    }
}
