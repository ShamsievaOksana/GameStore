using GameStore.Domain.Products;
using GameStore.Foundation;
using GameStore.WebClient.Product;

namespace GameStore.App.Mappers.Products
{
    public class ProductToProductModelMapper
        : Mapper<Product, ProductModel>
    {
        public override void Map(Product source, ProductModel target)
        {
            source.ShouldNotNull(nameof(source));
            target.ShouldNotNull(nameof(target));

            target.Id = source.Id;
            target.Name = source.Name;
            target.Description = source.Description;
            target.Price = source.Price;
            target.Published = source.Published;
        }
    }
}