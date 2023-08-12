using Application.Shared.Config;
using ErpDiscountsSubSystemNetCore.Diagnostics;
using Infastructure.Persistence.Config;
using Serilog;
using Serilog.Exceptions;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddApplicationServices(builder.Configuration);
    builder.Services.AddInfastructureServices(builder.Configuration);
    builder.Services.AddControllersWithViews();

    builder.Host.UseSerilog((hostContext, services, configuration) =>
    {
        configuration.WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception} {Properties:j}");
        configuration.Enrich.FromLogContext();
        configuration.Enrich.WithThreadId();
        configuration.Enrich.WithExceptionDetails();
        configuration.Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name);
        configuration.Destructure.ToMaximumDepth(10);
    });

    var app = builder.Build();

    app.ApplyMigrations();
    app.ApplySeeding();
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseSerilogRequestLogging();
    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();
    app.UseMiddleware<SerilogMiddleware>();
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.MapRazorPages();
    app.Run();
}
catch (Exception ex)
{
    if (Log.Logger == null || Log.Logger.GetType().Name == "SilentLogger")
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();
    }

    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}
public partial class Program { }