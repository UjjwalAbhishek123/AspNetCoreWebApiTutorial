using Caching_DistributedCachingDemo.Data;
using Caching_DistributedCachingDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace Caching_DistributedCachingDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //creating private field of ApplicationDbCContext
        private readonly ApplicationDbContext _dbContext;

        //creating private field for Distributed Cache
        private readonly IDistributedCache _cache;

        public ProductsController(IDistributedCache cache, ApplicationDbContext dbContext)
        {
            _cache = cache;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var cacheKey = "Get_All_Products";

            List<Product> products;

            //Get data from cache
            var cachedData = await _cache.GetAsync(cacheKey);

            if (cachedData != null)
            {
                //if data is found in cache, ENCODE and DESERIALIZE cached data
                //Encoding cache data
                var cachedDataString = Encoding.UTF8.GetString(cachedData);

                //deserializing Cache data
                products = JsonSerializer.Deserialize<List<Product>>(cachedDataString) ??
                    new List<Product>();
            }
            else
            {
                //If not found in cache, fetch from DATABASE
                products = await _dbContext.Products.ToListAsync();

                //Serialize data => Convert db object to JSON string
                var cachedDataString = JsonSerializer.Serialize(products);

                //Encoding => JSON string to byte array
                var newDataToCache = Encoding.UTF8.GetBytes(cachedDataString);

                //set cache options
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(2))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                //add Data in Cache
                await _cache.SetAsync(cacheKey, newDataToCache, options);
            }

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>GetProductById(int id)
        {
            var cacheKey = $"Product_{id}";
            
            Product product;

            //get data from cache
            var cachedData = await _cache.GetAsync(cacheKey);

            if (cachedData != null)
            {
                //if data found in cache, Encode and Desrialize cached data
                var cachedDataString = Encoding.UTF8.GetString(cachedData);

                product = JsonSerializer.Deserialize<Product>(cachedDataString) ?? new Product();
            }
            else
            {
                // If not found, then fetch data from database
                product = await _dbContext.Products.FirstOrDefaultAsync(prd => prd.Id == id) ?? new Product();

                //Serialize
                var cachedDataString = JsonSerializer.Serialize(product);

                //Encode
                var newDataToCache = Encoding.UTF8.GetBytes(cachedDataString);

                //Set cache options
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddHours(24))
                    .SetSlidingExpiration(TimeSpan.FromHours(12));

                //add data in cache
                await _cache.SetAsync(cacheKey, newDataToCache, options);
            }
            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            // Check if the provided ID matches the product ID
            if (id != product.Id)
            {
                return BadRequest(); // Return BadRequest if IDs do not match
            }

            // Set the entity state to modified
            _dbContext.Entry(product).State = EntityState.Modified;

            try
            {
                // Save changes to the database
                await _dbContext.SaveChangesAsync();

                // Create a cache key for the product
                var cacheKey = $"Product_{id}";

                // Serialize the product data
                var cachedDataString = JsonSerializer.Serialize(product);

                // Encode the data to byte array
                var newDataToCache = Encoding.UTF8.GetBytes(cachedDataString);

                // Set cache options
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddHours(24))
                    .SetSlidingExpiration(TimeSpan.FromHours(12));

                // Add the data to the cache
                await _cache.SetAsync(cacheKey, newDataToCache, options);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Check if the product exists in the database
                if (!ProductExists(id))
                {
                    return NotFound(); // Return NotFound if the product does not exist
                }
                else
                {
                    // Generate a custom response for the error
                    var customResponse = new
                    {
                        Code = 500,
                        Message = "Internal Server Error",
                        ErrorMessage = ex.Message // Not exposing actual error message to client
                    };
                    return StatusCode(StatusCodes.Status500InternalServerError, customResponse); // Return Internal Server Error response
                }
            }

            return NoContent(); // Return NoContent if the update was successful
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            //finding product at particular id
            var product = await _dbContext.Products.FindAsync(id);

            if(product == null)
            {
                return NotFound();
            }

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();

            var cacheKey = $"product_{id}";

            //remove cache
            await _cache.RemoveAsync(cacheKey);

            return NoContent();
        }

        //method to check that product exists at given id or not
        private bool ProductExists(int id)
        {
            return _dbContext.Products.Any(elem => elem.Id == id);
        }
    }
}