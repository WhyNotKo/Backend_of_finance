using Finance_control.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Finance_control.Controllers
{
   [ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TransactionsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTransactions()
    {
        var transactions = await _context.Transactions
            .Include(t => t.Category)
            .Include(t => t.User)
            .Select(t => new
            {
                t.Id,
                t.Description,
                t.Amount,
                t.IsIncome,
                Category = t.Category.Name,
                User = t.User.Username
            })
            .ToListAsync();
        return Ok(transactions);
    }

    [HttpGet("ByCategory/{categoryId}")]
    public async Task<IActionResult> GetTransactionsByCategory(int categoryId)
    {
        var transactions = await _context.Transactions
            .Where(t => t.CategoryId == categoryId)
            .Select(t => new
            {
                t.Id,
                t.Description,
                t.Amount,
                t.IsIncome
            })
            .ToListAsync();

        return Ok(transactions);
    }

    [HttpGet("SummaryByUser")]
    public async Task<IActionResult> GetTransactionSummaryByUser()
    {
        var summary = await _context.Transactions
            .GroupBy(t => t.User.Username)
            .Select(g => new
            {
                User = g.Key,
                TotalIncome = g.Where(t => t.IsIncome).Sum(t => t.Amount),
                TotalExpense = g.Where(t => !t.IsIncome).Sum(t => t.Amount)
            })
            .ToListAsync();

        return Ok(summary);
    }
}

}
