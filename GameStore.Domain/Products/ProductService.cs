using System.Collections.Generic;
using System.Threading.Tasks;

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
        
        public Task<Product> CreateProduct(Product product)
        {
            throw new System.NotImplementedException();
        }

        public Task<Product> UpdateProduct(Product product)
        {
            throw new System.NotImplementedException();
        }

        public Task<Product> GetProductById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<Product>> GetProducts()
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteProduct(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}