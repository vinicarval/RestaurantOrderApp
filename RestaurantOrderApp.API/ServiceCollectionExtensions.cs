using Microsoft.Extensions.DependencyInjection;
using RestaurantOrderApp.Application.Services;
using RestaurantOrderApp.Core.Interfaces.Repositories;
using RestaurantOrderApp.Core.Interfaces.Services;
using RestaurantOrderApp.Infrastructure.Persistence.Repositories;

namespace RestaurantOrderApp.API
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IDishService, DishService>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IDishRepository, DishRepository>();

            return services;
        }
    }
}
