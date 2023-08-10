using Integration.Tests.DatabaseTypes;
using Integration.Tests.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Tests
{
    public static class TestDatabaseFactory
    {
        public static async Task<ITestDb> CreateAsync()
        {
#if DEBUG
            var database = new SqlServerTestDatabase();
#else
        var database = new TestcontainersTestDatabase();
#endif
            await database.InitialiseAsync();
            return database;
        }
    }
}
