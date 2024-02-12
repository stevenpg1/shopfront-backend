namespace shopfront_backend.ViewModels
{
    public class CustomerOrderItemsVM
    {
        public string customerName { get; set; }
        public string customerAddress { get; set; }
        public string customerEmail { get; set; }
        public List<OrderItemStockItemVM> orderStockItems { get; set; }
    }
}
