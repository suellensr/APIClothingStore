using System.Security.Cryptography;

namespace APIStore.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }

        private static int nextId = 0;
        public Product(string productName, decimal price)
        {
            Id = nextId++;
            ProductName = productName;
            Price = price;
        }
    }
}
