using Microsoft.Extensions.DependencyInjection;

namespace Meerkat.Web.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationData(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, EfUnitOfWork>();

            return services;
        }
    }
}
