using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Finance_control.Data;
using Finance_control.Models;
using Microsoft.AspNetCore.Authorization;

namespace Finance_control.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly Manager _manager;

        public TransactionsController(Manager manager)
        {
            _manager = manager;
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransaction()
        {
            return Ok(await _manager.GetAllTransactionsAsync());
        }

        [HttpGet("GetPerDates")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactionPerDate([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            return Ok(await _manager.GetTransactionsAsyncPerDate(startDate,endDate));
        }

        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(int id)
        {
            var transaction = await _manager.GetTransactionByIdAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        // PUT: api/Transactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> PutTransaction(int id, Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return BadRequest();
            }

            try
            {
                await _manager.UpdateTransactionAsync(transaction);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await _manager.GetTransactionByIdAsync(id) is not null))
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

        // POST: api/Transactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
        {
            await _manager.AddTransactionAsync(transaction);

            return CreatedAtAction("GetTransaction", new { id = transaction.Id }, transaction);
        }

        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            await _manager.DeleteTransactionAsync(id);

            return NoContent();
        }
    }
}
