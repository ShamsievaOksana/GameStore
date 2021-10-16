using System;
using FizzWare.NBuilder;
using FluentAssertions;
using GameStore.App.Mappers.Products;
using GameStore.Domain.Products;
using GameStore.WebClient.Product;
using NUnit.Framework;

namespace GameStore.App.Tests.Mappers.Products
{
    [TestFixture]
    public class ProductToProductModelMapperTests
    {
        private ProductToProductModelMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = new ProductToProductModelMapper();
        }

        [Test]
        public void Map_SourceIsNull_ShouldThrow()
        {
            // Act
            var act = new Action(() => _mapper.Map(null, new ProductModel()));
            
            // Assert
            act.Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void Map_TargetIsNull_ShouldThrow()
        {
            // Act
            var act = new Action(() => _mapper.Map(new Product(), null));
            
            // Assert
            act.Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void Map_ShouldMapAllProperties()
        {
            // Arrange
            var source = Builder<Product>.CreateNew().Build();
            
            // Act
            var target = _mapper.Map(source);
            
            // Assert
            target.Id.Should().Be(source.Id);
            target.Name.Should().Be(source.Name);
            target.Description.Should().Be(source.Description);
            target.Price.Should().Be(source.Price);
            target.Published.Should().Be(source.Published);
        }
    }
}