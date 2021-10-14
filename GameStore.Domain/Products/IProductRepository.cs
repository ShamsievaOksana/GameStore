using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameStore.Domain.Products
{
    public interface IProductRepository
    {
        Task<Product> Add(Product product);
        
        Task<Product> Update(Product product);
        
        Task<Product> Get(int id);
        
        Task<IList<Product>> Get();
        
        Task Delete(int id);
    }
}