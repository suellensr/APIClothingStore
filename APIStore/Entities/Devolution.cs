using System.ComponentModel.DataAnnotations;

namespace APIStore.Entities
{
    public class Devolution
    {
        [Required]
        public List<Product> ReturnProducts { get; set; }
        [Required]
        public decimal TotalAmountToReturn { get; set; }
    }
}
