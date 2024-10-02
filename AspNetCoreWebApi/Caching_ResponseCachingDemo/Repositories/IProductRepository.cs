using Caching_ResponseCachingDemo.Models;

namespace Caching_ResponseCachingDemo.Repositories
{
    public interface IProductRepository
    {
        //define all the methods here for Data access layer
        Task<List<Product>> GetAllProductAsync();

        Task<Product> GetProductByIdAsync(int id);
        
        Task AddProductAsync(Product product);
        
        Task UpdateProductAsync(Product product);
        
        Task DeleteProductAsync(int id);
    }
}
