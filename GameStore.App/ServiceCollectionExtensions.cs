using System;
using GameStore.DataModel;
using GameStore.Domain.Products;
using GameStore.Foundation;
using GameStore.Infrastructure.Database.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace GameStore.App
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddProductService(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            
            return services;
        }

        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.Scan(
                x =>
                {
                    var assemblies = AppDomain.CurrentDomain.GetAssemblies();

                    x.FromAssemblies(assemblies)
                        .AddClasses(classes => classes.AssignableTo(typeof(IMapper<,>)))
                        .AsImplementedInterfaces().WithScopedLifetime();
                });
            return services;
        }

        public static IServiceCollection AddGameStoreDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("GameStoreDB");

            services.AddDbContext<GameStoreDbContext>(builder => builder.UseSqlServer(connectionString));
            
            return services;
        }
        
        public static IServiceCollection AddSwagger(this IServiceCollection services, SwaggerSetting swaggerSetting)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(swaggerSetting.Version, new OpenApiInfo
                {
                    Version = swaggerSetting.Version,
                    Title = swaggerSetting.Title,
                    Description = swaggerSetting.Description,
                    TermsOfService = new Uri(swaggerSetting.TermOfServiceUrl)
                });
            });

            return services;
        }
    }
}