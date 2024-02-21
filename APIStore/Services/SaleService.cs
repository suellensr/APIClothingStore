using System;
using APIStore.Entities;
using APIStore.Infrastructure;
using APIStore.Repository;

namespace APIStore.Entities
{
    public interface ISaleService
    {
        public void RegisterSale(int[] productIds, string clientName);
        public List<Sale> GetAllSales();
        public Sale GetSaleById(int saleId);
    }
    public class SaleService : ISaleService
    {
        private readonly IProductRepository _productRepository;
        private List<Sale> _sales = new List<Sale>();

        public SaleService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void RegisterSale(int[] productIds, string clientName)
        {
            var products = new List<Product>();
            foreach (var productId in productIds)
            {
                var product = _productRepository.GetProductById(productId);
                if (product != null)
                {
                    products.Add(product);
                }
                else
                {
                    throw new NotFoundException($"O produto com ID {productId} não foi encontrado. A compra não pode ser finalizada.");
                }
            }

            decimal totalAmount = products.Sum(product => product.Price);

            var sale = new Sale
            {
                Products = products,
                ClientName = clientName,
                TotalAmount = totalAmount
            };

            _sales.Add(sale);
        }

        public List<Sale> GetAllSales()
        {
            return _sales;
        }

        public Sale GetSaleById(int saleId)
        {
            return _sales.FirstOrDefault(sale => sale.Id == saleId);
        }
    }
}
