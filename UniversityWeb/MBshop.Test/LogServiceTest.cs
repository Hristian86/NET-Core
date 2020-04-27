using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MBshop.Data.Data;
using MBshop.Models;
using MBshop.Service.interfaces;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MBshop.Test
{
    public class LogServiceTest
    {
        [Fact]
        public void ShouldReturnSuccessOnLogUser()
        {

            var service = new LogService();

            var result = service.LoggedUser("Go6o");

            Assert.Equal("Success",result.Result);

        }

        [Fact]
        public void ShouldReturnNotFoundOnLogUserWithEmptyString()
        {

            var service = new LogService();

            var result = service.LoggedUser("");

            Assert.Equal("Not found", result.Result);

        }

        [Fact]
        public void ShouldReturnNotFoundOnLogUserWithNull()
        {

            var service = new LogService();

            var result = service.LoggedUser(null);

            Assert.Equal("Not found", result.Result);

        }

    }

    public class LogService
    {
        public async Task<string> LoggedUser(string userName)
        {
            var options = new DbContextOptionsBuilder<MovieShopDBSEContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            MovieShopDBSEContext db = new MovieShopDBSEContext(options);

            if (userName == null || userName == "")
            {
                return $"Not found";
            }

            Logs logUser = new Logs
            {
                UserLoged = userName,
            };

            db.Logs.Add(logUser);

            await db.SaveChangesAsync();

            return $"Success";
        }
    }
}
