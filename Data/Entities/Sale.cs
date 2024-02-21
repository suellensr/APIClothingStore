namespace Data.Entities
{
    public class Sale
    {
        public int Id { get; set; }
        public List<Product>? Products { get; set; }
        public string? ClientName { get; set; }
        public decimal TotalAmount { get; set; }

        private static int nextId = 0;

        public Sale ()
        {
            Id = nextId++;
        }

    }
}
