using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace APIStore.Entities
{
    public class Product
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public decimal Price { get; set; }

        //private static int nextId = 0;
        //public Product(string productName, decimal price)
        //{
        //    Id = nextId++;
        //    ProductName = productName;
        //    Price = price;
        //}
    }
}
