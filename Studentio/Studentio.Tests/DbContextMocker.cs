using Microsoft.EntityFrameworkCore;
using Studentio.Entities.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Studentio.Tests
{
    public static class DbContextMocker
    {
        public static RepositoryContext GetRepositoryContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>()
                         .UseInMemoryDatabase(databaseName: dbName)
                         .Options;

            var context = new RepositoryContext(options);

            context.Seed();

            return context;
        }
    }
}
