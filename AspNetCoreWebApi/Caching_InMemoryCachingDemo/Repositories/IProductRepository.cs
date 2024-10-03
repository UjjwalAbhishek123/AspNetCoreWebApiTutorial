using Caching_InMemoryCachingDemo.Models;

namespace Caching_InMemoryCachingDemo.Repositories
{
    public interface IProductRepository
    {
        //to get list of all products
        Task<List<Product>> GetAllProductsAsync();
        
        //to get individual product
        Task<Product> GetProductByIdAsync(int id);
        
        //to create product
        Task CreateProductAsync(Product product);

        //to update product
        Task UpdateProductAsync(Product product);

        //to delete product
        Task DeleteProductAsync(int id);
    }
}
