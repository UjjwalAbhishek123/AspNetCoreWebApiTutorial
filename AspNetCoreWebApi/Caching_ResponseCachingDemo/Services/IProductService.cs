using Caching_ResponseCachingDemo.Models;

namespace Caching_ResponseCachingDemo.Services
{
    public interface IProductService
    {
        //interface for business logic
        Task<List<Product>> GetAllProductAsync();

        Task<Product> GetProductByIdAsync(int id);

        Task AddProductAsync(Product product);

        Task UpdateProductAsync(Product product);

        Task DeleteProductAsync(int id);
    }
}
