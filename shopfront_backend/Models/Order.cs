namespace shopfront_backend.Models
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }

    }
}
