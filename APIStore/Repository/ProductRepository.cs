using APIStore.Entities;
using APIStore.Infrastructure;

namespace APIStore.Repository
{
    public interface IProductRepository
    {
        void AddProduct(Product product);
        List<Product> GetAllProducts();
        Product GetProductById(int productId);
        void DeleteProduct(int productId);
        void UpdateProduct(int productId, string newProductName, decimal newProductPrice);
    }

    public class ProductRepository : IProductRepository
    {
        private List<Product> _products = new List<Product>();

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public void DeleteProduct(int productId)
        {
            var productToRemove = GetProductById(productId);
            if (productToRemove != null)
            {
                _products.Remove(productToRemove);
            }
        }

        public List<Product> GetAllProducts()
        {
            return _products;
        }

        public Product GetProductById(int productId)
        {
            return _products.FirstOrDefault(p => p.Id == productId);
        }

        public void UpdateProduct(int productId, string newProductName, decimal newPrice)
        {
            var existingProduct = _products.FirstOrDefault(product => product.Id == productId);
            if (existingProduct != null)
            {
                existingProduct.ProductName = newProductName;
                existingProduct.Price = newPrice;
            }
            else
            {
                throw new NotFoundException($"O produto com ID {productId} não foi encontrado.");
            }
        }
    }
}
