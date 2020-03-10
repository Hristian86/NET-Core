using System;
using System.Collections.Generic;
using System.Text;

namespace DataDomain
{
    public class ConnectionString
    {
        //private readonly string connectionString = "Server=.\\SQLEXPRESS;Database=MovieRentalDBS;Trusted_Connection=True;";

        //public string GetConnectionString()
        //{
        //    return connectionString;
        //}

        public static string ConString { get; set; }
    }
}