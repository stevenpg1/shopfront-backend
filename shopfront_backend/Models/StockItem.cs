using System.Runtime.InteropServices;

namespace shopfront_backend.Models
{
    public class StockItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public int StockCount { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
