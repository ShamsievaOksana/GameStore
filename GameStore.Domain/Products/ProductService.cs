using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.Domain.Products.Exceptions;
using GameStore.Foundation;

namespace GameStore.Domain.Products
{
    public class ProductService
        : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        
        public async Task<Product> CreateProduct(Product product)
        {
            product.ShouldNotBeNull(nameof(product));
            
            var newProduct = await _productRepository.Add(product);

            if (newProduct == null || newProduct.Id == 0)
                throw new ProductCouldNotBeStoredException();
            
            return newProduct;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            product.ShouldNotBeNull(nameof(product));
            
            var productId = product.Id;
            var updatedProduct = await _productRepository.Update(product);

            if (updatedProduct == null)
                throw new ProductCouldNotBeUpdatedException();

            if (updatedProduct.Id != productId)
                throw new ProductMismatchException(productId, updatedProduct.Id);

            return updatedProduct;
        }

        public async Task<Product> GetProductById(int id)
        {
            id.ShouldBeGreaterThanZero(nameof(id));

            var product = await _productRepository.Get(id);

            if (product == null)
                throw new  ProductNotFoundException(id);

            if (product.Id != id)
                throw new ProductMismatchException(id, product.Id);
            
            return product;
        }

        public async Task<IList<Product>> GetProducts()
        {
            var products = await _productRepository.Get();

            return products ?? new List<Product>();
        }

        public async Task DeleteProduct(int id)
        {
            id.ShouldBeGreaterThanZero(nameof(id));
            
            await _productRepository.Delete(id);
        }
    }
}