using Microsoft.Extensions.DependencyInjection;

namespace Meerkat.Web.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IEventStatisticsService, EventStatisticsService>();

            return services;
        }
    }
}
