using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using GameStore.Domain.Products;
using GameStore.Domain.Products.Exceptions;
using Moq;
using NUnit.Framework;

namespace GameStore.Domain.Tests.Products
{
    [TestFixture]
    public class ProductServiceTests
    {
        private Mock<IProductRepository> _productRepositoryMock;
        private ProductService _service;

        [SetUp]
        public void Setup()
        {
            _productRepositoryMock = new Mock<IProductRepository>();

            _service = new ProductService(_productRepositoryMock.Object);
        }

        [Test]
        public void CreateProduct_ProductIsNull_ShouldThrow()
        {
            // Act
            var act = new Func<Task>(async () => await _service.CreateProduct(null));

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void CreateProduct_CreatedProductIsNull_ShouldThrow()
        {
            // Arrange
            _productRepositoryMock.Setup(x => x.Add(It.IsAny<Product>()))
                .ReturnsAsync(null as Product);
            
            // Act
            var act = new Func<Task>(async () => await _service.CreateProduct(new Product()));

            // Assert
            act.Should().Throw<ProductCouldNotBeStoredException>();
        }
        
        [Test]
        public void CreateProduct_ProductIdWasNotAssigned_ShouldThrow()
        {
            // Arrange
            _productRepositoryMock.Setup(x => x.Add(It.IsAny<Product>()))
                .ReturnsAsync(new Product {Id = 0});
            
            // Act
            var act = new Func<Task>(async () => await _service.CreateProduct(new Product()));

            // Assert
            act.Should().Throw<ProductCouldNotBeStoredException>();
        }
        
        [Test]
        public void CreateProduct_RepositoryThrewException_ShouldReThrow()
        {
            // Arrange
            _productRepositoryMock.Setup(x => x.Add(It.IsAny<Product>()))
                .Throws<Exception>();
            
            // Act
            var act = new Func<Task>(async () => await _service.CreateProduct(new Product()));

            // Assert
            act.Should().Throw<Exception>();
        }
        
        [Test]
        public async Task CreateProduct_ProductIdWasAssigned_ShouldBeSuccessful()
        {
            // Arrange
            _productRepositoryMock.Setup(x => x.Add(It.IsAny<Product>()))
                .ReturnsAsync(new Product {Id = 10});
            
            // Act
            var newProduct = await _service.CreateProduct(new Product());

            // Assert
            newProduct.Id.Should().Be(10);
        }
        
        [Test]
        public void UpdateProduct_ProductIsNull_ShouldThrow()
        {
            // Act
            var act = new Func<Task>(async () => await _service.UpdateProduct(null));

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void UpdateProduct_UpdatedProductIsNull_ShouldThrow()
        {
            // Arrange
            _productRepositoryMock.Setup(x => x.Update(It.IsAny<Product>()))
                .ReturnsAsync(null as Product);
            
            // Act
            var act = new Func<Task>(async () => await _service.UpdateProduct(new Product()));

            // Assert
            act.Should().Throw<ProductCouldNotBeUpdatedException>();
        }
        
        [Test]
        public void UpdateProduct_UpdatedProductIdIsNotTheSame_ShouldThrow()
        {
            // Arrange
            _productRepositoryMock.Setup(x => x.Update(It.IsAny<Product>()))
                .ReturnsAsync(new Product {Id = 20});
            
            // Act
            var act = new Func<Task>(async () => await _service.UpdateProduct(new Product {Id = 10}));

            // Assert
            act.Should().Throw<ProductMismatchException>();
        }
        
        [Test]
        public async Task UpdateProduct_UpdatedProductIdIsTheSame_ShouldBeSuccessful()
        {
            // Arrange
            const int productId = 10;

            _productRepositoryMock.Setup(x => x.Update(It.IsAny<Product>()))
                .ReturnsAsync(new Product {Id = productId});
            
            // Act
            var updatedProduct = await _service.UpdateProduct(new Product {Id = productId});

            // Assert
            updatedProduct.Id.Should().Be(productId);
        }
        
        [TestCase(0)]
        [TestCase(-1)]
        public void GetProductById_ProductIdIsInvalid_ShouldThrow(int productId)
        {
            // Act
            var act = new Func<Task>(async () => await _service.GetProductById(productId));

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>();
        }
        
        [Test]
        public void GetProductById_FetchedProductIdIsNotTheSame_ShouldThrow()
        {
            // Arrange
            _productRepositoryMock.Setup(x => x.Get(It.IsAny<int>()))
                .ReturnsAsync(new Product {Id = 20});
            
            // Act
            var act = new Func<Task>(async () => await _service.GetProductById(10));

            // Assert
            act.Should().Throw<ProductMismatchException>();
        }
        
        [Test]
        public void GetProductById_FetchedProductIsNull_ShouldThrow()
        {
            // Arrange
            _productRepositoryMock.Setup(x => x.Get(It.IsAny<int>()))
                .ReturnsAsync(null as Product);
            
            // Act
            var act = new Func<Task>(async () => await _service.GetProductById(10));

            // Assert
            act.Should().Throw<ProductNotFoundException>();
        }
        
        [Test]
        public void GetProductById_RepositoryThrewException_ShouldReThrow()
        {
            // Arrange
            _productRepositoryMock.Setup(x => x.Get(It.IsAny<int>()))
                .Throws<Exception>();
            
            // Act
            var act = new Func<Task>(async () => await _service.GetProductById(10));

            // Assert
            act.Should().Throw<Exception>();
        }
        
        [Test]
        public async Task GetProductById_FetchedProductIdIsTheSame_ShouldBeSuccessful()
        {
            // Arrange
            const int productId = 10;

            _productRepositoryMock.Setup(x => x.Get(It.IsAny<int>()))
                .ReturnsAsync(new Product {Id = productId});
            
            // Act
            var fetchedProduct = await _service.GetProductById(productId);

            // Assert
            fetchedProduct.Id.Should().Be(productId);
        }
        
        [Test]
        public void GetProducts_RepositoryThrewException_ShouldReThrow()
        {
            // Arrange
            _productRepositoryMock.Setup(x => x.Get())
                .Throws<Exception>();
            
            // Act
            var act = new Func<Task>(async () => await _service.GetProducts());

            // Assert
            act.Should().Throw<Exception>();
        }
        
        [Test]
        public async Task GetProducts_FetchedProductsListIsNull_ShouldReturnEmpty()
        {
            // Arrange
            _productRepositoryMock.Setup(x => x.Get())
                .ReturnsAsync(null as IList<Product>);
            
            // Act
            var fetchedProducts = await _service.GetProducts();

            // Assert
            fetchedProducts.Should().BeEmpty();
        }
        
        [Test]
        public async Task GetProducts_FetchedProductsListIsEmpty_ShouldReturnEmpty()
        {
            // Arrange
            _productRepositoryMock.Setup(x => x.Get())
                .ReturnsAsync(new List<Product>());
            
            // Act
            var fetchedProducts = await _service.GetProducts();

            // Assert
            fetchedProducts.Should().BeEmpty();
        }
        
        [Test]
        public async Task GetProducts_FetchedProductsListIsNotEmpty_ShouldBeSuccessful()
        {
            // Arrange
            _productRepositoryMock.Setup(x => x.Get())
                .ReturnsAsync(new List<Product>{new(), new(), new()});
            
            // Act
            var fetchedProducts = await _service.GetProducts();

            // Assert
            fetchedProducts.Should().HaveCount(3);
        }
        
        [TestCase(0)]
        [TestCase(-1)]
        public void DeleteProduct_IdIsInvalid_ShouldThrow(int productId)
        {
            // Act
            var act = new Func<Task>(async () => await _service.DeleteProduct(productId));

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>();
        }
        
        [Test]
        public void Delete_RepositoryThrewException_ShouldReThrow()
        {
            // Arrange
            _productRepositoryMock.Setup(x => x.Delete(It.IsAny<int>()))
                .Throws<Exception>();
            
            // Act
            var act = new Func<Task>(async () => await _service.DeleteProduct(10));

            // Assert
            act.Should().Throw<Exception>();
        }
        
        [Test]
        public async Task Delete_ShouldBeSuccessful()
        {
            // Arrange
            const int productId = 10;
            _productRepositoryMock.Setup(x => x.Delete(It.IsAny<int>()))
                .Returns(Task.CompletedTask);
            
            // Act
            await _service.DeleteProduct(productId);

            // Assert
            _productRepositoryMock.Verify(x => x.Delete(productId), Times.Once());
        }
    }
}