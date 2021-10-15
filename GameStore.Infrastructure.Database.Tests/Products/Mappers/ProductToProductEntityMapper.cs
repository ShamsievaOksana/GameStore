using GameStore.DataModel;
using GameStore.Domain.Products;
using GameStore.Foundation;

namespace GameStore.Infrastructure.Database.Tests.Products.Mappers
{
    public class ProductToProductEntityMapper
        : Mapper<Product, ProductEntity>
    {
        public override void Map(Product source, ProductEntity target)
        {
            throw new System.NotImplementedException();
        }
    }
}