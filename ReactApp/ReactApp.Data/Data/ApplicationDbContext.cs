//using ReactApp.Models;
//using IdentityServer4.EntityFramework.Options;
//using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ReactApp.Data.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactApp.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<User>
    {

        public virtual DbSet<User> User { get; set; }

        public virtual DbSet<AllProducts> AllProducts { get; set; }

        public virtual DbSet<Reviews> Reviews { get; set; }

        public virtual DbSet<VideoCards> VideoCards { get; set; }

        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }




    }
}
