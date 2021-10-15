using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using FluentAssertions;
using GameStore.App.Controllers;
using GameStore.Domain.Products;
using GameStore.Foundation;
using GameStore.WebClient.Product;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace GameStore.App.Tests.Controllers
{
    [TestFixture]
    public class ProductControllerTests
    {
        private Mock<IMapper<ProductModel, Product>> _productModelToProductMapperMock;
        private Mock<IMapper<Product, ProductModel>> _productToProductModelMapperMock;
        private Mock<IProductService> _productServiceMock;
        private ProductController _controller;
        
        [SetUp]
        public void Setup()
        {
            _productModelToProductMapperMock = new Mock<IMapper<ProductModel, Product>>();
            _productToProductModelMapperMock = new Mock<IMapper<Product, ProductModel>>();
            _productServiceMock = new Mock<IProductService>();

            _productModelToProductMapperMock.Setup(x => x.Map(It.IsAny<ProductModel>()))
                .Returns(new Product());

            _productToProductModelMapperMock.Setup(x => x.Map(It.IsAny<Product>()))
                .Returns(new ProductModel());
            
            _controller = new ProductController(_productServiceMock.Object,
                _productModelToProductMapperMock.Object, 
                _productToProductModelMapperMock.Object);
        }

        [Test]
        public async Task Post_ShouldCreateProduct()
        {
            // Arrange
            _productServiceMock.Setup(x => x.CreateProduct(It.IsAny<Product>()))
                .ReturnsAsync(new Product());

            var productModel = Builder<ProductModel>.CreateNew().Build();
            
            // Act
            var actionResult = await _controller.Post(productModel);
            
            // Assert
            var createdResult = actionResult as CreatedResult;
            createdResult.Should().NotBeNull();
            createdResult.Value.Should().BeOfType<ProductModel>();
        }
        
        [Test]
        public async Task Post_ServiceReturnedNull_ShouldReturnInternalServerErrorStatusCode()
        {
            // Arrange
            _productServiceMock.Setup(x => x.CreateProduct(It.IsAny<Product>()))
                .ReturnsAsync(null as Product);

            var productModel = Builder<ProductModel>.CreateNew().Build();
            
            // Act
            var actionResult = await _controller.Post(productModel);
            
            // Assert
            var createdResult = actionResult as StatusCodeResult;
            createdResult.Should().NotBeNull();
            createdResult.StatusCode.Should().Be(500);
        }
        
        [Test]
        public void Post_ServiceThrewException_ShouldReThrow()
        {
            // Arrange
            _productServiceMock.Setup(x => x.CreateProduct(It.IsAny<Product>()))
                .Throws<Exception>();

            var productModel = Builder<ProductModel>.CreateNew().Build();
            
            // Act
            var act = new Func<Task>(async ()=> await _controller.Post(productModel));
            
            // Assert
            act.Should().Throw<Exception>();
        }
        
        [Test]
        public async Task Put_ShouldUpdateProduct()
        {
            // Arrange
            _productServiceMock.Setup(x => x.UpdateProduct(It.IsAny<Product>()))
                .ReturnsAsync(new Product());

            var productModel = Builder<ProductModel>.CreateNew().Build();
            
            // Act
            var actionResult = await _controller.Put(productModel);
            
            // Assert
            var okObjectResult = actionResult as OkObjectResult;
            okObjectResult.Should().NotBeNull();
            okObjectResult.Value.Should().BeOfType<ProductModel>();
        }
        
        [Test]
        public async Task Put_ServiceReturnedNull_ShouldReturnInternalServerErrorStatusCode()
        {
            // Arrange
            _productServiceMock.Setup(x => x.UpdateProduct(It.IsAny<Product>()))
                .ReturnsAsync(null as Product);

            var productModel = Builder<ProductModel>.CreateNew().Build();
            
            // Act
            var actionResult = await _controller.Put(productModel);
            
            // Assert
            var statusCodeResult = actionResult as StatusCodeResult;
            statusCodeResult.Should().NotBeNull();
            statusCodeResult.StatusCode.Should().Be(500);
        }
        
        [Test]
        public void Put_ServiceThrewException_ShouldReThrow()
        {
            // Arrange
            _productServiceMock.Setup(x => x.UpdateProduct(It.IsAny<Product>()))
                .Throws<Exception>();

            var productModel = Builder<ProductModel>.CreateNew().Build();
            
            // Act
            var act = new Func<Task>(async ()=> await _controller.Put(productModel));
            
            // Assert
            act.Should().Throw<Exception>();
        }
        
        [Test]
        public async Task Delete_ShouldDeleteProduct()
        {
            // Act
            var actionResult = await _controller.Delete(10);
            
            // Assert
            var noContentResult = actionResult as NoContentResult;
            noContentResult.Should().NotBeNull();
        }
        
        
        [Test]
        public void Delete_ServiceThrewException_ShouldReThrow()
        {
            // Arrange
            _productServiceMock.Setup(x => x.DeleteProduct(It.IsAny<int>()))
                .Throws<Exception>();

            // Act
            var act = new Func<Task>(async ()=> await _controller.Delete(10));
            
            // Assert
            act.Should().Throw<Exception>();
        }
        
        [Test]
        public async Task Get_AllProducts_ShouldBeSuccessful()
        {
            // Arrange
            _productServiceMock.Setup(x => x.GetProducts())
                .ReturnsAsync(new List<Product> {new(), new(), new()});
                
            // Act
            var actionResult = await _controller.Get();
            
            // Assert
            var okObjectResult = actionResult as OkObjectResult;
            okObjectResult.Should().NotBeNull();
            okObjectResult.Value.Should().NotBeNull();
            okObjectResult.Value.Should().BeOfType<List<ProductModel>>();
        }
        
        [Test]
        public async Task Get_AllProducts_ServiceReturnedNull_ShouldBeSuccessful()
        {
            // Arrange
            _productServiceMock.Setup(x => x.GetProducts())
                .ReturnsAsync(null as IList<Product>);
                
            // Act
            var actionResult = await _controller.Get();
            
            // Assert
            var okObjectResult = actionResult as OkObjectResult;
            okObjectResult.Should().NotBeNull();
            okObjectResult.Value.Should().NotBeNull();
            okObjectResult.Value.Should().BeOfType<List<ProductModel>>();
        }
        
        [Test]
        public async Task Get_AllProducts_ServiceReturnedEmptyCollection_ShouldBeSuccessful()
        {
            // Arrange
            _productServiceMock.Setup(x => x.GetProducts())
                .ReturnsAsync(new List<Product>());
                
            // Act
            var actionResult = await _controller.Get();
            
            // Assert
            var okObjectResult = actionResult as OkObjectResult;
            okObjectResult.Should().NotBeNull();
            okObjectResult.Value.Should().NotBeNull();
            okObjectResult.Value.Should().BeOfType<List<ProductModel>>();
        }
        
        [Test]
        public void Get_AllProducts_ServiceThrewException_ShouldReThrow()
        {
            // Arrange
            _productServiceMock.Setup(x => x.GetProducts())
                .Throws<Exception>();

            // Act
            var act = new Func<Task>(async () => await _controller.Get());
            
            // Assert
            act.Should().Throw<Exception>();
        }
        
        [Test]
        public async Task Get_ById_ShouldBeSuccessful()
        {
            // Arrange
            _productServiceMock.Setup(x => x.GetProductById(It.IsAny<int>()))
                .ReturnsAsync(new Product());
                
            // Act
            var actionResult = await _controller.Get(10);
            
            // Assert
            var okObjectResult = actionResult as OkObjectResult;
            okObjectResult.Should().NotBeNull();
            okObjectResult.Value.Should().NotBeNull();
            okObjectResult.Value.Should().BeOfType<ProductModel>();
        }
        
        [Test]
        public async Task Get_ById_ShouldReturnNotFound()
        {
            // Arrange
            _productServiceMock.Setup(x => x.GetProductById(It.IsAny<int>()))
                .ReturnsAsync(null as Product);
                
            // Act
            var actionResult = await _controller.Get(10);
            
            // Assert
            var notFoundResult = actionResult as NotFoundResult;
            notFoundResult.Should().NotBeNull();
        }
        
        [Test]
        public void Get_ById_ServiceThrewException_ShouldReThrow()
        {
            // Arrange
            _productServiceMock.Setup(x => x.GetProductById(It.IsAny<int>()))
                .Throws<Exception>();

            // Act
            var act = new Func<Task>(async () => await _controller.Get(10));
            
            // Assert
            act.Should().Throw<Exception>();
        }
    }
}