using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using UniversityWeb.Models;

namespace UniversityWeb.Data
{
    public class MovieRentalContext : DbContext
    {
        public MovieRentalContext()
        {
        }

        public MovieRentalContext(DbContextOptions<MovieRentalContext> options) : base(options)
        {

        }

        //DbSet<UserRental> remt { get; set; }
        
        DbSet<Movies> movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<MovieTable>
        }




        //DbSet<UserRental> userRental { get; set; }
    }
}
