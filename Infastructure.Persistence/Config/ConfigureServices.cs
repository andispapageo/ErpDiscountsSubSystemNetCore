using Domain.Core;
using Domain.Core.Interfaces;
using Infastructure.Data;
using Infastructure.Persistence.Data.Seeding;
using Infastructure.Persistence.Repositories;
using Infastructure.Persistence.UnitOfWork;
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

            services.AddTransient<SeedingInitializer>();

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            return services;
        }

        public static void ApplyMigrations(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            using (var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
            {
                try
                {
                    var seedingInitializer = scope.ServiceProvider.GetRequiredService<SeedingInitializer>();
                    appContext.Database.Migrate();
                    appContext.Database.EnsureCreated();

                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message, ex);
                    throw;
                }
            }
        }

        public static void ApplySeeding(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            using (var seedingInitializer = scope.ServiceProvider.GetRequiredService<SeedingInitializer>())
            {
                var resMigration = seedingInitializer.Seed().Result;
                Log.Information("Migrations and Seed are {0}", resMigration < 0 ? "not applied" : "applied successfully");
            }
        }
    }
}
