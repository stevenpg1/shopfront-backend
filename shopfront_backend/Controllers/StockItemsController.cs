using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shopfront_backend.DatabaseContext;
using shopfront_backend.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace shopfront_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StockItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/StockItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockItem>>> GetStockItems()
        {
            return await _context.StockItems.Where(i => i.StockCount > 0).ToListAsync();
        }

        // GET: api/StockItems/5 
        [HttpGet("{id}")]
        public async Task<ActionResult<StockItem>> GetStockItem(Guid id)
        {
            var stockItem = await _context.StockItems.FindAsync(id);

            if (stockItem == null)
            {
                return NotFound();
            }

            return stockItem;
        }

        // PUT: api/StockItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStockItem(Guid id, StockItem stockItem)
        {
            if (id != stockItem.Id)
            {
                return Problem("StockItem Id on query string must match that in the json body data");
            }

            _context.Entry(stockItem).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockItemExists(id))
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

        // POST: api/StockItems
        [HttpPost]
        public async Task<ActionResult<StockItem>> PostStockItem(StockItem stockItem)
        {
            _context.StockItems.Add(stockItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostStockItem", new { id = stockItem.Id }, stockItem);
        }

        // DELETE: api/StockItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockItem(Guid id)
        {
            var stockItem = await _context.StockItems.FindAsync(id);
            if (stockItem == null)
            {
                return NotFound();
            }

            _context.StockItems.Remove(stockItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StockItemExists(Guid id)
        {
            return _context.StockItems.Any(e => e.Id == id);
        }
    }
}
