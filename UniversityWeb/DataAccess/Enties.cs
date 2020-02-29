using System;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.UserRental;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class Enties : DbContext
    {
        public Enties(DbContextOptions<Enties> options) : base(options)
        {
           
        }

        public DbSet<UserRentals> rental { get; set; }

        //public DbSet<Movies> movieses { get; set; }
    }
}
