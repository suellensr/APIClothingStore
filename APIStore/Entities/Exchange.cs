namespace APIStore.Entities
{
    public class Exchange
    {
        public List<Product> ProductsToExchange { get; set; }
        public decimal TotalAmountExchange { get; set; }
        public List<Product> NewProducts { get; set; }
        public decimal TotalAmountNewProducts { get; set; }
        public decimal ValueDifference { get; set; }
    }
}
