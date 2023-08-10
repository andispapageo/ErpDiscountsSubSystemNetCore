using Functional.Tests.Interfaces;
using Infastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Respawn;
using System.Data.Common;
using Testcontainers.MsSql;

namespace Functional.Tests.DatabaseTypes
{
    public class TestcontainersTestDatabase : ITestDb
    {
        private readonly MsSqlContainer _container;
        private DbConnection _connection = null!;
        private string _connectionString = null!;
        private Respawner _respawner = null!;
        public TestcontainersTestDatabase()
        {
            _container = new MsSqlBuilder()
                .WithAutoRemove(true)
                .Build();
        }

        public async Task InitialiseAsync()
        {
            await _container.StartAsync();
            _connectionString = _container.GetConnectionString();
            _connection = new SqlConnection(_connectionString);

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(_connectionString)
                .Options;

            var context = new ApplicationDbContext(options);
            context.Database.Migrate();
            _respawner = await Respawner.CreateAsync(_connectionString, new RespawnerOptions
            {
                TablesToIgnore = new Respawn.Graph.Table[] { "__EFMigrationsHistory" }
            });
        }

        public DbConnection GetConnection() => _connection;
        public async Task ResetAsync() => await _respawner.ResetAsync(_connectionString);

        public async Task DisposeAsync()
        {
            await _connection.DisposeAsync();
            await _container.DisposeAsync();
        }
    }
}
