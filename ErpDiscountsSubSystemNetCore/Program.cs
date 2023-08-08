using Infastructure.Data;
using Infastructure.Persistence.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfastructureServices(builder.Configuration);
builder.Services.AddControllersWithViews();
builder.Host.UseSerilog((hostContext, services, configuration) => configuration.WriteTo.Console());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    using (var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
    {
        try
        {
            appContext.Database.Migrate();
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message, ex);
            throw;
        }
    }
}


app.Run();
