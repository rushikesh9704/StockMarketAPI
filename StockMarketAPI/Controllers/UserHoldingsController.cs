using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockMarketAPI.Models;
using StockMarketAPI.StockDBContext;

namespace StockMarketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserHoldingsController : ControllerBase
    {
        private readonly StockMarketApplicationDbContext _context;

        public UserHoldingsController(StockMarketApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UserHoldings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserHolding>>> GetUserHoldings()
        {
            return await _context.UserHoldings.ToListAsync();
        }

        // GET: api/UserHoldings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserHolding>> GetUserHolding(int id)
        {
            var userHolding = await _context.UserHoldings.FindAsync(id);

            if (userHolding == null)
            {
                return NotFound();
            }

            return userHolding;
        }

        // PUT: api/UserHoldings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserHolding(int id, UserHolding userHolding)
        {
            if (id != userHolding.HoldingId)
            {
                return BadRequest();
            }

            _context.Entry(userHolding).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserHoldingExists(id))
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

        // POST: api/UserHoldings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserHolding>> PostUserHolding(UserHolding userHolding)
        {
            userHolding.PurchasePrice = 
                _context.StockPrices.OrderByDescending(x=>x.PriceDate).FirstOrDefault(x => x.StockId == userHolding.StockId).Price.Value;
                
            _context.UserHoldings.Add(userHolding);


            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserHolding", new { id = userHolding.HoldingId }, userHolding);
        }

        // DELETE: api/UserHoldings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserHolding(int id)
        {
            var userHolding = await _context.UserHoldings.FindAsync(id);
            if (userHolding == null)
            {
                return NotFound();
            }

            _context.UserHoldings.Remove(userHolding);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserHoldingExists(int id)
        {
            return _context.UserHoldings.Any(e => e.HoldingId == id);
        }
    }
}
