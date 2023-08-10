using Infastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;

namespace Functional.Tests.Services
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly DbConnection _connection;
        public CustomWebApplicationFactory(DbConnection connection) => _connection = connection;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddDbContext<ApplicationDbContext>((sp, options) =>
                {
                    options.UseSqlServer(_connection);
                });
            });
        }
    }
}
