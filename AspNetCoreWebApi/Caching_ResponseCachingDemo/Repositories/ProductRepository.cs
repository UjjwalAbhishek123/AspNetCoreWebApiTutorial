using Caching_ResponseCachingDemo.Models;

namespace Caching_ResponseCachingDemo.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products;

        public ProductRepository()
        {
            _products = new List<Product>()
            {
                new Product { Id = 1, Name = "Product A", Price = 100 },
                new Product { Id = 2, Name = "Product B", Price = 150 },
            };
        }

        public async Task AddProductAsync(Product product)
        {
            await Task.Run(() => _products.Add(product));
        }

        public async Task DeleteProductAsync(int id)
        {
            await Task.Run(() =>
            {
                var product = _products.FirstOrDefault(p => p.Id == id);

                if (product != null)
                {
                    _products.Remove(product);
                }
            });
        }

        public async Task<List<Product>> GetAllProductAsync()
        {
            return await Task.FromResult(_products.ToList());
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await Task.FromResult(_products.FirstOrDefault(p => p.Id == id));
        }

        public async Task UpdateProductAsync(Product product)
        {
            await Task.Run(() =>
            {
                var existingProduct = _products.FirstOrDefault(p => p.Id == product.Id);

                if (existingProduct != null)
                {
                    existingProduct.Name = product.Name;
                    existingProduct.Price = product.Price;
                }
            });
        }
    }
}
