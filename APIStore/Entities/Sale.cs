using System.ComponentModel.DataAnnotations;

namespace APIStore.Entities
{
    public class Sale
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public List<Product>? Products { get; set; }
        [Required]
        public string? ClientName { get; set; }
        [Required]
        public decimal TotalAmount { get; set; }

        //private static int nextId = 0;

        //public Sale ()
        //{
        //    Id = nextId++;
        //}

    }
}
