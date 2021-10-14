using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameStore.Domain.Products
{
    public interface IProductService
    {
        Task<Product> CreateProduct(Product product);
        
        Task<Product> UpdateProduct(Product product);
        
        Task<Product> GetProductById(int id);
        
        Task<IList<Product>> GetProducts();
        
        Task DeleteProduct(int id);
    }
}