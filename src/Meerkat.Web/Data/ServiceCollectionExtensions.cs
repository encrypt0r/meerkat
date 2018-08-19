using Meerkat.Web.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Meerkat.Web.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationData(this IServiceCollection services)
        {
            services.AddScoped<IEventGroupsRepository, EventGroupsRepository>();
            services.AddScoped<IEventsRepository, EventsRepository>();
            services.AddScoped<IUnitOfWork, EfUnitOfWork>();

            return services;
        }
    }
}
