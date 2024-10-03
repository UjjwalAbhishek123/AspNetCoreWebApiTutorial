using Caching_InMemoryCachingDemo.Models;

namespace Caching_InMemoryCachingDemo.Services
{
    public interface IProductService
    {
        //interface for business logic
        Task<List<Product>> GetAllProductsAsync();

        Task<Product> GetProductByIdAsync(int id);

        Task CreateProductAsync(Product product);

        Task UpdateProductAsync(Product product);

        Task DeleteProductAsync(int id);
    }
}
