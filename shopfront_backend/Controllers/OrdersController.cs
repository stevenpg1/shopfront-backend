using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shopfront_backend.DatabaseContext;
using shopfront_backend.Models;
using shopfront_backend.ViewModels;

namespace shopfront_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        // GET: api/Orders/5 
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(Guid id, Order order)
        {
            if (id != order.Id)
            {
                return Problem("Order Id on query string must match that in the json body data");
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(CustomerOrderItemsVM customerOrderItemsVM)
        {
            //TransformVMOrderIntoOrder(customerOrderItemsVM);
            var order = new Order();
            order.Id = Guid.NewGuid();
            order.OrderDate = DateTime.Now;
            order.CustomerName = customerOrderItemsVM.customerName;
            order.CustomerAddress = customerOrderItemsVM.customerAddress;
            _context.Orders.Add(order);

            foreach (var item in customerOrderItemsVM.orderStockItems)
            {
                OrderItem orderItem = new OrderItem();
                orderItem.Id = Guid.NewGuid();
                orderItem.Quantity = item.Amount;
                orderItem.StockItem = _context.StockItems.First(i => i.Id == item.Id);
                order.OrderItems.Add(orderItem);
            }

            try
            {
                await _context.SaveChangesAsync();

                foreach (var item in customerOrderItemsVM.orderStockItems)
                {
                    
                    StockItem stockItem = _context.StockItems.First(i => i.Id == item.Id);
                    stockItem.StockCount = item.StockCount - item.Amount;
                    _context.Entry(stockItem).State = EntityState.Modified;
                }

                await _context.SaveChangesAsync();

                // HERE IS WHERE SEND EMAIL COULD BE INITIATED - REQUIRES SETTING UP A MAIL PROVIDER
                // IT WOULD USE THIS TO THE EMAIL ADDRESS: customerOrderItemsVM.customerEmail;
                // 
            }
            catch (Exception ex)
            {

                string errMessage = ex.Message;
            }
            
            return CreatedAtAction("PostOrder", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(Guid id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }

        private Order TransformVMOrderIntoOrder(CustomerOrderItemsVM customerOrderItemsVM)
        {
            var order = new Order();


            if (customerOrderItemsVM != null)
            {
                order.Id = Guid.NewGuid();
                order.OrderDate = DateTime.Now;
                order.CustomerName = customerOrderItemsVM.customerName;
                order.CustomerAddress = customerOrderItemsVM.customerAddress;
                foreach (var item in customerOrderItemsVM.orderStockItems)
                {
                    OrderItem orderItem = new OrderItem();
                    orderItem.Id = Guid.NewGuid();
                    orderItem.Quantity = item.Amount;
                    StockItem stockItem = new StockItem();
                    stockItem.Id = item.Id;
                    stockItem.Name = item.Name;
                    stockItem.StockCount = item.StockCount - item.Amount;
                    stockItem.UnitPrice = item.UnitPrice;
                    orderItem.StockItem = stockItem;
                    order.OrderItems.Add(orderItem);
                }
            }

            return order;
        }
    }
}
