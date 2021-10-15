using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Domain.Products;
using GameStore.Foundation;
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
        private readonly IMapper<ProductModel, Product> _productModelToProductMapper;
        private readonly IMapper<Product, ProductModel> _productToProductModelMapper;

        public ProductController(IProductService productService,
            IMapper<ProductModel, Product> productModelToProductMapper,
            IMapper<Product, ProductModel> productToProductModelMapper)
        {
            _productService = productService;
            _productModelToProductMapper = productModelToProductMapper;
            _productToProductModelMapper = productToProductModelMapper;
        }
        
        /// <summary>
        /// Gets list of products.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _productService.GetProducts();

            return Ok(MapProducts(products));
        }

        /// <summary>
        /// Gets a product by given ID.
        /// </summary>
        /// <param name="id">Product ID.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _productService.GetProductById(id);

            if (product == null)
                return NotFound();
            
            return Ok(MapProduct(product));
        }


        [HttpPost]
        public async Task<IActionResult> Post(ProductModel model)
        {
            var product = await _productService.CreateProduct(MapProduct(model));

            if (product == null)
                return StatusCode(500);

            return Created($"/api/product/{product.Id}", MapProduct(product));
        }
        
        [HttpPut]
        public async Task<IActionResult> Put(ProductModel model)
        {
            var product = await _productService.UpdateProduct(MapProduct(model));
            
            if (product == null)
                return StatusCode(500);
            
            return Ok(MapProduct(product));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteProduct(id);
            return NoContent();
        }
        
        private List<ProductModel> MapProducts(IList<Product> products)
        {
            return products == null
                ? new List<ProductModel>()
                : products.Select(x => _productToProductModelMapper.Map(x)).ToList();
        }
        
        private ProductModel MapProduct(Product product)
        {
            return _productToProductModelMapper.Map(product);
        }
        
        private Product MapProduct(ProductModel product)
        {
            return _productModelToProductMapper.Map(product);
        }
    }
}