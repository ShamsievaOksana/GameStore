using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.DataModel;
using GameStore.Domain.Products;
using GameStore.Domain.Products.Exceptions;
using GameStore.Foundation;
using Microsoft.EntityFrameworkCore;

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
        
        public async Task<Product> Add(Product product)
        {
            product.ShouldNotBeNull(nameof(product));

            var productEntity = _productToProductEntityMapper.Map(product);

            _context.Add(productEntity);
            await _context.SaveChangesAsync();

            productEntity = await _context.Products.FirstOrDefaultAsync(x => x.Id == productEntity.Id);

            return _productEntityToProductMapper.Map(productEntity);
        }

        public async Task<Product> Update(Product product)
        {
            product.ShouldNotBeNull(nameof(product));
            
            var productEntity = await _context.Products.FirstOrDefaultAsync(x => x.Id == product.Id);

            if (productEntity == null)
                throw new ProductNotFoundException(product.Id);
            
            _productToProductEntityMapper.Map(product, productEntity);
            _context.Update(productEntity);
            await _context.SaveChangesAsync();
            
            productEntity = await _context.Products.FirstOrDefaultAsync(x => x.Id == productEntity.Id);

            return _productEntityToProductMapper.Map(productEntity);
        }

        public async Task<Product> Get(int id)
        {
            id.ShouldBeGreaterThanZero(nameof(id));
            
            var productEntity = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            return productEntity == null
                ? null
                : _productEntityToProductMapper.Map(productEntity);
        }

        public async Task<IList<Product>> Get()
        {
            var productEntities = await _context.Products.ToListAsync();

            return productEntities.Select(x => _productEntityToProductMapper.Map(x)).ToList();
        }

        public async Task Delete(int id)
        {
            id.ShouldBeGreaterThanZero(nameof(id));
            
            var productEntity = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if(productEntity == null)
                return;

            _context.Remove(productEntity);
            await _context.SaveChangesAsync();
        }
    }
}