using Microsoft.AspNetCore.Mvc;
using Finance_control.Data;
using Finance_control.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Finance_control;

[Route("api/[controller]")]
[ApiController]
public class ReportsController : ControllerBase
{
    private readonly Manager _manager;

    public ReportsController(Manager manager)
    {
        _manager = manager;
    }

    // GET: api/Reports
    [HttpGet]
    //[Authorize]
    public async Task<ActionResult<Report>> GetReport([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
    {
        var report = await _manager.GenerateReportAsync(startDate, endDate);
        return Ok(report);
    }

    // GET: api/Reports/net-balance
    [HttpGet("net-balance")]
    //[Authorize]
    public async Task<ActionResult<decimal>> GetNetBalance([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
    {
        var netBalance = await _manager.CalculateNetBalanceAsync(startDate, endDate);
        return Ok(netBalance);
    }
}