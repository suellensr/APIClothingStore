using Data.Entities;
using Data.Repository;
using System;

namespace Data.Entities
{
	public class ProductService
	{
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void CreateProduct(Product product)
        {
            _productRepository.AddProduct(product);
        }

        public void UpdateProduct(int productId, string newName, decimal newPrice)
        {
            _productRepository.UpdateProduct(productId, newName, newPrice);
        }
    }
}

