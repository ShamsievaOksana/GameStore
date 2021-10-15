using GameStore.DataModel;
using GameStore.Domain.Products;
using GameStore.Foundation;

namespace GameStore.Infrastructure.Database.Products.Mappers
{
    public class ProductEntityToProductMapper
        : Mapper<ProductEntity, Product>
    {
        public override void Map(ProductEntity source, Product target)
        {
            source.ShouldNotNull();
            target.ShouldNotNull();
            
            target.Id = source.Id;
            target.Name = source.Name;
            target.Description = source.Description;
            target.Price = source.Price;
            target.Published = source.Published;
        }
    }
}