using Data.Infrastructure;
using Data.Repository;
using System;

namespace Data.Entities
{
    public class DevolutionService
    {

        private readonly IProductRepository _productRepository;
        private readonly ISaleService _saleService;

        public DevolutionService(IProductRepository productRepository, ISaleService salesService)
        {
            _productRepository = productRepository;
            _saleService = salesService;
        }

        public Devolution CreateDevolution(int saleId, int[] returnProductIds)
        {
            Sale originalSale = _saleService.GetSaleById(saleId);

            if (originalSale == null)
            {
                throw new NotFoundException($"A venda com ID {saleId} não foi encontrada.");
            }

            //Filtering products from repository by ID
            var returnProducts = new List<Product>();

            foreach (var returnProductId in returnProductIds)
            {
                var product = _productRepository.GetProductById(returnProductId);
                if (product != null)
                {
                    returnProducts.Add(product);
                }
                else
                {
                    throw new NotFoundException($"O produto com ID {returnProductId} não foi encontrado." +
                        $" Não será possível fazer a devolução.");
                }
            }


            // Check if the return products were in the original sale
            foreach (var returnProduct in returnProducts)
            {
                if (!originalSale.Products.Contains(returnProduct))
                {
                    throw new Exception($"O produto com ID {returnProduct.Id} não estava na venda original." +
                        $"Não será possível realizar a devolução.");
                }
            }

            decimal totalAmountToReturn = returnProducts.Sum(product => product.Price);

            var devolution = new Devolution
            {
                ReturnProducts = returnProducts,
                TotalAmountToReturn = totalAmountToReturn
            };

            return devolution;
        }
            
    }
}


