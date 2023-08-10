namespace ErpDiscountsSubSystemNetCore.Config
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddHttpContextAccessor();
            services.AddHealthChecks();

            return services;
        }
    }
}
