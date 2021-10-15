using GameStore.DataModel;
using GameStore.Domain.Products;
using GameStore.Foundation;

namespace GameStore.Infrastructure.Database.Tests.Products.Mappers
{
    public class ProductEntityToProductMapper
        : Mapper<ProductEntity, Product>
    {
        public override void Map(ProductEntity source, Product target)
        {
            throw new System.NotImplementedException();
        }
    }
}