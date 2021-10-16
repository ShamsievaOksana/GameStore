using System;
using System.Linq;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using FluentAssertions;
using GameStore.DataModel;
using GameStore.Domain.Products;
using GameStore.Domain.Products.Exceptions;
using GameStore.Foundation;
using GameStore.Infrastructure.Database.Products;
using GameStore.Infrastructure.Database.Products.Mappers;
using NUnit.Framework;

namespace GameStore.Infrastructure.Database.Tests.Products
{
    public class ProductRepositoryTests
    {
        private IMapper<Product, ProductEntity> _productToProductEntityMapper;
        private IMapper<ProductEntity, Product>  _productEntityToProductMapper;
        private InMemoryGameStoreDbContext _context;

        private ProductRepository _repository;

        [SetUp]
        public void Setup()
        {
            _productToProductEntityMapper = new ProductToProductEntityMapper();
            _productEntityToProductMapper = new ProductEntityToProductMapper();
            _context = new InMemoryGameStoreDbContext();

            _repository = new ProductRepository(_context, _productToProductEntityMapper,
                _productEntityToProductMapper);
        }

        [Test]
        public void Add_ProductIsNull_ShouldThrow()
        {
            // Act
            var act = new Func<Task>(async () => await _repository.Add(null));
            
            // Assert
            act.Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public async Task Add_ShouldAddProductAndAssignProductId()
        {
            // Arrange
            var product = Builder<Product>.CreateNew()
                .With(x => x.Id, 0)
                .Build();
            
            // Act
            var newProduct = await _repository.Add(product);
            
            // Assert
            newProduct.Id.Should().BeGreaterThan(0);
        }
        
        [Test]
        public void Update_ProductDoesntExist_ShouldThrow()
        {
            // Arrange
            var product = Builder<Product>.CreateNew()
                .With(x => x.Id, 10)
                .Build();
            
            // Act
            var act = new Func<Task>(async () => await _repository.Update(product));
            
            // Assert
            act.Should().Throw<ProductNotFoundException>();
        }
        
        [Test]
        public async Task Update_ProductExists_ShouldUpdateProduct()
        {
            // Arrange
            var productEntity = Builder<ProductEntity>.CreateNew()
                .With(x => x.Id, 0)
                .With(x=>x.Published, false)
                .Build();

            _context.Add(productEntity);
            await _context.SaveChangesAsync();
            
            var product = Builder<Product>.CreateNew()
                .With(x => x.Id, productEntity.Id)
                .With(x=>x.Name, "New product name")
                .With(x=>x.Description, "New product description")
                .With(x=>x.Price, 299)
                .With(x=>x.Published, true)
                .Build(); 
            
            // Act
            var updatedProduct = await _repository.Update(product);
            
            // Assert
            updatedProduct.Id.Should().Be(productEntity.Id);
            updatedProduct.Name.Should().Be("New product name");
            updatedProduct.Description.Should().Be("New product description");
            updatedProduct.Price.Should().Be(299);
            updatedProduct.Published.Should().BeTrue();
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Delete_IdIsInvalid_ShouldThrow(int productId)
        {
            // Act
            var act = new Func<Task>(async () => await _repository.Delete(productId));
            
            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Delete_ProductDoesntExist_ShouldDoNothing()
        {
            // Act
            var act = new Func<Task>(async () => await _repository.Delete(10));
            
            // Assert
            act.Should().NotThrow();
        }
        
        [Test]
        public async Task Delete_ProductExist_ShouldDelete()
        {
            // Arrange
            var productEntity = Builder<ProductEntity>
                .CreateNew()
                .With(x => x.Id, 0)
                .Build();

            _context.Add(productEntity);
            await _context.SaveChangesAsync();
            _context.Products.FirstOrDefault(x => x.Id == productEntity.Id).Should().NotBeNull();
            
            // Act
            await _repository.Delete(productEntity.Id);
            
            // Assert
            _context.Products.FirstOrDefault(x => x.Id == productEntity.Id).Should().BeNull();
        }
        
        [TestCase(0)]
        [TestCase(-1)]
        public void Get_IdIsInvalid_ShouldThrow(int productId)
        {
            // Act
            var act = new Func<Task>(async () => await _repository.Get(productId));
            
            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>();
        }
        
        [Test]
        public async Task Get_ById_ProductExist_ShouldGetProduct()
        {
            // Arrange
            var productEntity = Builder<ProductEntity>
                .CreateNew()
                .With(x => x.Id, 0)
                .Build();

            _context.Add(productEntity);
            await _context.SaveChangesAsync();
            
            
            // Act
            var product = await _repository.Get(productEntity.Id);
            
            // Assert
            product.Should().NotBeNull();
        }
        
        [Test]
        public async Task Get_ById_ProductDoesntExist_ShouldReturnNull()
        {
            // Arrange
            var productEntity = Builder<ProductEntity>
                .CreateNew()
                .With(x => x.Id, 0)
                .Build();

            _context.Add(productEntity);
            await _context.SaveChangesAsync();
            
            
            // Act
            var products = await _repository.Get(productEntity.Id + 1);
            
            // Assert
            products.Should().BeNull();
        }
        
        [Test]
        public async Task Get_NoProducts_ShouldReturnEmpty()
        {
            // Act
            var product = await _repository.Get();
            
            // Assert
            product.Should().BeEmpty();
        }
        
        [Test]
        public async Task Get_ShouldReturnListOfTheProducts()
        {
            // Arrange
            _context.Add(Builder<ProductEntity>
                .CreateNew()
                .With(x => x.Id, 0)
                .Build());
            
            _context.Add(Builder<ProductEntity>
                .CreateNew()
                .With(x => x.Id, 0)
                .Build());
            
            _context.Add(Builder<ProductEntity>
                .CreateNew()
                .With(x => x.Id, 0)
                .Build());
            
            await _context.SaveChangesAsync();
            
            
            // Act
            var products = await _repository.Get();
            
            // Assert
            products.Should().HaveCount(3);
        }

        [Test]
        public async Task Add_ShouldSetCreatedAndModifiedDate()
        {
            // Arrange
            var product = new Product
            {
                Name = "Product Name",
                Description = "Product Description",
                Price = 10
            };
            
            // Act
            product = await _repository.Add(product);
            
            // Assert
            var productEntity = await _context.Products.FindAsync(product.Id);
            (productEntity.Created > DateTime.MinValue).Should().BeTrue();
            (productEntity.Modified > DateTime.MinValue).Should().BeTrue();
        }
        
        [Test]
        public async Task Update_ShouldSetModifiedDate()
        {
            // Arrange
            var product = new Product
            {
                Name = "Product Name",
                Description = "Product Description",
                Price = 10
            };
            
            // Act
            product = await _repository.Add(product);
            var productEntity = await _context.Products.FindAsync(product.Id);
            var modifiedDateAfterAdding = productEntity.Modified;
            
            product.Published = true;
            product = await _repository.Update(product);
            productEntity = await _context.Products.FindAsync(product.Id);
            var modifiedDateAfterUpdating = productEntity.Modified;
            
            // Assert
            (modifiedDateAfterUpdating > modifiedDateAfterAdding).Should().BeTrue();
        }

        [TearDown]
        public void TearDown()
        {
            _context?.Dispose();
        }
    }
}