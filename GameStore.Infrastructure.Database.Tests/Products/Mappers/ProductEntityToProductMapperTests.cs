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
    public class ProductEntityToProductMapperTests
    {
        private ProductEntityToProductMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = new ProductEntityToProductMapper();
        }

        [Test]
        public void Map_SourceIsNull_ShouldThrow()
        {
            // Act
            var act = new Action(() => _mapper.Map(null, new Product()));
            
            // Assert
            act.Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void Map_TargetIsNull_ShouldThrow()
        {
            // Act
            var act = new Action(() => _mapper.Map(new ProductEntity(), null));
            
            // Assert
            act.Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void Map_ShouldMapProductId()
        {
            // Arrange 
            var source = Builder<ProductEntity>
                .CreateNew()
                .With(x=>x.Id, 10)
                .Build();
            
            // Act
            var target = _mapper.Map(source);
            
            // Assert
            target.Id.Should().Be(source.Id);
        }
        
        [Test]
        public void Map_ShouldBeSuccessful()
        {
            // Arrange 
            var source = Builder<ProductEntity>.CreateNew().Build();
            
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