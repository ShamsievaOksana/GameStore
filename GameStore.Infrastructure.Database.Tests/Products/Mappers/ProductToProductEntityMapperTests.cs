using System;
using FizzWare.NBuilder;
using FluentAssertions;
using GameStore.DataModel;
using GameStore.Domain.Products;
using GameStore.Infrastructure.Database.Products.Mappers;
using NUnit.Framework;

namespace GameStore.Infrastructure.Database.Tests.Products.Mappers
{
    [TestFixture]
    public class ProductToProductEntityMapperTests
    {
        private ProductToProductEntityMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = new ProductToProductEntityMapper();
        }

        [Test]
        public void Map_SourceIsNull_ShouldThrow()
        {
            // Act
            var act = new Action(() => _mapper.Map(null, new ProductEntity()));
            
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
        public void Map_ShouldNotMapProductId()
        {
            // Arrange 
            var source = Builder<Product>.CreateNew().Build();
            
            // Act
            var target = _mapper.Map(source);
            
            // Assert
            target.Id.Should().NotBe(source.Id);
        }
        
        [Test]
        public void Map_ShouldBeSuccessful()
        {
            // Arrange 
            var source = Builder<Product>.CreateNew().Build();
            
            // Act
            var target = _mapper.Map(source);
            
            // Assert
            target.Name.Should().Be(source.Name);
            target.Description.Should().Be(source.Description);
            target.Price.Should().Be(source.Price);
            target.Published.Should().Be(source.Published);
        }
    }
}