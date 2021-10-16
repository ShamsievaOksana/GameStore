using GameStore.Domain.Products;
using GameStore.Foundation;
using GameStore.WebClient.Product;

namespace GameStore.App.Mappers.Products
{
    public class ProductModelToProductMapper
        : Mapper<ProductModel, Product>
    {
        public override void Map(ProductModel source, Product target)
        {
            source.ShouldNotBeNull(nameof(source));
            target.ShouldNotBeNull(nameof(target));

            target.Id = source.Id;
            target.Name = source.Name;
            target.Description = source.Description;
            target.Price = source.Price;
            target.Published = source.Published;
        }
    }
}