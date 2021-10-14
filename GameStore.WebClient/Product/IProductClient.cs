using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameStore.WebClient.Product
{
    public interface IProductClient
    {
        Task<ProductModel> Get(int id);
        
        Task<IList<ProductModel>> Get();

        Task<ProductModel> Create(ProductModel model);
        
        Task<ProductModel> Update(ProductModel model);
        
        Task Delete(int id);
    }
}