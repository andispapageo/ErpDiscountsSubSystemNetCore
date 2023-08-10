using Integration.Tests.Interfaces;
using Integration.Tests.Services;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Integration.Tests
{
    [SetUpFixture]
    public partial class TestingUnit
    {
        private static ITestDb _database;
        private static CustomWebApplicationFactory _factory = null!;
        private static IServiceScopeFactory _scopeFactory = null!;
        private static string? _userId;

        [OneTimeSetUp]
        public async Task RunBeforeAnyTests()
        {
            _database = await TestDatabaseFactory.CreateAsync();

            _factory = new CustomWebApplicationFactory(_database.GetConnection());
            _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        }

    }
}
