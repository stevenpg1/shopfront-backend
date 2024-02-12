namespace shopfront_backend.Models
{
    public class OrderItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Quantity { get; set; }
        public StockItem StockItem { get; set; } = new StockItem();
    }
}
