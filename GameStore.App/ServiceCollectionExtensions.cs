using Microsoft.Extensions.DependencyInjection;

namespace GameStore.App
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddProductService(this IServiceCollection services)
        {
            return services;
        }
    }
}