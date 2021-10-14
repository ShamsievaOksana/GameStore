using System.Threading.Tasks;
using GameStore.Domain.Products;
using GameStore.WebClient.Product;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.App.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController
        : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        
        /// <summary>
        /// Gets list of products.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }
        
        /// <summary>
        /// Gets a product by given ID.
        /// </summary>
        /// <param name="id">Product ID.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProductModel model)
        {
            return Ok();
        }
        
        [HttpPut]
        public async Task<IActionResult> Put(ProductModel model)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Detele(int id)
        {
            return NoContent();
        }
    }
}