using System.ComponentModel.DataAnnotations;
using System.Linq;
using FizzWare.NBuilder;
using FluentAssertions;
using GameStore.WebClient.Product;
using NUnit.Framework;

namespace GameStore.WebClient.Tests
{
    [TestFixture]
    public class ProductModelTests
    {
        [Test]
        public void Validate_PriceIsMissing_ShouldNotBeValid()
        {
            // Arrange
            var productModel = Builder<ProductModel>
                .CreateNew()
                .With(x => x.Price, 0)
                .Build();

            // Act
            var validationResult = productModel.Validate(new ValidationContext(productModel));
            
            // Assert
            validationResult.Should().NotBeNullOrEmpty();
            validationResult.FirstOrDefault(x => x.MemberNames.Contains("Price")).Should().NotBeNull();
        }
        
        [Test]
        public void Validate_PriceHasNegativeValue_ShouldNotBeValid()
        {
            // Arrange
            var productModel = Builder<ProductModel>
                .CreateNew()
                .With(x => x.Price, -1)
                .Build();

            // Act
            var validationResult = productModel.Validate(new ValidationContext(productModel));
            
            // Assert
            validationResult.Should().NotBeNullOrEmpty();
            validationResult.FirstOrDefault(x => x.MemberNames.Contains("Price")).Should().NotBeNull();
        }
    }
}