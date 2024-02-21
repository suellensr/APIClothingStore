using System.ComponentModel.DataAnnotations;

namespace APIStore.Entities
{
    public class Exchange
    {
        [Required]
        public List<Product> ProductsToExchange { get; set; }
        [Required]
        public decimal TotalAmountExchange { get; set; }
        [Required]
        public List<Product> NewProducts { get; set; }
        [Required]
        public decimal TotalAmountNewProducts { get; set; }
        [Required]
        public decimal ValueDifference { get; set; }
    }
}
