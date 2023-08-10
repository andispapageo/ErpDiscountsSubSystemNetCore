using Functional.Tests.DatabaseTypes;
using Functional.Tests.Interfaces;

namespace Functional.Tests
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
