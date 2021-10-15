using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.DataModel;
using GameStore.Domain.Products;
using GameStore.Foundation;

namespace GameStore.Infrastructure.Database.Products
{
    public class ProductRepository
        : IProductRepository
    {
        private readonly GameStoreDbContext _context;
        private readonly IMapper<Product, ProductEntity> _productToProductEntityMapper;
        private readonly IMapper<ProductEntity, Product> _productEntityToProductMapper;

        public ProductRepository(GameStoreDbContext context,
            IMapper<Product, ProductEntity> productToProductEntityMapper,
            IMapper<ProductEntity, Product> productEntityToProductMapper)
        {
            _context = context;
            _productToProductEntityMapper = productToProductEntityMapper;
            _productEntityToProductMapper = productEntityToProductMapper;
        }
        
        public Task<Product> Add(Product product)
        {
            throw new System.NotImplementedException();
        }

        public Task<Product> Update(Product product)
        {
            throw new System.NotImplementedException();
        }

        public Task<Product> Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<Product>> Get()
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}