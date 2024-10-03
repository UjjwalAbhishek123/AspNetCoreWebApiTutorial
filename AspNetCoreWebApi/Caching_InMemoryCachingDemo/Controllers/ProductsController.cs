using Caching_InMemoryCachingDemo.Models;
using Caching_InMemoryCachingDemo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Caching_InMemoryCachingDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //creating IMemoryCache private object
        private readonly IMemoryCache _cache;

        //create IProductRepository object to access service layer
        private readonly IProductRepository _repository;

        public ProductsController(IMemoryCache cache, IProductRepository repository)
        {
            _cache = cache;
            _repository = repository;
        }

        //GET: get all products
        [HttpGet]
        public async Task<ActionResult> GetAllProducts()
        {
            var products = await _repository.GetAllProductsAsync();
            return Ok(products);
        }

        //GET: get particular product
        [HttpGet("{id}")]
        public async Task<ActionResult> GetProductById(int id)
        {
            //create a cache key
            string cacheKey = $"product_{id}";

            //try to get data from cache at defined key
            //if data is not in cache => cache Miss => fetch data from data source
            if (!_cache.TryGetValue(cacheKey, out Product product))
            {
                //if data is not in cache, fetch data
                product = await _repository.GetProductByIdAsync(id);

                if(product == null)
                {
                    return NotFound();
                }

                //Set the cache options
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5)); //cache 5 minutes

                //Save data in cache
                _cache.Set(cacheKey, product, cacheEntryOptions);
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            await _repository.CreateProductAsync(product);

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            product.Id = id;

            //update the product
            await _repository.UpdateProductAsync(product);

            //after updation, clear the cache
            _cache.Remove($"product_{id}");

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            //delete particular product
            await _repository.DeleteProductAsync(id);

            //after deletion, clear the cache
            _cache.Remove($"product_{id}");

            return NoContent();
        }
    }
}
