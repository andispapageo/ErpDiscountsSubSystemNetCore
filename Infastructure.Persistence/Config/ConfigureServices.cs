using Infastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Infastructure.Persistence.Config
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfastructureServices(this IServiceCollection services, IConfigurationRoot configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            return services;
        }

        public static void ApplyMigrations(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            using (var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
            {
                try
                {
                    appContext.Database.Migrate();
                    appContext.Database.EnsureCreated();
                    var resMigration = appContext.Seed().Result;
                    Log.Information("Migrations and Seed are {0}", resMigration < 0 ? "not applied" : "applied successfully");
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message, ex);
                    throw;
                }
            }
        }
    }
}
