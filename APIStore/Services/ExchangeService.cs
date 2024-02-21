using APIStore.Infrastructure;
using APIStore.Repository;
using System;
using System.Collections.Generic;

namespace APIStore.Entities
{
	public class ExchangeService
	{
        private readonly IProductRepository _productRepository;
        private readonly ISaleService _saleService;
        public ExchangeService(IProductRepository productRepository, ISaleService saleService)
		{
            _productRepository = productRepository;
            _saleService = saleService;
        }

        public Exchange CreateExchange(int saleId, int[] exchangeProductIds, int[] newProductIds)
        {
            (bool isExchangeValid, List<Product> exchangeProducts, decimal totalAmountExchangeProducts) = IsExchangeValid(saleId, exchangeProductIds);

            (bool isNewProductsValid, List<Product> newProducts, decimal totalAmountNewProducts) = IsNewProductsValid(newProductIds);

            if (isExchangeValid && isNewProductsValid)
            {
                var exchange = new Exchange
                {
                    ProductsToExchange = exchangeProducts,
                    TotalAmountExchange = totalAmountExchangeProducts,
                    NewProducts = newProducts,
                    TotalAmountNewProducts = totalAmountNewProducts,
                    ValueDifference = (totalAmountExchangeProducts - totalAmountNewProducts) * (-1),
                };

                return exchange;
            }

            return null;
        }

            private (bool, List<Product>, decimal) IsExchangeValid(int saleId, int[] exchangeProductIds)
            {
                Sale originalSale = _saleService.GetSaleById(saleId);

                if (originalSale == null)
                {
                    return (false, null, 0.00m);
                    throw new NotFoundException($"A venda com ID {saleId} não foi encontrada.");
                }

                //Filtering products from repository by ID
                var exchangeProducts = new List<Product>();

                foreach (var exchangeProductId in exchangeProductIds)
                {
                    var product = _productRepository.GetProductById(exchangeProductId);
                    if (product != null)
                    {
                        exchangeProducts.Add(product);
                    }
                    else
                    {
                        return (false, null, 0.00m);
                        throw new NotFoundException($"O produto com ID {exchangeProductId} não foi encontrado." +
                            $" Não será possível fazer a troca.");
                    }
                }


                // Check if the return products were in the original sale
                foreach (var returnProduct in exchangeProducts)
                {
                    if (!originalSale.Products.Contains(returnProduct))
                    {
                        return (false, null, 0.00m);
                        throw new Exception($"O produto com ID {returnProduct.Id} não estava na venda original." +
                            $"Não será possível realizar a troca.");
                    }
                }

                decimal totalAmountExcehangeProducts = exchangeProducts.Sum(product => product.Price);

                return (true, exchangeProducts, totalAmountExcehangeProducts);
            }

            private (bool, List<Product>, decimal) IsNewProductsValid(int[] newProductIds)
            {
                //Filtering products from repository by ID
                var newProducts = new List<Product>();

                foreach (var newProductId in newProductIds)
                {
                    var product = _productRepository.GetProductById(newProductId);
                    if (product != null)
                    {
                        newProducts.Add(product);
                    }
                    else
                    {
                        return (false, null, 0.00m);
                        throw new NotFoundException($"O produto com ID {newProductId} não foi encontrado." +
                            $" Não será possível fazer a troca.");
                    }
                }

                decimal totalAmountNewProducts = newProducts.Sum(product => product.Price);

                return (true, newProducts, totalAmountNewProducts);
            }
        
    }
}
