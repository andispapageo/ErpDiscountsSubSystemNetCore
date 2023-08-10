using Functional.Tests.Interfaces;
using Infastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Respawn;
using System.Data.Common;

namespace Functional.Tests.DatabaseTypes
{
    public class SqlServerTestDatabase : ITestDb
    {
        private readonly string _connectionString = null!;
        private SqlConnection _connection = null!;
        private Respawner _respawner = null!;

        public SqlServerTestDatabase()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            _connectionString = connectionString;
        }

        public async Task InitialiseAsync()
        {
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
        public async Task DisposeAsync() => await _connection.DisposeAsync();
    }
}
