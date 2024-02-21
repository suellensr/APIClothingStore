using System;
using APIStore.Entities;

namespace APIStore.Entities
{
    public class SalesService
    {
        private List<Product> _products;
        private string _clientName;
        private decimal _totalAmount = 0.00m;

        public SalesService(List<Product> produtos, string clientName)
        {
            _products = produtos;
            _clientName = clientName;
        }

        internal void CalculateTotalAmount()
        {
            
            foreach (var product in _products)
            {
                _totalAmount += product.;
            }
        }
    }
}
