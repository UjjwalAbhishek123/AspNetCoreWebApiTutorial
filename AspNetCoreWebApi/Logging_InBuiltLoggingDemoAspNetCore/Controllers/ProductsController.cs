using Logging_InBuiltLoggingDemoAspNetCore.Data;
using Logging_InBuiltLoggingDemoAspNetCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Logging_InBuiltLoggingDemoAspNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //creating readonly property of ILogger
        private readonly ILogger<ProductsController> _logger;

        private readonly ApplicationDbContext _dbContext;
        public ProductsController(ILogger<ProductsController> logger, ApplicationDbContext dbcontext)
        {
            _logger = logger;
            _dbContext = dbcontext;
            
        }

        //GET all products
        [HttpGet]
        public async Task<IActionResult> GetAllPRoducts()
        {
            //logging the info
            //in-built logging
            //_logger.LogInformation("Fetching all products");

            //Logging using Serilog
            Log.Information("Fetching all products");

            var products = await _dbContext.Products.ToListAsync();
            return Ok(products);

        }

        //GET product by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            //logging the info
            //in-built logging
            //_logger.LogDebug("Trying to fetch product with ID {id}", id);

            //Logging using Serilog
            Log.Debug("Received request to fetch product with ID {ProductId}", id);

            var product = await _dbContext.Products.FindAsync(id);

            if (product == null)
            {
                //_logger.LogWarning("Product with ID {id} not found..", id);
                Log.Warning("Product with Id {ProductId} not found", id);
                return NotFound();
            }

            //_logger.LogInformation("Successfully fetched product with Id {id}", id);
            Log.Warning("Fetching product with ID {ProductId}", id);
            return Ok(product);
        }

        //Create new Product
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            if(product == null)
            {
                //_logger.LogError("Product object is null during creation attempt");
                Log.Error("Received null product for creation");

                return BadRequest("Product object is null");
            }

            if (string.IsNullOrWhiteSpace(product.Name))
            {
                //_logger.LogError("Product name is null or empty");
                Log.Error("Product name is null or empty");

                return BadRequest("Product name cannot be null or empty");
            }

            if (product.Price <= 0)
            {
                //_logger.LogError("Price must be greater than zero");
                Log.Error("Price must be greater than zero");

                return BadRequest("Product price must be greater than zero");
            }

            //_logger.LogDebug("Creating product: {@Product}", product);
            Log.Debug("Creating product: {@Product}", product);

            _dbContext.Products.Add(product);

            //saving to db
            await _dbContext.SaveChangesAsync();

            //_logger.LogInformation("Product created with ID {Id}", product.Id);

            Log.Error("Product created with ID {Id}", product.Id);

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        //Update an exiting product
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            Log.Debug("Received Request to update product with ID {ProductId}", id);

            if(updatedProduct == null)
            {
                //_logger.LogError("Updated product object is null during update attempt for ID {Id}", id);
                Log.Error("Updated product object is null during update attempt for ID {Id}", id);

                return BadRequest("Updated product object is null");
            }

            if (id != updatedProduct.Id)
            {
                //_logger.LogWarning("Product ID mismatch: {Id} vs {UpdatedId}", id, updatedProduct.Id);

                Log.Warning("Product ID mismatch: {Id} vs {UpdatedId}", id, updatedProduct.Id);

                return BadRequest("Product ID mismatch");
            }

            var product = await _dbContext.Products.FindAsync(id);
            if (product == null)
            {
                //_logger.LogWarning("Product with ID {Id} not found for update", id);

                Log.Warning("Product with ID {Id} not found for update", id);
                return NotFound();
            }

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;

            //save to database
            await _dbContext.SaveChangesAsync();

            //_logger.LogInformation("Product with ID {Id} updated", id);

            Log.Information("Product with ID {Id} updated", id);
            return NoContent();
        }

        // Delete a product
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Log.Debug("Received Request to delete product with ID {ProductId}", id);

            var product = await _dbContext.Products.FindAsync(id);
            if (product == null)
            {
                //_logger.LogWarning("Product with ID {Id} not found for deletion", id);

                Log.Warning("Product with ID {Id} not found for deletion", id);
                return NotFound();
            }

            _dbContext.Products.Remove(product);
            
            await _dbContext.SaveChangesAsync(); // Save to database
            
            //_logger.LogInformation("Product with ID {Id} deleted", id);

            Log.Information("Product with ID {Id} deleted", id);
            
            return NoContent();
        }
    }
}
