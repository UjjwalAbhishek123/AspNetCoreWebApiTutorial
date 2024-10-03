using Caching_InMemoryCachingDemo.Models;

namespace Caching_InMemoryCachingDemo.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products;

        public ProductRepository()
        {
            _products = new List<Product>()
            {
                new Product { Id = 1, Name = "Product A", Price = 10.0m },
                new Product { Id = 2, Name = "Product B", Price = 20.0m }
            };
        }

        public Task UpdateProductAsync(Product product)
        {
            var index = _products.FindIndex(p => p.Id == product.Id);
            if (index != -1)
            {
                _products[index] = product;
            }

            return Task.CompletedTask;
        }

        public Task DeleteProductAsync(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _products.Remove(product);
            }
            return Task.CompletedTask;
        }

        public Task<List<Product>> GetAllProductsAsync()
        {
            return Task.FromResult(_products);
        }

        public Task<Product> GetProductByIdAsync(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(product);
        }

        public Task CreateProductAsync(Product product)
        {
            //simple ID generation
            product.Id = _products.Count + 1;

            _products.Add(product);

            return Task.CompletedTask;
        }
    }
}
