using System;
using FluentAssertions;
using GameStore.Domain.Products;
using GameStore.Infrastructure.Database.Products;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace GameStore.App.Tests
{
    [TestFixture]
    public class ServiceCollectionExtensionsTests
    {
        private IServiceCollection _serviceCollection;
        private IServiceProvider _serviceProvider;

        [SetUp]
        public void Setup()
        {
            _serviceCollection = new ServiceCollection();

            _serviceCollection.AddProductService();

            _serviceProvider = _serviceCollection.BuildServiceProvider();
        }
        
        [TestCase(typeof(IProductRepository), typeof(ProductRepository))]
        [TestCase(typeof(IProductService), typeof(ProductService))]
        public void GetService_ShouldResolveService(Type serviceType, Type implementationType)
        {
            _serviceProvider.GetService(serviceType).Should().BeOfType(implementationType);
        }
    }
}