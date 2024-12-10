using Microsoft.AspNetCore.Mvc;
using Finance_control.Data;
using Finance_control.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

[Route("api/[controller]")]
[ApiController]
public class ReportsController : ControllerBase
{
    private readonly Finance_controlContext _context;

    public ReportsController(Finance_controlContext context)
    {
        _context = context;
    }

    // GET: api/Reports
    [HttpGet]
    [Authorize(Roles ="admin")]
    public async Task<ActionResult<Report>> GetReport(
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate)
    {
        // Получаем все транзакции из базы
        var query = _context.Transaction.AsQueryable();

        // Если указаны даты, фильтруем транзакции по периоду
        if (startDate.HasValue)
        {
            query = query.Where(t => t.Date >= startDate.Value);
        }

        if (endDate.HasValue)
        {
            query = query.Where(t => t.Date <= endDate.Value);
        }

        // Выполняем расчет
        var totalIncome = await query
            .Where(t => t.IsIncome())
            .SumAsync(t => t.Amount);

        var totalExpense = await query
            .Where(t => t.IsExpense() )
            .SumAsync(t => t.Amount);

        var report = new Report
        {
            Period = DateTime.Now, // Устанавливаем текущую дату как дату отчета
            TotalIncome = totalIncome,
            TotalExpense = totalExpense,

        };

        return Ok(report);
    }
    // GET: api/Reports/net-balance
    [HttpGet("net-balance")]
    [Authorize]
    public async Task<ActionResult<decimal>> GetNetBalance()
    {
        // Получаем все транзакции из базы
        var query = _context.Transaction.AsQueryable();

        // Подсчитываем доходы и расходы
        var totalIncome = await query
            .Where(t => t.Type == "Income")
            .SumAsync(t => t.Amount);

        var totalExpense = await query
            .Where(t => t.Type == "Expense")
            .SumAsync(t => t.Amount);

        var report = new Report
        {
            Period = DateTime.Now, // Устанавливаем текущую дату как дату отчета
            TotalIncome = totalIncome,
            TotalExpense = totalExpense,

        };

        // Вычисляем чистый баланс
        var netBalance = report.NetBalance();

        return Ok(netBalance);
    }

}
