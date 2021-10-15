using GameStore.DataModel;
using GameStore.Domain.Products;
using GameStore.Foundation;

namespace GameStore.Infrastructure.Database.Products.Mappers
{
    public class ProductToProductEntityMapper
        : Mapper<Product, ProductEntity>
    {
        public override void Map(Product source, ProductEntity target)
        {
            source.ShouldNotNull(nameof(source));
            source.ShouldNotNull(nameof(target));

            target.Name = source.Name;
            target.Description = source.Description;
            target.Price = source.Price;
            target.Published = source.Published;
        }
    }
}