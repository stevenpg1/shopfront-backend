namespace shopfront_backend.ViewModels
{
    public class OrderItemStockItemVM
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public int StockCount { get; set; }
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
